using System;
using System.Text;

namespace PE;

public enum AlignmentType
{
	AlignLeft,
	AlignRight,
	AlignCenter
}
public class TextFormatter
{
	public static void Test()
	{
		var aText = Align("Nerf This", 30, AlignmentType.AlignLeft);
		System.Console.WriteLine("|" + aText + "|" + aText.Length);

		aText = Align("Nerf This", 30, AlignmentType.AlignCenter);
		System.Console.WriteLine("|" + aText + "|" + aText.Length);

		aText = Align("Nerf This", 30, AlignmentType.AlignRight);
		System.Console.WriteLine("|" + aText + "|" + aText.Length);

		System.Console.WriteLine("|" + Truncate("Nerf This", 4 + 3) + "|");

		System.Console.WriteLine(WrapWord("Nerf This", 3));
	}

	public static string Align(string text, int width, AlignmentType alignment)
	{
		if (text.Length >= width)
		{
			return text;
		}

		switch (alignment)
		{
			case AlignmentType.AlignLeft:
				return text.PadRight(width);
			case AlignmentType.AlignRight:
				return text.PadLeft(width);
			case AlignmentType.AlignCenter:
				return text.PadLeft(((width - text.Length) / 2) + text.Length).PadRight(width);
			default:
				throw new ArgumentException("Invalid alignment");
		}
	}
	public static string Truncate(string text, int width)
	{
		//? Чомусь vscode запропонувала поміняти Substring на AsSpan
		return text.Length <= width ? text : string.Concat(text.AsSpan(0, width - 3), "...");
	}
	public static string[] WrapWord(string text, int width)
	{
		List<string> result = new List<string>();
		StringBuilder currentLine = new StringBuilder();

		for (int index = 0; index < text.Length; index++)
		{
			char character = text[index];

			if (character == ' ')
			{
				result.Add(currentLine.ToString().Trim());
				currentLine.Clear();
			}
			else
			{
				currentLine.Append(character);

				if (currentLine.Length >= width)
				{
					result.Add(currentLine.ToString().Trim());
					currentLine.Clear();
				}
			}
		}

		if (currentLine.Length > 0)
		{
			result.Add(currentLine.ToString().Trim());
		}

		return result.ToArray();
	}

}
