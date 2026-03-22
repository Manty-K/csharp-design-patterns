// Base handler
public abstract class SupportHandler
{
    protected SupportHandler _next;

    public SupportHandler SetNext(SupportHandler next)
    {
        _next = next;
        return next; // allows chaining
    }

    public abstract void Handle(string issue);
}

// Concrete handlers
public class BotHandler : SupportHandler
{
    public override void Handle(string issue)
    {
        if (issue == "faq")
        {
            Console.WriteLine("[Bot] Handled: FAQ question");
        }
        else
        {
            Console.WriteLine("[Bot] Can't handle, escalating...");
            _next?.Handle(issue);
        }
    }
}

public class JuniorAgentHandler : SupportHandler
{
    public override void Handle(string issue)
    {
        if (issue == "billing")
        {
            Console.WriteLine("[Junior] Handled: Billing issue");
        }
        else
        {
            Console.WriteLine("[Junior] Can't handle, escalating...");
            _next?.Handle(issue);
        }
    }
}

public class SeniorAgentHandler : SupportHandler
{
    public override void Handle(string issue)
    {
        Console.WriteLine($"[Senior] Handled: {issue}"); // handles everything
    }
}

public class Main
{

    public static void Main()
    {
        var bot = new BotHandler();
        var junior = new JuniorAgentHandler();
        var senior = new SeniorAgentHandler();

        bot.SetNext(junior).SetNext(senior); // chain: bot → junior → senior

        bot.Handle("faq");        // [Bot] Handled
        bot.Handle("billing");    // [Bot] ↑ [Junior] Handled
        bot.Handle("outage");     // [Bot] ↑ [Junior] ↑ [Senior] Handled
    }
}

// Usage — build the chain
