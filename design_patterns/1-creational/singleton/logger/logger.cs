class Logger{
    // Singleton start
    private static Logger instance;

    private Logger(){}

    public static Logger GetInstance(){

        if(instance == null)
            instance = new Logger();
        
        return instance;

    }
    // singleton end

    public void Log(string message){
        // code to log into single file
    }

}