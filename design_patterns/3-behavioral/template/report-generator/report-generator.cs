
using System;


namespace VariableScope
{

    public abstract class Reporter
    {

        public void Generate()
        {
            FetchData();
            FormatData();
            GenerateReport();
            SendReport();

        }

        protected abstract void FetchData();
        protected abstract void FormatData();
        protected abstract void GenerateReport();

        protected virtual void SendReport()
        {
            Console.WriteLine("Sending via email...");
        }

    }

    public class SalesReporter : Reporter
    {
        protected override void FetchData()
        {
            Console.WriteLine("Fetching sales data...");
        }
        protected override void FormatData()
        {
            Console.WriteLine("Formatting as Table");
        }

        protected override void GenerateReport()
        {
            Console.WriteLine("Sales Report Ready.");
        }
    }

    public class InvertoryReport : Reporter
    {

        protected override void FetchData()
        {
            Console.WriteLine("Fetching inventory data...");
        }

        protected override void FormatData()
        {
            Console.WriteLine("Formatting as List");
        }

        protected override void GenerateReport()
        {
            Console.WriteLine("Inventory report ready");
        }

        protected override void SendReport()
        {
            Console.WriteLine("Sending via Slack..");
        }
    }



    class Program
    {

        static void Main(string[] args)
        {

            Reporter salesReport = new SalesReporter();
            salesReport.Generate();
            Console.WriteLine();
            Reporter inventoryReport = new InvertoryReport();
            inventoryReport.Generate();


        }

    }
}