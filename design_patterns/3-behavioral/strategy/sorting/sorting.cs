public interface ISortStrategy
{
    void Sort(List<int> data);
}

public class BubbleSort : ISortStrategy
{
    public void Sort(List<int> data) => Console.WriteLine("Bubble Sorting");
}

public class MergeSort : ISortStrategy
{
    public void Sort(List<int> data) => Console.WriteLine("Merge Sorting");
}

public class QuickSort : ISortStrategy
{
    public void Sort(List<int> data) => Console.WriteLine("Quick Sorting");
}

public class Sorter
{
    public List<int> Numbers { get; set; }
    private ISortStrategy Strategy;

    public Sorter(List<int> numbers, ISortStrategy strategy)
    {
        Numbers = numbers;
        Strategy = strategy;
    }

    public void Sort() => Strategy.Sort(Numbers);

    public void SetStrategy(ISortStrategy strategy) => Strategy = strategy;
}

public class Program
{
    public static void Main()
    {
        List<int> nums = new() { 5, 3, 5 };

        Sorter sorter = new(nums, new BubbleSort());

        sorter.Sort();
        sorter.SetStrategy(new MergeSort());
        sorter.Sort();
    }
}
