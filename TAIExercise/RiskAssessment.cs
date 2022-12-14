using JOS.ExternalMergeSort;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIExercise
{
	public class RiskAssessment
	{
		private readonly String _tempPath = "C:/Temp/riskFiles";
		private readonly String _sortedById = "sortedById.csv";
		private readonly String _evaluated = "evaluated.csv";
		private readonly RiskAssessmentOptions _options = null;

		public RiskAssessment(RiskAssessmentOptions options = null)
		{
			_options = options ?? new RiskAssessmentOptions();
		}
		
		public async Task RunAssessment()
		{
			//Sort by Id first
			await SortById();

			//Read lines per Id, evaluate, write if necessary
			EvaluateFile();

			//Sort _evaluated file by risk amount and Id
			await SortByRiskAmountAndId();

			//Delete temp files and folders
			Cleanup();
		}

		private void Cleanup()
		{
			if (File.Exists(_sortedById))
			{
				File.Delete(_sortedById);
			}
			if (File.Exists(_evaluated))
			{
				File.Delete(_evaluated);
			}
			if (Directory.Exists(_tempPath))
			{
				Directory.Delete(_tempPath, true);
			}
		}

		private async Task SortByRiskAmountAndId()
		{
			ExternalMergeSorterOptions options = new ExternalMergeSorterOptions()
			{
				Sort = new ExternalMergeSortSortOptions()
				{
					//Sort the _evaluated file by Risk desc (column 3) and Id (column 0)
					Comparer = new CsvAmountAndIdColumnSorter(3, 0, true, _options.Separator)
				},
				FileLocation = _tempPath
			};

			ExternalMergeSorter sorter = new ExternalMergeSorter(options);

			using (FileStream unsortedFile = File.OpenRead(Path.Combine(_tempPath, _evaluated)))
			using (FileStream targetFile = File.Create(_options.OutputFile))
			{
				//Add header to final output file
				targetFile.Write(Encoding.ASCII.GetBytes("ID,LastName,FirstName,RiskAmount\n"));
				await sorter.Sort(unsortedFile, targetFile, CancellationToken.None);
			}
		}

		private void EvaluateFile()
		{
			using (StreamReader sr = new StreamReader(Path.Combine(_tempPath, _sortedById)))
			using (StreamWriter sw = new StreamWriter(Path.Combine(_tempPath, _evaluated)))
			{
				List<String[]> client = new List<String[]>();

				while (sr.Peek() != -1)
				{
					String line = sr.ReadLine();
					String[] fields = line.Split(_options.Separator);

					//If there is no current client OR the id from the new fields matches the current client id, add the fields
					if (client.Count == 0 || (client[0][_options.IdColumn] == fields[_options.IdColumn]))
					{
						client.Add(fields);
					}
					else
					{
						//fields is a new client.
						//Compute and evaluate current client
						//Write to file if above threshold
						//Reinitialize client with fields

						Decimal totalFace = 0;
						Decimal totalCash = 0;
						Decimal risk = 0;

						foreach (String[] row in client)
						{
							if (!Decimal.TryParse(row[_options.FaceAmountColumn], out Decimal rowFace))
							{
								throw new Exception($"Face Amount must be a number. Client: {row[_options.IdColumn]}");
							}
							if (!Decimal.TryParse(row[_options.CashValueColumn], out Decimal rowCash))
							{
								throw new Exception($"Cash Value must be a number. Client: {row[_options.IdColumn]}");
							}

							totalFace += rowFace;
							totalCash += rowCash;
						}

						risk = totalFace - totalCash;

						if (risk > _options.RiskThreshold)
						{
							sw.WriteLine($"{client[0][_options.IdColumn]},{client[0][_options.LastNameColumn]},{client[0][_options.FirstNameColumn]}, {risk}");
						}

						//Set next client
						client = new List<String[]> { fields };
					}
				}
			}
		}

		private async Task SortById()
		{
			//Check directories
			if (!Directory.Exists(Path.GetDirectoryName(_options.OutputFile)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(_options.OutputFile));
			}

			if (!Directory.Exists(_tempPath))
			{
				Directory.CreateDirectory(_tempPath);
			}

			ExternalMergeSorterOptions options = new ExternalMergeSorterOptions()
			{
				FileLocation = _tempPath
			};

			ExternalMergeSorter sorter = new ExternalMergeSorter(options);

			using (FileStream unsortedFile = File.OpenRead(_options.InputFile))
			using (FileStream targetFile = File.Create(Path.Combine(_tempPath, _sortedById)))
			{
				//A header should not be sorted with the rest of the lines
				//if there is a Header on the file, move the reader forward
				if (_options.HasHeader)
				{
					while (unsortedFile.Position < unsortedFile.Length)
					{
						Int32 byteInt = unsortedFile.ReadByte();
						Byte b = (Byte)byteInt;
						if (byteInt == -1 || b == '\n')
						{
							break;
						}
					}
				}
				await sorter.Sort(unsortedFile, targetFile, CancellationToken.None);
			}
		}
	}
}
