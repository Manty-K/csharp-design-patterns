using System;


namespace VariableScope
{
    public interface IChatMediator
    {
        void SendMessage(string message, User sender);
        void Register(User user);
    }

    // Concrete mediator — controls all communication
    public class ChatRoom : IChatMediator
    {
        private List<User> _users = new();

        public void Register(User user) => _users.Add(user);

        public void SendMessage(string message, User sender)
        {
            foreach (var user in _users)
            {
                if (user != sender)  // don't send to yourself
                    user.Receive(message, sender.Name);
            }
        }
    }

    // Colleague — only knows about mediator, not other users
    public abstract class User
    {
        protected IChatMediator _mediator;
        public string Name { get; }

        public User(string name, IChatMediator mediator)
        {
            Name = name;
            _mediator = mediator;
        }

        public abstract void Send(string message);
        public abstract void Receive(string message, string from);
    }

    // Concrete colleague
    public class ChatUser : User
    {
        public ChatUser(string name, IChatMediator mediator)
            : base(name, mediator) { }

        public override void Send(string message)
        {
            Console.WriteLine($"[{Name}] sends: {message}");
            _mediator.SendMessage(message, this);
        }

        public override void Receive(string message, string from) =>
            Console.WriteLine($"[{Name}] received from {from}: {message}");
    }

    class Program
    {

        static void Main(string[] args)
        {

            var chatRoom = new ChatRoom();

            var alice = new ChatUser("Alice", chatRoom);
            var bob = new ChatUser("Bob", chatRoom);
            var carol = new ChatUser("Carol", chatRoom);

            chatRoom.Register(alice);
            chatRoom.Register(bob);
            chatRoom.Register(carol);

            alice.Send("Hey everyone!");

        }

    }
}