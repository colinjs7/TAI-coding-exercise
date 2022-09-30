using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIExercise
{
	public class RiskAssessmentOptions
	{
		internal readonly Boolean HasHeader = true;
		internal readonly Decimal RiskThreshold = 300000;
		internal readonly Int32 IdColumn = 0;
		internal readonly Int32 LastNameColumn = 1;
		internal readonly Int32 FirstNameColumn = 2;
		internal readonly Int32 FaceAmountColumn = 11;
		internal readonly Int32 CashValueColumn = 12;
		internal readonly String Separator = ",";
		internal readonly String InputFile = "C:/Temp/ClientCoveragePolicy.csv";
		internal readonly String OutputFile = "C:/Temp/RiskAssessment.csv";

		public RiskAssessmentOptions()
		{
			String header = ConfigurationManager.AppSettings["hasHeader"];
			String id = ConfigurationManager.AppSettings["idColumn"];
			String last = ConfigurationManager.AppSettings["lastNameColumn"];
			String first = ConfigurationManager.AppSettings["firstNameColumn"];
			String face = ConfigurationManager.AppSettings["faceAmountColumn"];
			String cash = ConfigurationManager.AppSettings["cashValueColumn"];
			String risk = ConfigurationManager.AppSettings["riskThreshold"];
			String sep = ConfigurationManager.AppSettings["separator"];
			String input = ConfigurationManager.AppSettings["inputFile"];
			String output = ConfigurationManager.AppSettings["outputFile"];

			if (String.IsNullOrWhiteSpace(header) || !Boolean.TryParse(header, out HasHeader))
			{
				HasHeader = true;
			}
			if (String.IsNullOrWhiteSpace(id) || !Int32.TryParse(id, out IdColumn))
			{
				IdColumn = 0;
			}
			if (String.IsNullOrWhiteSpace(last) || !Int32.TryParse(last, out LastNameColumn))
			{
				LastNameColumn = 1;
			}
			if (String.IsNullOrWhiteSpace(first) || !Int32.TryParse(first, out FirstNameColumn))
			{
				FirstNameColumn = 2;
			}
			if (String.IsNullOrWhiteSpace(face) || !Int32.TryParse(face, out FaceAmountColumn))
			{
				FaceAmountColumn = 11;
			}
			if (String.IsNullOrWhiteSpace(cash) || !Int32.TryParse(cash, out CashValueColumn))
			{
				CashValueColumn = 12;
			}
			if (String.IsNullOrWhiteSpace(risk) || !Decimal.TryParse(risk, out RiskThreshold))
			{
				RiskThreshold = 300000;
			}
			if (!String.IsNullOrWhiteSpace(sep))
			{
				Separator = sep;
			}
			if (!String.IsNullOrWhiteSpace(input))
			{
				InputFile = input;
			}
			if (!String.IsNullOrWhiteSpace(output))
			{
				OutputFile = output;
			}
			if (!File.Exists(InputFile))
			{
				throw new ArgumentException($"Input File {InputFile} does not exist.");
			}
		}

		public RiskAssessmentOptions(Boolean hasHeader, Decimal riskThreshold, Int32 idColumn, Int32 lastNameColumn, Int32 firstNameColumn, Int32 faceAmountColumn, Int32 cashValueColumn, String separator, String inputFile, String outputFile)
		{
			HasHeader = hasHeader;
			RiskThreshold = riskThreshold;
			IdColumn = idColumn;
			LastNameColumn = lastNameColumn;
			FirstNameColumn = firstNameColumn;
			FaceAmountColumn = faceAmountColumn;
			CashValueColumn = cashValueColumn;
			Separator = separator;
			InputFile = inputFile;
			OutputFile = outputFile;
		}
	}
}
