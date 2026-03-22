// Iterator interface
public interface IIterator<T>
{
    bool HasNext();
    T Next();
}

// Collection interface
public interface IIterableCollection<T>
{
    IIterator<T> CreateIterator();
}

// Concrete collection
public class SongPlaylist : IIterableCollection<string>
{
    private List<string> _songs = new();

    public void AddSong(string song) => _songs.Add(song);

    public IIterator<string> CreateIterator() =>
        new SongIterator(_songs);
}

// Concrete iterator
public class SongIterator : IIterator<string>
{
    private List<string> _songs;
    private int _index = 0;

    public SongIterator(List<string> songs) => _songs = songs;

    public bool HasNext() => _index < _songs.Count;

    public T Next() => _songs[_index++];
}

public class Pragram
{
    public static void Main()
    {
        // Usage
        var playlist = new SongPlaylist();
        playlist.AddSong("Bohemian Rhapsody");
        playlist.AddSong("Stairway to Heaven");
        playlist.AddSong("Hotel California");

        var iterator = playlist.CreateIterator();
        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
        // Bohemian Rhapsody
        // Stairway to Heaven
        // Hotel California
    }
}

