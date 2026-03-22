using System;


namespace VariableScope
{

    public class Expense
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }

    }

    public abstract class ApprovalHandler
    {
        protected ApprovalHandler _nextHandler;
        public abstract void HandleRequest(Expense expense);
        public ApprovalHandler SetNext(ApprovalHandler handler)
        {
            _nextHandler = handler;
            return _nextHandler;
        }
    }

    public class TeamLeadHandler : ApprovalHandler
    {
        public override void HandleRequest(Expense expense)
        {
            if (expense.Amount <= 10000)
            {
                Console.WriteLine($"Team Lead approved: {expense.Description} - ₹{expense.Amount}");
            }
            else
            {
                _nextHandler.HandleRequest(expense);
            }
        }

    }

    public class ManagerHandler : ApprovalHandler
    {
        public override void HandleRequest(Expense expense)
        {
            if (expense.Amount <= 50000)
            {
                Console.WriteLine($"Manager approved: {expense.Description} - ₹{expense.Amount}");
            }
            else
            {
                _nextHandler.HandleRequest(expense);
            }
        }


    }

    public class DirectorHandler : ApprovalHandler
    {
        public override void HandleRequest(Expense expense)
        {
            if (expense.Amount <= 100_000)
            {
                Console.WriteLine($"Director approved: {expense.Description} - ₹{expense.Amount}");
            }
            else
            {
                _nextHandler.HandleRequest(expense);
            }
        }

    }

    public class CFOHandler : ApprovalHandler
    {
        public override void HandleRequest(Expense expense)
        {
            Console.WriteLine($"CFO approved: {expense.Description} - ₹{expense.Amount}");


        }

    }

    public class ExpenseApprovalChain
    {

        public static ApprovalHandler BuildChain()
        {
            var teamLead = new TeamLeadHandler();
            var manager = new ManagerHandler();
            var director = new DirectorHandler();
            var cfo = new CFOHandler();
            teamLead.SetNext(manager).SetNext(director).SetNext(cfo);
            return teamLead;
        }

    }



    class Program
    {

        static void Main(string[] args)
        {
            ApprovalHandler chain = ExpenseApprovalChain.BuildChain();

            chain.HandleRequest(new Expense { Description = "Office Supplies", Amount = 200000 });
        }

    }
}