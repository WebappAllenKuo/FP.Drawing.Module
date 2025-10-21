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

public enum Direction
{
    Up, Down, Left, Right,
    UpRight, UpLeft, DownRight, DownLeft
}

public record DrawingContext(Canvas Canvas, FPoint Cursor)
{
    public DrawingContext DrawPixel(FPoint point, char symbol)
        => DrawingTool.DrawPixel(Canvas, point, symbol);

    public DrawingContext DrawPixel(char symbol)
        => DrawingTool.DrawPixel(Canvas, Cursor, symbol);

    public DrawingContext DrawLine(Direction direction, int length, char symbol)
        => DrawingTool.DrawLine(Canvas, Cursor, direction, length, symbol);

    public DrawingContext DrawLineWhile(Direction direction, Func<FPoint, bool> condition, char symbol)
        => DrawingTool.DrawLineWhile(Canvas, Cursor, direction, condition, symbol);

    public DrawingContext MoveCursor(FPoint newCursor)
        => this with { Cursor = newCursor };

    public DrawingContext MoveCursor(int deltaX, int deltaY)
        => this with { Cursor = Cursor.Move(deltaX, deltaY) };
        
    public DrawingContext DrawRectangle(int width, int height, char symbol)
    {
        var context = this.DrawPixel(symbol);
        // 上邊
        context = context.DrawLine(Direction.Right, width - 1, symbol);
        // 右邊
        context = context.DrawLine(Direction.Down, height - 1, symbol);
        // 下邊
        context = context.DrawLine(Direction.Left, width - 1, symbol);
        // 左邊
        context = context.DrawLine(Direction.Up, height - 2, symbol);
        return context;
    }
}

public static class DrawingTool
{
    private static ImmutableDictionary<Direction, (int deltaX, int deltaY)> DirectionDeltas { get; } =
        new Dictionary<Direction, (int deltaX, int deltaY)>
        {
            { Direction.Up, (0, -1) },
            { Direction.Down, (0, 1) },
            { Direction.Left, (-1, 0) },
            { Direction.Right, (1, 0) },
            { Direction.UpRight, (1, -1) },
            { Direction.UpLeft, (-1, -1) },
            { Direction.DownRight, (1, 1) },
            { Direction.DownLeft, (-1, 1) },
        }.ToImmutableDictionary();

    public static DrawingContext DrawPixel(Canvas canvas, FPoint point, char symbol)
        => new DrawingContext(canvas.DrawPixel(point, symbol), point);

    /// <summary>
    /// 繪製直線,當下的點不會被繪製,從下一個點開始繪製
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="start"></param>
    /// <param name="direction"></param>
    /// <param name="length"></param>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public static DrawingContext DrawLine(Canvas canvas, FPoint start, Direction direction, int length, char symbol)
    {
        var currentCanvas = canvas;
        var currentPoint = start;

        var (deltaX, deltaY) = DirectionDeltas[direction];

        for (int i = 0; i < length; i++)
        {
            currentPoint = currentPoint.Move(deltaX, deltaY);
            currentCanvas = currentCanvas.DrawPixel(currentPoint, symbol);
        }

        return new DrawingContext(currentCanvas, currentPoint);
    }

    public static DrawingContext DrawLineWhile(Canvas canvas, FPoint start, Direction direction, Func<FPoint, bool> condition, char symbol)
    {
        var currentCanvas = canvas;
        var currentPoint = start;

        var (deltaX, deltaY) = DirectionDeltas[direction];

        while (true)
        {
            currentPoint = currentPoint.Move(deltaX, deltaY);
            if (!condition(currentPoint))
            {
                break;
            }
            currentCanvas = currentCanvas.DrawPixel(currentPoint, symbol);
        }

        return new DrawingContext(currentCanvas, currentPoint);
    }
}