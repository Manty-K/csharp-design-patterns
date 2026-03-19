public class Bird {
    public string Name { get; set; }

    public virtual void Eat() {
        Console.WriteLine($"{Name} is eating!");
    }
}

public interface IFlyable{
        void Fly();
}


public class Sparrow : Bird , IFlyable{
    public void Fly() {
        Console.WriteLine($"{Name} is flying high!");
    }
}
public class Penguin : Bird {}


public class BirdSanctuary {
    private List<IFlyable> _birds;

    public BirdSanctuary(List<IFlyable> birds) {
        _birds = birds;
    }

    public void MakeBirdsFly() {
        foreach (var bird in _birds) {
            bird.Fly();
    }
}
}

