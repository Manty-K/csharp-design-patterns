
interface Shape{ 
    double GetArea();
}

public class Rectangle : Shape{
   public double Height {get;set;}
   public double Wight {get;set; }

   double GetArea(){
     return Height * Width;
   }
}
public class Circle : Shape{
    public double Radius {get;set;}

    double GetArea(){
      return Math.PI * Radius * Radius;
   }

}


public class AreaCalculator {
 public double TotalArea(Shape[] arrObjects)
   {
      double area = 0;

      foreach(Shape obj in arrObjects)
      {
        area += obj.GetArea();
      }
      return area;
   }
}





