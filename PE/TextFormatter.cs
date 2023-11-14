using System.Text;

namespace PE;

public enum AlignmentType
{
	Left,
	Right,
	Center
}
public class FormatOptions
{
	public int Width { get; set; }
	public OverflowType Overflow { get; set; }
	public AlignmentType Alignment { get; set; }

	public FormatOptions(int width, AlignmentType alignment = AlignmentType.Left, OverflowType overflow = OverflowType.Hide)
	{
		if (width <= 0) throw new ArgumentException("Invalid width");
		this.Overflow = overflow;
		this.Alignment = alignment;
		this.Width = width;
	}
}
public enum OverflowType
{
	Hide,
	WrapWord
}
public class TextFormatter
{
	public static void Test()
	{
		var aText = Align("Nerf This", 30, AlignmentType.Left);
		System.Console.WriteLine("|" + aText + "|" + aText.Length);

		aText = Align("Nerf This", 30, AlignmentType.Center);
		System.Console.WriteLine("|" + aText + "|" + aText.Length);

		aText = Align("Nerf This", 30, AlignmentType.Right);
		System.Console.WriteLine("|" + aText + "|" + aText.Length);

		System.Console.WriteLine("|" + Truncate("Nerf This", 4 + 3) + "|");

		System.Console.WriteLine(new string('-', 20));

		var bText = WrapWord("Nerf This, I just pressing qui", 8);
		System.Console.WriteLine(string.Join('\n', bText));

		System.Console.WriteLine(new string('-', 20));

		bText = Format("Nerf This, I just pressing qui", new FormatOptions(15));
		System.Console.WriteLine(string.Join('\n', bText));

		System.Console.WriteLine(new string('-', 20));

		bText = Format("Nerf This, I just pressing qui", 8, (opt) =>
		{
			opt.Alignment = AlignmentType.Center;
			opt.Overflow = OverflowType.WrapWord;
		});
		System.Console.WriteLine(string.Join('\n', bText));

		System.Console.WriteLine(new string('-', 20));

		bText = Format("Nerf This, I just pressing qui", 8, (opt) =>
		{
			opt.Alignment = AlignmentType.Right;
			opt.Overflow = OverflowType.WrapWord;
		});
		System.Console.WriteLine(string.Join('\n', bText));
	}

	public static IEnumerable<string> Format(string text, int width)
	{
		return Format(text, new FormatOptions(width));
	}
	public static IEnumerable<string> Format(string text, int width, Action<FormatOptions> options)
	{
		var formatOptions = new FormatOptions(width);
		options(formatOptions);
		return Format(text, formatOptions);
	}
	public static IEnumerable<string> Format(string text, FormatOptions options)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new ArgumentException("Invalid text");
		}

		if (options.Overflow == OverflowType.Hide)
		{
			return new[] { Truncate(text, options.Width) };
		}

		return WrapWord(text, options.Width)
			.Select(line => Align(line, options.Width, options.Alignment))
			.ToArray();
	}
	private static string Align(string text, int width, AlignmentType alignment)
	{
		if (text.Length >= width) return text;

		switch (alignment)
		{
			case AlignmentType.Left:
				return text.PadRight(width);
			case AlignmentType.Right:
				return text.PadLeft(width);
			case AlignmentType.Center:
				int padLeft = (width - text.Length) / 2;
				return text.PadLeft(padLeft + text.Length).PadRight(width);
			default:
				throw new ArgumentException("Invalid alignment");
		}
	}
	private static string Truncate(string text, int width)
	{
		//? Чомусь vscode запропонувала поміняти Substring на AsSpan
		return text.Length <= width ? text : string.Concat(text.AsSpan(0, width - 3), "...");
	}
	private static IEnumerable<string> WrapWord(string text, int width)
	{
		List<string> result = new();
		StringBuilder currentLine = new();

		foreach (string word in text.Split(' '))
		{
			if (word.Length > width)
			{
				throw new FormatException("Text is too long");
			}

			if (currentLine.Length + word.Length + 1 > width)
			{
				result.Add(currentLine.ToString().TrimStart());
				currentLine.Clear();
			}

			currentLine.Append(' ').Append(word);
		}

		if (currentLine.Length > 0)
		{
			result.Add(currentLine.ToString().TrimStart());
		}

		return result;
	}
}