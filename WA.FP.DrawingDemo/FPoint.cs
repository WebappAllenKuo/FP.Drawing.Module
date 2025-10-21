namespace WA.FP.Drawing;
public record FPoint(int X, int Y)
{
    public FPoint Move(int deltaX, int deltaY) => this with { X = X + deltaX, Y = Y + deltaY };
}