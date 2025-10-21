using WA.FP.Drawing;

namespace WA.FP.DrawingDemo
{
    // https://github.com/WebappAllenKuo/FP.Drawing.Module.git
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


            canvas = new Canvas(20, 20);
            context = new DrawingContext(canvas, new FPoint(0, 0));

            context = context
                .DrawPixel(new FPoint(0, 0), '*')
                .DrawLineWhile(Direction.Right, p => p.X < 5, '-') 
                .DrawLineWhile(Direction.Down, p => p.Y < 5, '|')
                ; 
            
            System.Console.WriteLine("範例3: DrawLineWhile");
            Console.WriteLine(context.Canvas.Render());


            canvas = new Canvas(20, 20);
            context = new DrawingContext(canvas, new FPoint(0, 0));

            context = context
                .DrawPixel(new FPoint(0, 0), '1')
                .MoveCursor(3, 3).DrawPixel('2')
                .MoveCursor(new FPoint(10, 10)).DrawPixel('3')
                ; 
            
            System.Console.WriteLine("範例4: 絕對位置,相對位置的Move");
            Console.WriteLine(context.Canvas.Render());


            canvas = new Canvas(20, 20);
            context = new DrawingContext(canvas, new FPoint(0, 0));

            context = context
                .DrawPixel(new FPoint(0, 0), 'O')
                .DrawLine(Direction.Right, 5, '*')
                .DrawLine(Direction.Down, 5, '*')
                .DrawLine(Direction.Left, 5, '-')
                .DrawLine(Direction.Up, 4, '|');
            
            System.Console.WriteLine("範例5: 應用範例,繪製一個方框");
            Console.WriteLine(context.Canvas.Render());


            canvas = new Canvas(40, 40);
            context = new DrawingContext(canvas, new FPoint(0, 0));

            context = context
                .DrawRectangle(40, 40, '#')
                .MoveCursor(5, 5)
                .DrawRectangle(30,30, '*')
                .MoveCursor(5, 5)
                .DrawRectangle(20,20, '*')
                ;
            
            System.Console.WriteLine("範例6: 應用範例,繪製三層方框");
            Console.WriteLine(context.Canvas.Render());
        }
    }
}
