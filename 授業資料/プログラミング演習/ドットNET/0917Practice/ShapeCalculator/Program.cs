namespace ShapeCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //四角形クラス
            Square square = new Square(840f, 680f);
            square.GetArea();

            //三角形クラス
            Triangle triangle = new Triangle(480f, 360f);
            triangle.GetArea();
        }
    }
}
