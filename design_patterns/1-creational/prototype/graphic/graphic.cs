public interface IClonable{
    IClonable Clone();
}

public class Graphic : IClonable{

    public string ID {get;set;}
    public string Name {get;set;}
    public string Position {get;set;}

    public Graphic(string id, string name, string position){
        ID = id;
        Name = name;
        Position = position;
    }

    public IClonable Clone(){
        return new Graphic(id:ID, name:Name, position:Position);
    }

    public override string ToString(){
        return $"id : {ID} | position : {Position} | name : {Name}";
    }

}