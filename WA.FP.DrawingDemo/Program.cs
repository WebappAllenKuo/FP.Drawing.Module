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


			canvas = new Canvas(40, 40);
			context = new DrawingContext(canvas, new FPoint(0, 20));

			context = context
				.DrawLine(Direction.UpRight, 15, '*')
                .DrawLine(Direction.DownRight, 15, '*')
                .DrawLine(Direction.DownLeft, 15, '*')
                .DrawLine(Direction.UpLeft, 15, '*')
                .MoveCursor(5,0)
				.DrawLine(Direction.UpRight, 10, '*')
				.DrawLine(Direction.DownRight, 10, '*')
				.DrawLine(Direction.DownLeft, 10, '*')
				.DrawLine(Direction.UpLeft, 10, '*')
				.MoveCursor(5, 0)
				.DrawLine(Direction.UpRight, 5, '*')
				.DrawLine(Direction.DownRight, 5, '*')
				.DrawLine(Direction.DownLeft, 5, '*')
				.DrawLine(Direction.UpLeft, 5, '*')

				.MoveCursor(5, 0).DrawPixel('*')
				;

			System.Console.WriteLine("範例7: 應用範例,繪製三層菱形");
			Console.WriteLine(context.Canvas.Render());


			canvas = new Canvas(40, 40);
			context = new DrawingContext(canvas, new FPoint(0, 0));

			context = context
				.DrawRectangle(40,40,'.') // 外框
                .MoveCursor(2,2)
				.DrawRectangle(16,36,'*') // 左框
                .MoveCursor(new FPoint(20,2)).Draw靠左正向三角形(18,'*')
				.MoveCursor(new FPoint(21, 21)).Draw靠左倒向三角形(17, '*')
				;

			System.Console.WriteLine("範例8: 應用範例,繪製巢狀圖形");
			Console.WriteLine(context.Canvas.Render());
		}
    }
}
