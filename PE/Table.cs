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
		if (values.Length != _columns.Count) throw new ArgumentException("Неправильна кількість стовпців >w<");

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
		var colWidths = new int[_columns.Count];
		for (int col = 0; col < _columns.Count; col++)
		{
			colWidths[col] = _columns[col].Name.Length;
			foreach (var row in _rows)
			{
				var cell = row[col]!.ToString();
				if (cell!.Length > colWidths[col])
					colWidths[col] = cell.Length;
			}

			if (_columns[col].Width.HasValue)
			{
				colWidths[col] = _columns[col].Width!.Value;
			}
		}

		var header = new StringBuilder();
		header.Append('╔');
		for (int column = 0; column < _columns.Count; column++)
		{
			header.Append(new string('═', colWidths[column] + 2));
			if (column != _columns.Count - 1)
			{
				header.Append('╦');
			}
		}
		header.Append("╗\n");

		for (int thatColumn = 0; thatColumn < _columns.Count; thatColumn++)
		{
			GridColumn myPickedColumn = _columns[thatColumn];
			var formattedName = TextFormatter.Format(myPickedColumn.Name, colWidths[thatColumn], (opt) =>
			{
				opt.Alignment = myPickedColumn.AlignHeader;
				opt.Overflow = OverflowType.WrapWord;
			});
			header.Append("║ ").Append(formattedName.First()).Append(' ');
		}
		header.Append("║\n");

		var separator = new StringBuilder();
		separator.Append('╠');
		for (int column = 0; column < _columns.Count; column++)
		{
			separator.Append(new string('═', colWidths[column] + 2));
			if (column != _columns.Count - 1)
			{
				separator.Append('╬');
			}
		}
		separator.Append("╣\n");

		var rows = new StringBuilder();

		int rowsCount = _rows.Count;

		for (int row = 0; row < rowsCount; row++)
		{
			var rowString = new StringBuilder();
			for (int column = 0; column < _columns.Count; column++)
			{
				GridColumn myPickedCell = _columns[column];
				object thatRowCol = _rows[row][column];

				var formattedCell = TextFormatter.Format(thatRowCol?.ToString() ?? "", colWidths[column], (opt) =>
				{
					opt.Alignment = myPickedCell.AlignData;
					opt.Overflow = OverflowType.WrapWord;
				});
				rowString.Append("║ ").Append(formattedCell.First()).Append(' ');
			}
			rowString.Append("║\n");

			if (row != rowsCount - 1)
			{
				rowString.Append('╠');

				foreach (int width in colWidths)
				{
					rowString.Append(new string('-', width + 2)).Append('╬');
				}
				// ^ починає індекс з кінця як .at(-1) в NodeJS 16+
				rowString[^1] = '╣';

				rowString.Append('\n');
			}

			rows.Append(rowString);
		}

		var footer = new StringBuilder();
		footer.Append('╚');
		for (int column = 0; column < _columns.Count; column++)
		{
			footer.Append(new string('═', colWidths[column] + 2));
			if (column != _columns.Count - 1)
			{
				footer.Append('╩');
			}
		}
		footer.Append('╝');

		// Хехе а ось і сам рендер :D
		Console.Write(header);
		Console.Write(separator);
		Console.Write(rows);
		Console.Write(footer);
	}
}
/**/