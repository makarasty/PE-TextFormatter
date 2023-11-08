using System.Text;

namespace PE;

class ConsoleHelper
{
	public static void PrintAllCharacters(int charsPerLine)
	{
		for (int i = 0; i <= char.MaxValue; i++)
		{
			char character = (char)i;
			string charString = character.ToString();
	
			Console.Write(charString);
	
			if (i < char.MaxValue)
			{
				Console.Write(' ');
			}
	
			if (i > 0 && i % charsPerLine == 0)
			{
				Console.WriteLine();
			}
		}
	}
}