class AssetManager {

    private static AssetManager _instance;

    private AssetManager() {} 

    public static AssetManager GetInstance() {
        if (_instance == null)
            _instance = new AssetManager();
        
        return _instance;
    }
}


