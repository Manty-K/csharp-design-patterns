using System;
using System.Collections.Generic;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        var factory = new CharacterTypeFactory();

        var symbols = new List<Character>();
        for (int i = 0; i < 5; i++)
            symbols.Add(new Character(i, i, factory.Get('A', "Roboto", 45)));

        symbols.Add(new Character(10, 11, factory.Get('B', "Roboto", 45)));
        symbols.Add(new Character(12, 17, factory.Get('C', "Roboto", 45)));  // new type

        foreach (var s in symbols) s.Render();
    }
}

public class CharacterType{

    public char Symbol {get;}
    public string Font {get;}
    public int Size {get;}

    public CharacterType(char symbol, string font, int size){
        Symbol = symbol;
        Font = font;
        Size = size;
    }

    public void Render(int x, int y){
        Console.WriteLine($"Rendering {Symbol} {Font} {Size} at {x},{y}");
    }
}

public class  CharacterTypeFactory{
    private Dictionary<string,CharacterType> data = new();
    public CharacterType Get(char symbol, string font, int size){
        string key = $"{symbol}_{font}_{size}";
        if(!data.ContainsKey(key)){
            data[key] = new CharacterType(symbol,font,size);
            Console.WriteLine($"[Factory] Created new CharacterType: {symbol}");
        }
        return data[key];
    }
    
}

public class Character{

    private int X;
    private int Y;
    private CharacterType Type;

    public Character(int x, int y, CharacterType type){
        X =  x;
        Y = y;
        Type = type;
    }

    public void Render(){
        Type.Render(X,Y);
    }
    
}
