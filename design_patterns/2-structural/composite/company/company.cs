
using System;
using System.Collections.Generic;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        var akka = new IndividualEmployee("Akshada", "Intern");
        var srishti = new IndividualEmployee("Srushti", "Intern");
        var manthan = new Manager("Shreya","Senior Dev");
        manthan.Add(akka);
        manthan.Add(srishti);
        
        var dipali =  new IndividualEmployee("Dipali", "QA");
        var ps =  new Manager("Prathamesh", "QA lead");
        ps.Add(dipali);
        var niranjan = new Manager("Niranjan","CEO");
        niranjan.Add(manthan);
        niranjan.Add(ps);
        niranjan.Print();
    }
}
public interface IEmployee{
    string Name {get;}
    string Role {get;}
    void Print(string indent = "");
}   

public class IndividualEmployee : IEmployee{
    public string Name {get;}
    public string Role {get;}

    public IndividualEmployee(string name, string role){
         Name = name;
         Role = role;
    }

    public void Print(string indent = "") =>
        Console.WriteLine($"{indent}📄 {Name} ({Role})");

}

public class Manager : IEmployee{
    public string Name {get;}
    public string Role {get;}

    public Manager(string name, string role){
         Name = name;
         Role = role;
    }

    private List<IEmployee> team = new List<IEmployee>();
    public void Add(IEmployee employee) => team.Add(employee);


    public void Print(string indent = "") {
        Console.WriteLine($"{indent}📁 {Name} ({Role})");
        foreach(var employee in team){
            employee.Print(indent + "  ");
        }
    }

}