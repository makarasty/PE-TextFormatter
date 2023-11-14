namespace PE;

public class GridCell
{
	public object Value { get; set; } = new object();
}

public class GridColumn
{
	public string Name { get; set; }
	public int Width { get; private set; }
	public AlignmentType AlignHeader { get; set; }
	public AlignmentType AlignData { get; set; }

	public GridColumn(string name, int width)
	{
		Name = name;
		Width = width;
	}
}

public class OneColumnGrid
{
	private readonly GridColumn _column;
	private readonly List<GridCell> _rows;

	public OneColumnGrid(GridColumn column)
	{
		_column = column;
		_rows = new List<GridCell>();
	}

	public void AddRow(object value)
	{
		_rows.Add(new GridCell { Value = value });
	}

	public void Render()
	{
		IEnumerable<string> header = TextFormatter.Format(_column.Name, _column.Width, options =>
		{
			options.Alignment = _column.AlignHeader;
			options.Overflow = OverflowType.WrapWord;
		});

		string horizontalLine = new('═', _column.Width);
		char verticalLine = '║';

		Console.WriteLine($"╔{horizontalLine}╗");
		Console.WriteLine($"{verticalLine}{header.First()}{verticalLine}");
		Console.WriteLine($"╠{horizontalLine}╣");

		foreach (var row in _rows)
		{
			var formattedRow = TextFormatter.Format(row.Value.ToString() ?? "", _column.Width, options =>
			{
				options.Alignment = _column.AlignData;
				options.Overflow = OverflowType.WrapWord;
			});

			foreach (var line in formattedRow)
			{
				Console.WriteLine($"{verticalLine}{line}{verticalLine}");
			}
		}

		Console.WriteLine($"╚{horizontalLine}╝");
	}
}