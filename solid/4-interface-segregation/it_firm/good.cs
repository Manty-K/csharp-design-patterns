public interface IProgrammer
{
    void WorkOnTask();
}
public interface IManager
{
    void CreateSubTask();
    void AssignTask();
}

public class TeamLead : IProgrammer, IManager
{
   public void AssignTask()
   {
      //Code to assign a task.
   }
   public void CreateSubTask()
   {
      //Code to create a sub task
   }
   public void WorkOnTask()
   {
      //Code to implement perform assigned task.
   }
}
public class Manager: IManager
{
   public void AssignTask()
   {
      //Code to assign a task.
   }
   public void CreateSubTask()
   {
      //Code to create a sub task.
   }
}

public class Programmer : IProgrammer {

    public void WorkOnTask(){
            // Working
    }

}