using System.Collections.Immutable;

namespace WA.FP.Drawing;

public record FPoint(int X, int Y)
{
    public FPoint Move(int deltaX, int deltaY) => this with { X = X + deltaX, Y = Y + deltaY };
}

public record Canvas(int Width, int Height, ImmutableDictionary<FPoint, char> Pixels)
{
    public Canvas(int width, int height) : this(width, height, ImmutableDictionary<FPoint, char>.Empty)
    {
    }

    /// <summary>
    /// 繪製單一像素
    /// </summary>
    /// <param name="point"></param>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public Canvas DrawPixel(FPoint point, char symbol)
        => this with { Pixels = Pixels.SetItem(point, symbol) };

    public char GetPixel(FPoint point)
        => Pixels.TryGetValue(point, out var symbol) ? symbol : ' ';

    public string Render()
    {
        var lines = new List<string>();
        for (int y = 0; y < Height; y++)
        {
            var lineChars = new char[Width];
            for (int x = 0; x < Width; x++)
            {
                lineChars[x] = GetPixel(new FPoint(x, y));
            }
            lines.Add(new string(lineChars));
        }
        return string.Join('\n', lines);
    }
}