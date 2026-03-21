
public interface IFileReader{
    string Read(string filename);
}


public class MyFileReader: IFileReader{
    public string Read(string filename){
        Console.WriteLine("Reading File.");
        return "File Content....";
    }
}

public class FileReaderCacheProxy : IFileReader {

    private readonly MyFileReader reader = new MyFileReader();


    public string Read(string filename){
        // simulated cache hit or miss
        Console.WriteLine($"Finding in {filename} Cache.");
        Random rand = new Random();
        bool randomBool = rand.Next(2) == 0; 
        if(randomBool){
            return "File Contents...(from cache)"; 
        }


        return reader.Read(filename);
    }

}