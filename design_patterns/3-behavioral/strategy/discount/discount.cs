public interface IDiscountStrategy
{
    double Calculate(double originalPrice);
}

public class NoDiscount : IDiscountStrategy
{
    public double Calculate(double originalPrice) => originalPrice;
}

public class PercentageDiscount : IDiscountStrategy
{
    private readonly double Percentage;

    public PercentageDiscount(double percent)
    {
        if (percent < 0 || percent > 100)
            throw new Exception("Percentage not in range");
        Percentage = percent;
    }

    public double Calculate(double originalPrice)
    {
        double normalized = Percentage / 100;
        double amountToDeduct = normalized * originalPrice;
        return originalPrice - amountToDeduct;
    }
}

class FlatDiscount : IDiscountStrategy
{
    private readonly double Price;

    public FlatDiscount(double price)
    {
        if (price < 0)
            throw new Exception("Price cannot be negative");
        Price = price;
    }

    public double Calculate(double originalPrice)
    {
        if (Price > originalPrice)
            throw new Exception("Price cannot be more than original price");
        return originalPrice - Price;
    }
}

public class PriceCalculator
{
    private IDiscountStrategy DiscountStrategy;

    public PriceCalculator(IDiscountStrategy discountStrategy)
    {
        DiscountStrategy = discountStrategy;
    }

    public void SetDiscountStrategy(IDiscountStrategy discountStrategy)
    {
        DiscountStrategy = discountStrategy;
    }

    public double Calculate(double price) => DiscountStrategy.Calculate(price);
}

public class Program
{
    public static void Main()
    {
        PriceCalculator calculator = new(new PercentageDiscount(50));

        Console.WriteLine(calculator.Calculate(123));  // 61.5

        calculator.SetDiscountStrategy(new FlatDiscount(20));

        Console.WriteLine(calculator.Calculate(123));  // 103
    }
}
