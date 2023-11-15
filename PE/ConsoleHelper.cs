using System.Text;

namespace PE;

class ConsoleHelper
{
	public static void PrintAllCharacters(int charsPerLine)
	{
		for (int i = char.MinValue; i <= char.MaxValue; i++)
		{
			char character = (char)i;
			string charString = character.ToString();
			string charCodeString = i.ToString("0000");
	
			Console.Write($"{charString} ({charCodeString})");

			if (i % charsPerLine == 0)
			{
				Console.WriteLine();
			}
		}
	}
}