using WA.FP.Drawing;

namespace WA.FP.DrawingDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var canvas = new Canvas(20, 20);
            var context = new DrawingContext(canvas, new FPoint(0, 0));

            context = context
                .DrawPixel(new FPoint(0, 5), '*')
                .DrawLine(Direction.Right, 5, '-') // 繪製直線,當下的點不會被繪製,從下一個點開始繪製
                .DrawLine(Direction.Down, 5, '|'); // 繪製直線,當下的點不會被繪製,從下一個點開始繪製
            
            System.Console.WriteLine("範例1: 繪製單一像素,垂直水平直線");
            Console.WriteLine(context.Canvas.Render());
            
            canvas = new Canvas(25, 25);
            context = new DrawingContext(canvas, new FPoint(0, 0));

            context = context
                .DrawPixel(new FPoint(0, 10), '-')
                .DrawLine(Direction.UpRight, 5, '-') // 繪製右上斜線
                .DrawLine(Direction.DownRight, 5, '|') // 繪製右下斜線
                ; 
            
            System.Console.WriteLine("範例2: 繪製右上,左下直線");
            Console.WriteLine(context.Canvas.Render());
        }
    }
}
