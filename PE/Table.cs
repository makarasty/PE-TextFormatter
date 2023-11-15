using System.Text;
namespace PE;
public class GridColumn
{
	public string Name { get; set; }
	public int? Width { get; set; }
	public AlignmentType AlignHeader { get; set; }
	public AlignmentType AlignData { get; set; }
	public GridColumn(string name, int? width = null)
	{
		Name = name;
		Width = width;
	}
}
public class Table
{
	private readonly List<GridColumn> _columns = new();
	private readonly List<List<object>> _rows = new();
	public void AddColumn(GridColumn gridColumn)
	{
		_columns.Add(gridColumn);
	}
	public void AddRow(params object[] values)
	{
		_rows.Add(values.ToList());
	}
	public void SetCell(int col, int row, object value)
	{
		_rows[row][col] = value;
	}
	public object GetCell(int col, int row)
	{
		return _rows[row][col];
	}
	public void Render()
	{
		// Та сама крута логіка яка рахує довжину стовпця відповідно до самого довгого рядка в стовпці 
		var rowCount = _rows.Count;
		var columnCount = _columns.Count;
		var colWidths = new int[columnCount];

		for (int column = 0; column < columnCount; column++)
		{
			colWidths[column] = _columns[column].Name.Length;
			for (int row = 0; row < rowCount; row++)
			{
				var cell = _rows[row][column].ToString();
				colWidths[column] = Math.Max(colWidths[column], cell!.Length);
			}
			if (_columns[column].Width.HasValue)
			{
				colWidths[column] = _columns[column].Width!.Value;
			}
		}
		var header = new StringBuilder();
		var separator = new StringBuilder();
		var rows = new StringBuilder();
		var footer = new StringBuilder();

		header.Append('╔');
		separator.Append('╠');
		footer.Append('╚');

		for (int column = 0; column < columnCount; column++)
		{
			header.Append(new string('═', colWidths[column] + 2));
			separator.Append(new string('═', colWidths[column] + 2));
			footer.Append(new string('═', colWidths[column] + 2));

			if (column != columnCount - 1)
			{
				header.Append('╦');
				separator.Append('╬');
				footer.Append('╩');
			}
		}
		header.Append("╗\n");
		separator.Append("╣\n");
		footer.Append("╝\n");

		List<List<string>> columnHeaders = new();

		for (int column = 0; column < columnCount; column++)
		{
			GridColumn myPickedColumn = _columns[column];
			var formattedName = TextFormatter.Format(myPickedColumn.Name, colWidths[column], (opt) =>
			{
				opt.Alignment = myPickedColumn.AlignHeader;
				opt.Overflow = OverflowType.WrapWord;
			});
			columnHeaders.Add(formattedName.ToList());
		}

		int maxColumnLength = columnHeaders.Max(subList => subList.Count);
		header.EnsureCapacity(columnCount * maxColumnLength * 10);

		for (int t = 0; t < maxColumnLength; t++)
		{
			header.Append("║ ");
			for (int column = 0; column < columnCount; column++)
			{
				List<string> tColumn = columnHeaders[column];
				if (t < tColumn.Count)
				{
					header.Append(tColumn[t]).Append(" ║ ");
				}
				else
				{
					header.Append(' ').Append(new string(' ', colWidths[column])).Append("║ ");
				}
			}
			header.Append('\n');
		}

		int rowsCount = _rows.Count;
		for (int row = 0; row < rowsCount; row++)
		{
			List<List<string>> rowHeaders = new();
			var rowString = new StringBuilder();

			for (int column = 0; column < columnCount; column++)
			{
				GridColumn myPickedCell = _columns[column];
				object thatRowCol = _rows[row][column];
				var formattedCell = TextFormatter.Format(thatRowCol?.ToString() ?? "", colWidths[column], (opt) =>
				{
					opt.Alignment = myPickedCell.AlignData;
					opt.Overflow = OverflowType.WrapWord;
				});
				rowHeaders.Add(formattedCell.ToList());
			}

			int maxRowLength = rowHeaders.Max(subList => subList.Count);
			rowString.EnsureCapacity(columnCount * maxRowLength * 10);

			for (int t = 0; t < maxRowLength; t++)
			{
				rowString.Append("║ ");
				for (int column = 0; column < columnCount; column++)
				{
					List<string> tColumn = rowHeaders[column];
					if (t < tColumn.Count)
					{
						rowString.Append(tColumn[t]).Append(" ║ ");
					}
					else
					{
						rowString.Append(' ').Append(new string(' ', colWidths[column])).Append("║ ");
					}
				}
				rowString.Append('\n');
			}
			if (row != rowsCount - 1)
			{
				rowString.Append('╠');
				foreach (int width in colWidths)
				{
					rowString.Append(new string('-', width + 2)).Append('╬');
				}
				rowString[^1] = '╣';
				rowString.Append('\n');
			}
			rows.Append(rowString);
		}

		// Хехе а ось і сам рендер :D
		Console.Write(header);
		Console.Write(separator);
		Console.Write(rows);
		Console.Write(footer);
	}
}
/**/