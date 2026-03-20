using System;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        IText mytext = new PlainText("hello");
        
        mytext = new BoldDecorator(mytext);
        mytext = new UpperCaseDecorator(mytext);
        mytext = new ExclamationDecorator(mytext);
        
        Console.WriteLine(mytext.GetContent());
       
    }
}

public interface IText{
    string GetContent();
}

public class PlainText : IText{

    private string _content;

    public PlainText(string content){
        _content = content;
    }

    public string GetContent(){
        return _content;
    }
}

public abstract class TextDecorator : IText{

    protected IText _text;

    public TextDecorator(IText text){
        _text = text;
    }
    public abstract string GetContent();

}

public class BoldDecorator : TextDecorator{

    public BoldDecorator(IText text) : base(text){}

    public override string GetContent(){
        return "**" + _text.GetContent() + "**";

    }
}


public class UpperCaseDecorator : TextDecorator{
      public UpperCaseDecorator(IText text) : base(text){}

    public override string GetContent(){
        return _text.GetContent().ToUpper();

    }
}


public class ExclamationDecorator : TextDecorator{
      public ExclamationDecorator(IText text) : base(text){}

    public override string GetContent(){
        return  _text.GetContent() + "!!!";

    }
}

