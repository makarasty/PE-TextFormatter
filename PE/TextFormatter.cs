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
		System.Console.WriteLine("|" + Align("Nerf This", 30, AlignmentType.AlignLeft) + "|");

		System.Console.WriteLine("|" + Align("Nerf This", 30, AlignmentType.AlignCenter) + "|");

		System.Console.WriteLine("|" + Align("Nerf This", 30, AlignmentType.AlignRight) + "|");

		System.Console.WriteLine("|" + Truncate("Nerf This", 4 + 3) + "|");
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
	public static string Truncate(string text, int width)
	{
		return text.Length <= width ? text : text.Substring(0, width - 3) + "...";
	}
}
