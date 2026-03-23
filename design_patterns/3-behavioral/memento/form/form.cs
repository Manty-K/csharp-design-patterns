

namespace VariableScope
{

    public record Form(string Name, string Email, string Phone);


    public class Memento<T>
    {
        public T Content { get; }
        public Memento(T content) => Content = content;
    }

    // 
    public class History<T>
    {
        private Stack<Memento<T>> _snapshots = new();

        public void Push(Memento<T> memento) => _snapshots.Push(memento);

        public Memento<T> Pop()
        {
            if (_snapshots.Count == 0)
                throw new InvalidOperationException("Nothing to undo.");
            return _snapshots.Pop();
        }
    }

    public class Originator<T>
    {
        private T _content;

        public void Write(T content)
        {
            _content = content;
            Console.WriteLine($"Content: {_content}");
        }

        public Memento<T> Save() => new Memento<T>(_content);

        public void Restore(Memento<T> memento)
        {
            _content = memento.Content;
            Console.WriteLine($"Restored content: {_content}");
        }

    }

    public class Program
    {
        public static void Main()
        {


            var originator = new Originator<Form>();
            var history = new History<Form>();

            history.Push(originator.Save());

            var f1 = new Form(Name: "Alice", Email: "ali@e.c", Phone: "123-456-7890");

            originator.Write(f1);

            history.Push(originator.Save());

            var f2 = f1 with { Email = "manthan@email.com" };

            originator.Write(f2);

            history.Push(originator.Save());

            var f3 = f2 with { Phone = "+919999999999" };

            originator.Write(f3);



            originator.Restore(history.Pop());
            originator.Restore(history.Pop());
            originator.Restore(history.Pop());

        }
    }
}