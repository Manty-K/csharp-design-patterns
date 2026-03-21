

public class Projector{

    public void TurnOn(){
        Console.WriteLine("Turning on Projector");
    }

    public void SetInput(string source){
        Console.WriteLine($"Setting input : {source}");
    }
}

public class SoundSystem{
     public void TurnOn(){
        Console.WriteLine("Turning on Sound System");
    }

    public void SetVolume(int volume){
        Console.WriteLine($"Setting volume : {volume}");
    }
}

public class StreamingPlayer{

    public void Play(string movie){
        Console.WriteLine($"Playing : {movie}");
    }
}


public class HomeTheaterFacade{

    private Projector projector = new Projector();
    private SoundSystem soundSystem = new SoundSystem();
    private StreamingPlayer streamingPlayer = new StreamingPlayer();

    public void WatchMovie(string movie){
        projector.TurnOn();
        projector.SetInput("S1");
        soundSystem.TurnOn();
        soundSystem.SetVolume(10);
        streamingPlayer.Play(movie);
    }
    
}