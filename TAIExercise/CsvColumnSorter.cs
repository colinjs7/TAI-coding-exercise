/*  Inspiration for this class came from https://josef.codes/sorting-large-csv-files-by-column-using-csharp/
 * 
 *  The multiple-column portion was my own addition
 */

namespace TAIExercise
{
	class CsvAmountAndIdColumnSorter : IComparer<String>
	{
		private readonly String _separator;
		private readonly Int32 _amtColumn;
		private readonly Int32 _idColumn;
		private readonly Boolean _sortAmtDesc;

		public CsvAmountAndIdColumnSorter(Int32 amountColumn, Int32 idColumn, Boolean sortAmtDesc = true, String separator = ",")
		{
			_separator = separator;
			_amtColumn = amountColumn;
			_idColumn = idColumn;
			_sortAmtDesc = sortAmtDesc;
		}

		public Int32 Compare(String? x, String? y)
		{
			Int32 ret = 0;

			if (x == null && y != null)
			{
				ret = -1;
			}
			else if (y == null && x != null)
			{
				ret = 1;
			}
			else if (x == null && y == null)
			{
				ret = 0;
			}
			else
			{
				String[] xFields = x.Split(_separator);
				String[] yFields = y.Split(_separator);

				Decimal.TryParse(xFields[_amtColumn], out Decimal xAmt);
				Decimal.TryParse(yFields[_amtColumn], out Decimal yAmt);
				
				if (xAmt > yAmt)
				{
					ret = _sortAmtDesc ? -1 : 1;
				}
				else if (xAmt < yAmt)
				{
					ret = _sortAmtDesc ? 1 : -1;
				}
				else
				{
					ret = Comparer<String>.Default.Compare(xFields[_idColumn], yFields[_idColumn]);
				}
			}
			return ret;
		}
	}
}
