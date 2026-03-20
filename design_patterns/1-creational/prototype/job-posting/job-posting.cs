public interface IClonable{
    IClonable Clone();
}


public class JobPosting : IClonable{

    public string Title {get; set;}
    public string Description {get; set;}
    public string Location {get; set;}
    public bool IsRemote {get; set;}

    JobPosting(string title, string description, string location, bool isRemote){
        Title = title;
        Description = description;
        Location = location;
        IsRemote = isRemote;
    }


    public IClonable Clone(){
        return new JobPosting(title:Title, description:Description, location:Location, isRemote:IsRemote);
    }

    public override string ToString(){
        return $"title: {Title}, description:{Description}, location:{Location}, isRemote:{IsRemote}";
    }
}