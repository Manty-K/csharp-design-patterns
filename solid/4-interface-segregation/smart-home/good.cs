public interface IPowerDevice {
    void TurnOn();
    void TurnOff();
}
public interface IBrightnessDevice {
    void SetBrightness(int level);
}
public interface IMusicDevice {
    void PlayMusic(string song);
}

public interface ITemperature {
    void SetTemperature(int temp);
}

public class SmartLight : IPowerDevice, IBrightnessDevice {
    public void TurnOn()  => Console.WriteLine("Light ON");
    public void TurnOff() => Console.WriteLine("Light OFF");
    public void SetBrightness(int level) => Console.WriteLine($"Brightness: {level}");
}

public class SmartThermostat : IPowerDevice, ITemperature{
    public void TurnOn()  => Console.WriteLine("Thermostat ON");
    public void TurnOff() => Console.WriteLine("Thermostat OFF");
    public void SetTemperature(int temp) => Console.WriteLine($"Temp: {temp}°C");
}

public class SmartSpeaker : IPowerDevice, IMusicDevice {
    public void TurnOn()  => Console.WriteLine("Speaker ON");
    public void TurnOff() => Console.WriteLine("Speaker OFF");
    public void PlayMusic(string song) => Console.WriteLine($"Playing: {song}");
}