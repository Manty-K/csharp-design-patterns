// Implementation interface (Device side)
public interface IDevice {
    void TurnOn();
    void TurnOff();
    void SetVolume(int level);
}

// Concrete implementations
public class SamsungTV : IDevice {
    public void TurnOn() => Console.WriteLine("[Samsung] TV On");
    public void TurnOff() => Console.WriteLine("[Samsung] TV Off");
    public void SetVolume(int level) => Console.WriteLine($"[Samsung] Volume: {level}");
}

public class SonyTV : IDevice {
    public void TurnOn() => Console.WriteLine("[Sony] TV On");
    public void TurnOff() => Console.WriteLine("[Sony] TV Off");
    public void SetVolume(int level) => Console.WriteLine($"[Sony] Volume: {level}");
}

// Abstraction (Remote side) — holds a reference to IDevice
public abstract class Remote {
    protected IDevice _device;
    public Remote(IDevice device) => _device = device;
    public abstract void Power();
    public abstract void VolumeUp();
}

// Refined abstractions
public class BasicRemote : Remote {
    private int _volume = 5;
    public BasicRemote(IDevice device) : base(device) { }
    public override void Power() => _device.TurnOn();
    public override void VolumeUp() => _device.SetVolume(++_volume);
}

public class SmartRemote : Remote {
    private int _volume = 5;
    public SmartRemote(IDevice device) : base(device) { }
    public override void Power() {
        Console.WriteLine("[Smart] Launching smart menu...");
        _device.TurnOn();
    }
    public override void VolumeUp() {
        Console.WriteLine("[Smart] Auto-adjusting...");
        _device.SetVolume(++_volume);
    }
}

// Usage — mix and match freely
Remote r1 = new BasicRemote(new SamsungTV());
r1.Power();      // [Samsung] TV On
r1.VolumeUp();   // [Samsung] Volume: 6

Remote r2 = new SmartRemote(new SonyTV());
r2.Power();      // [Smart] Launching smart menu... [Sony] TV On
r2.VolumeUp();   // [Smart] Auto-adjusting... [Sony] Volume: 6