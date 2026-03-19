public class Bird {
    public string Name { get; set; }

    public virtual void Fly() {
        Console.WriteLine($"{Name} is flying!");
    }

    public virtual void Eat() {
        Console.WriteLine($"{Name} is eating!");
    }
}

public class Sparrow : Bird {
    public override void Fly() {
        Console.WriteLine($"{Name} is flying high!");
    }
}

public class Penguin : Bird {
    public override void Fly() {
        throw new Exception("Penguins can't fly!"); // 🚩
    }
}

public class BirdSanctuary {
    private List<Bird> _birds;

    public BirdSanctuary(List<Bird> birds) {
        _birds = birds;
    }

    public void MakeBirdsFly() {
        foreach (var bird in _birds) {
            bird.Fly(); // 💥 blows up when Penguin is in the list
        }
    }
}