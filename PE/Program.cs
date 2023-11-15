using System.Text;
namespace PE;
class Program
{
	static void Main(string[] args)
	{
		Console.OutputEncoding = Encoding.Unicode;
		var table = new Table();

		table.AddColumn(new GridColumn("Type", 10)
		{
			AlignHeader = AlignmentType.Right,
			AlignData = AlignmentType.Center
		});

		table.AddColumn(new GridColumn("Name")
		{
			AlignHeader = AlignmentType.Left,
			AlignData = AlignmentType.Right
		});

		table.AddColumn(new GridColumn("Imbalance?")
		{
			AlignHeader = AlignmentType.Left,
			AlignData = AlignmentType.Center
		});

		table.AddRow("tank", "D.va", "Yes");
		table.AddRow("dps", "Echo", "Yes");
		table.AddRow("healer", "Mercy", "Nerf this!");

		table.Render();

		System.Console.WriteLine("");

		table.AddRow("healer", "Lucio", "Useless");

		table.SetCell(2, 2, "Make this better!");

		table.Render();

		Console.ReadKey();
	}
}