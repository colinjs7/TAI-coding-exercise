using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIExercise
{
	internal class RiskAssessmentOptions
	{
		internal readonly Boolean HasHeader;
		internal readonly Decimal RiskThreshold = 300000;
		internal readonly Int32 IdColumn = 0;
		internal readonly Int32 LastNameColumn = 1;
		internal readonly Int32 FirstNameColumn = 2;
		internal readonly Int32 FaceAmountColumn = 11;
		internal readonly Int32 CashValueColumn = 12;
		internal readonly String Separator = ",";
		internal readonly String InputFile = String.Empty;
		internal readonly String OutputFile = String.Empty;

		public RiskAssessmentOptions()
		{
			Boolean.TryParse(ConfigurationManager.AppSettings["hasHeader"], out HasHeader);
			Int32.TryParse(ConfigurationManager.AppSettings["idColumn"], out IdColumn);
			Int32.TryParse(ConfigurationManager.AppSettings["lastNameColumn"], out LastNameColumn);
			Int32.TryParse(ConfigurationManager.AppSettings["firstNameColumn"], out FirstNameColumn);
			Int32.TryParse(ConfigurationManager.AppSettings["faceAmountColumn"], out FaceAmountColumn);
			Int32.TryParse(ConfigurationManager.AppSettings["cashValueColumn"], out CashValueColumn);
			Decimal.TryParse(ConfigurationManager.AppSettings["riskThreshold"], out RiskThreshold);
			Separator = ConfigurationManager.AppSettings["separator"];
			InputFile = ConfigurationManager.AppSettings["inputFile"];
			OutputFile = ConfigurationManager.AppSettings["outputFile"];
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
