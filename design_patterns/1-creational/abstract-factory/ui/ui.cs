// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

using System;
using System.Collections.Generic;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        IUIFactory factory = new LightThemeFactory();
            var ui = new UIRenderer(factory);
        ui.RenderAll();
    }

    
    
    
}
public interface IButton{
    void Render();

}
public interface ICheckbox{
    void Render();
}


public interface IUIFactory{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}


public class LightButton:IButton{

    public void Render(){Console.WriteLine("Rendering light button");}
}

public class LightCheckbox:ICheckbox{

    public void Render() { Console.WriteLine("Rendering light checkbox");}
}


public class DarkButton:IButton{

    public void Render() { Console.WriteLine("Rendering Dark button");}
}


public class DarkCheckbox:ICheckbox{

    public void Render() => Console.WriteLine("Rendering dark checkbox");
}

public class LightThemeFactory : IUIFactory{

    public IButton CreateButton() => new LightButton();
    public ICheckbox CreateCheckbox() => new LightCheckbox();
}

public class DarkThemeFactory : IUIFactory{

    public IButton CreateButton() => new DarkButton();
    public ICheckbox CreateCheckbox() => new DarkCheckbox();
}


public class UIRenderer{
     private IButton _button;
    private ICheckbox _checkbox;

    public UIRenderer(IUIFactory factory) {
        _button = factory.CreateButton();
        _checkbox = factory.CreateCheckbox();
    }

    public void RenderAll() {
        _button.Render();
        _checkbox.Render();
    }
}
