public interface ICoffee:
    string GetDescription();
    double GetCost();



public class PlainCoffee : ICoffee{
    string GetDescription()=> "Plain Coffee";
    double GetCost() => 1.0;
}

public abstract class CoffeeDecorator : ICoffee{
    protected ICoffee _coffee;
    public CoffeeDecorator(ICoffee coffee){
        _coffee = coffee;
    }
    public abstract string GetDescription();
    public abstract double GetCost();

}

public class MilkDecorator : CoffeeDecorator{
    public MilkDecorator(ICoffee coffee) : base(coffee){}
    public override string GetDescription() => _coffee.GetDescription() + " + Milk";
    public override double GetCost() => _coffee.GetCost() + 10;
}

public class ChocoDecorator : CoffeeDecorator{
    public ChocoDecorator(ICoffee coffee) : base(coffee){}
    public override string GetDescription() => _coffee.GetDescription() + " + Choco";
    public override double GetCost() => _coffee.GetCost() + 15;
}

// Usage — wrap like layers
ICoffee order = new PlainCoffee();
order = new MilkDecorator(order);
order = new SugarDecorator(order);
order = new MilkDecorator(order); // extra milk!

Console.WriteLine(order.GetDescription()); 
// Plain Coffee + Milk + Sugar + Milk
Console.WriteLine($"₹{order.GetCost()}"); 
// ₹55