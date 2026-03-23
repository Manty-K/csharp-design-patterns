

namespace VariableScope
{

    public interface IShape
    {
        void Accept(IShapeVisitor visitor);
    }

    public interface IShapeVisitor
    {
        void Visit(Circle circle);
        void Visit(Rectangle rectangle);
    }

    public class Circle : IShape
    {
        public double Radius { get; }
        public Circle(double radius)
        {
            Radius = radius;
        }
        public void Accept(IShapeVisitor visitor) => visitor.Visit(this);

    }

    public class Rectangle : IShape
    {
        public double Length { get; }
        public double Breadth { get; }

        public Rectangle(double length, double breadth)
        {
            Length = length;
            Breadth = breadth;
        }
        public void Accept(IShapeVisitor visitor) => visitor.Visit(this);

    }


    public class AreaCalculator : IShapeVisitor
    {
        public void Visit(Circle circle)
        {
            Console.WriteLine($"Area Circle: {Math.PI * circle.Radius * circle.Radius} ");
        }

        public void Visit(Rectangle rectangle)
        {
            Console.WriteLine($"Area Rectangle : {rectangle.Length * rectangle.Breadth} ");
        }
    }

    public class PerimeterCalculator : IShapeVisitor
    {
        public void Visit(Circle circle)
        {
            Console.WriteLine($"Perimeter Circle : {Math.PI * 2 * circle.Radius} ");
        }

        public void Visit(Rectangle rectangle)
        {
            Console.WriteLine($"Perimeter Rectangle : {2 * rectangle.Length + 2 * rectangle.Breadth} ");
        }
    }

    public class Program
    {
        public static void Main()
        {


            var circle = new Circle(5);
            var rectangle = new Rectangle(4, 6);

            circle.Accept(new AreaCalculator());
            rectangle.Accept(new AreaCalculator());

            circle.Accept(new PerimeterCalculator());
            rectangle.Accept(new PerimeterCalculator());


        }
    }
}