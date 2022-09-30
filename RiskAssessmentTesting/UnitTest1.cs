using TAIExercise;

namespace RiskAssessmentTesting
{
	public class UnitTest1
	{
		[Fact]
		public async void RiskAssessmentNoOptions()
		{
			RiskAssessment ra = new RiskAssessment();
			await ra.RunAssessment();
			Assert.True(File.Exists("C:/Temp/RiskAssessment.csv"));
		}

		[Fact]
		public async void RiskAssessmentDefaultOptions()
		{
			RiskAssessment ra = new RiskAssessment(new RiskAssessmentOptions());
			await ra.RunAssessment();
			Assert.True(File.Exists("C:/Temp/RiskAssessment.csv"));
		}

		[Fact]
		public async void RiskAssessmentCustomOptions()
		{
			Boolean hasHeader = true;
			Decimal riskThreshold = 300000;
			Int32 idColumn = 0;
			Int32 lastNameColumn = 1;
			Int32 firstNameColumn = 2;
			Int32 faceAmountColumn = 11;
			Int32 cashValueColumn = 12;
			String separator = ",";
			String inputFile = "C:/Temp/ClientCoveragePolicy.csv";
			String outputFile = "C:/Temp/RiskAssessment.csv";

			RiskAssessmentOptions options = new RiskAssessmentOptions(hasHeader, riskThreshold, idColumn,
				lastNameColumn, firstNameColumn, faceAmountColumn, cashValueColumn, separator, inputFile, outputFile);
			RiskAssessment ra = new RiskAssessment(options);
			await ra.RunAssessment();
			Assert.True(File.Exists(outputFile));
		}

		[Fact]
		public async void RiskAssessmentBadInputFile()
		{
			Boolean hasHeader = true;
			Decimal riskThreshold = 300000;
			Int32 idColumn = 0;
			Int32 lastNameColumn = 1;
			Int32 firstNameColumn = 2;
			Int32 faceAmountColumn = 11;
			Int32 cashValueColumn = 12;
			String separator = ",";
			String inputFile = "C:/Temp/FileDoesntExist.csv";
			String outputFile = "C:/Temp/RiskAssessment.csv";

			RiskAssessmentOptions options = new RiskAssessmentOptions(hasHeader, riskThreshold, idColumn,
				lastNameColumn, firstNameColumn, faceAmountColumn, cashValueColumn, separator, inputFile, outputFile);
			RiskAssessment ra = new RiskAssessment(options);
			await Assert.ThrowsAsync<ArgumentException>(() => ra.RunAssessment());
		}

		[Fact]
		public async void RiskAssessmentHeaderMarkedFalse()
		{
			//This will throw an exception because the file actually has a header
			Boolean hasHeader = false;
			Decimal riskThreshold = 300000;
			Int32 idColumn = 0;
			Int32 lastNameColumn = 1;
			Int32 firstNameColumn = 2;
			Int32 faceAmountColumn = 11;
			Int32 cashValueColumn = 12;
			String separator = ",";
			String inputFile = "C:/Temp/ClientCoveragePolicy.csv";
			String outputFile = "C:/Temp/RiskAssessment.csv";

			RiskAssessmentOptions options = new RiskAssessmentOptions(hasHeader, riskThreshold, idColumn,
				lastNameColumn, firstNameColumn, faceAmountColumn, cashValueColumn, separator, inputFile, outputFile);
			RiskAssessment ra = new RiskAssessment(options);
			await Assert.ThrowsAsync<IndexOutOfRangeException>(() => ra.RunAssessment());
		}
	}
}