using System.Text;

namespace PE;

class Program
{
	static void Main()
	{
		Console.OutputEncoding = Encoding.Unicode;

		GridColumn column = new("My litle table", 20)
		{
			AlignHeader = AlignmentType.Center,
			AlignData = AlignmentType.Left
		};

		OneColumnGrid table = new(column);

		table.AddRow("Nerf this");
		table.AddRow("Warframe");
		table.AddRow(123);

		table.Render();

		Console.ReadKey();
	}
}