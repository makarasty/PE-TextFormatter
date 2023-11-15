using System.Text;

namespace PE;

class Program
{
	static void Main()
	{
		Console.OutputEncoding = Encoding.Unicode;

		GridColumn column = new("My litle table")
		{
			AlignHeader = AlignmentType.Center,
			AlignData = AlignmentType.Center
		};

		OneColumnGrid table = new(column);

		table.AddRow("Nerf this");
		table.AddRow("vghfg,f dfdfdfhhfgd fghfghfgh dfdg df dfgdfg dfddgdf dfdgd dfg");
		table.AddRow(123);

		table.Render();

		Console.ReadKey();
	}
}