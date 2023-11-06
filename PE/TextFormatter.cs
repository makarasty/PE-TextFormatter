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
		System.Console.WriteLine("|" + Align("Net", 20, AlignmentType.AlignLeft) + "|");

		System.Console.WriteLine("|" + Align("Net", 20, AlignmentType.AlignCenter) + "|");

		System.Console.WriteLine("|" + Align("Net", 20, AlignmentType.AlignRight) + "|");
	}

	public static string Align(string text, int width, AlignmentType alignment)
	{
		if (text.Length >= width)
		{
			return text;
		}
		int spacesToAdd = width - text.Length;

		switch (alignment)
		{
			case AlignmentType.AlignLeft:
				return text.PadRight(spacesToAdd);
			case AlignmentType.AlignRight:
				return text.PadLeft(spacesToAdd);
			case AlignmentType.AlignCenter:
				return text.PadLeft(width / 2).PadRight(spacesToAdd);
			default:
				throw new ArgumentException("Invalid alignment");
		}
	}
	public static string Truncate(string text, int width){
		string newText = text;
		if (text.Length > width)
		text.Substring(7, 5);
		return text;
	}
}
