using System.Text;

namespace PE;

class Program
{
	static void Main()
	{
		Console.OutputEncoding = Encoding.Unicode;

		var textFormatter = new TextFormatter();

		//TextFormatter.Test();

		ConsoleHelper.PrintAllCharacters(20);

		Console.ReadKey();
	}
}
