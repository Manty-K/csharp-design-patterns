public interface ILead
{
   void CreateSubTask();
   void AssginTask();
   void WorkOnTask();
}
public class TeamLead : ILead
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
public class Manager: ILead
{
   public void AssignTask()
   {
      //Code to assign a task.
   }
   public void CreateSubTask()
   {
      //Code to create a sub task.
   }
   public void WorkOnTask()
   {
      throw new Exception("Manager can't work on Task");
   }
}

public class Programer: ILead
{
   public void AssignTask()
   {
      throw new Exception("Manager can't assign Task");
   }
   public void CreateSubTask()
   {
      throw new Exception("Manager can't create tasks");
   }
   public void WorkOnTask()
   {
        // code to work on task
   }
}