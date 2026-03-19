

public interface DocumentFactory{

    Document createDocument();

}


public class TxtDocumentFacrory : DocumentFactory{
    
    TxtDocument createDocument(){

    }

}


public class PdfDocumentFacrory : DocumentFactory{

    PdfDocument createDocument(){
        
    }
    
}




public interface Document{

    void addText(string text);
    void save(string path);

}

public class TxtDocument:Document{
    void addText(string text){
     
    }
     void save(string path){
        
    }
}

public class PdfDocument:Document{
    void addText(string text){
     
    }
     void save(string path){
        
    }
}