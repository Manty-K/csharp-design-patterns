public interface ISmartDevice {
    void TurnOn();
    void TurnOff();
    void SetBrightness(int level);
    void SetTemperature(int temp);
    void PlayMusic(string song);
}

public class SmartLight : ISmartDevice {
    public void TurnOn()  => Console.WriteLine("Light ON");
    public void TurnOff() => Console.WriteLine("Light OFF");
    public void SetBrightness(int level) => Console.WriteLine($"Brightness: {level}");
    
    public void SetTemperature(int temp) {
        throw new Exception("Light can't set temperature!");
    }
    public void PlayMusic(string song) {
        throw new Exception("Light can't play music!");
    }
}

public class SmartThermostat : ISmartDevice {
    public void TurnOn()  => Console.WriteLine("Thermostat ON");
    public void TurnOff() => Console.WriteLine("Thermostat OFF");
    public void SetTemperature(int temp) => Console.WriteLine($"Temp: {temp}°C");
    
    public void SetBrightness(int level) {
        throw new Exception("Thermostat can't set brightness!");
    }
    public void PlayMusic(string song) {
        throw new Exception("Thermostat can't play music!");
    }
}

public class SmartSpeaker : ISmartDevice {
    public void TurnOn()  => Console.WriteLine("Speaker ON");
    public void TurnOff() => Console.WriteLine("Speaker OFF");
    public void PlayMusic(string song) => Console.WriteLine($"Playing: {song}");
    
    public void SetBrightness(int level) {
        throw new Exception("Speaker can't set brightness!");
    }
    public void SetTemperature(int temp) {
        throw new Exception("Speaker can't set temperature!");
    }
}