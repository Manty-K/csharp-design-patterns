

public interface IButton{
    void Render();

}
public interface IScrollbar{
    void Render();
}

public class WindowButton:IButton{
    public void Render(){Console.WriteLine("Rendering [Win] Button");}
}
public class WindowScrollbar:IScrollbar{
    public void Render(){Console.WriteLine("Rendering [Win] Scrollbar");}
}

public class MacButton:IButton{

    public void Render(){Console.WriteLine("Rendering [Mac] Button");}
}
public class MacScrollbar:IScrollbar{

    public void Render(){Console.WriteLine("Rendering [Mac] Scrollbar");}
}


public interface IUIFactory{
    IButton CreateButton();
    IScrollbar CreateScrollbar();
}

public class WindowUIFactory :IUIFactory{

    public IButton CreateButton() => new WindowButton();
    public IScrollbar CreateScrollbar() => new WindowScrollbar();
}

public class MacUIFactory :IUIFactory{

    public IButton CreateButton() => new MacButton();
    public IScrollbar CreateScrollbar() => new MacScrollbar();
}

class PlatformFactoryProvider{
    public static IUIFactory GetFactory(string platform){
        return platform switch 
             {
            "windows"  => new WindowUIFactory(),
            "mac"   => new MacUIFactory(),
            _       => throw new Exception("Undefined Type")
        };
    }
}

public class PlatformUIRenderer{
    private IButton _button;
    private IScrollbar _scrollbar;

    public PlatformUIRenderer(string platform) {
        IUIFactory factory = PlatformFactoryProvider.GetFactory(platform);
        _button = factory.CreateButton();
        _scrollbar = factory.CreateScrollbar();
    }

    public void RenderAll() {
        _button.Render();
        _scrollbar.Render();
    }
}
