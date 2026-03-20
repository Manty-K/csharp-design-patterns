public class WeatherAPI {
    public string FetchData(double lat, double lon) =>
        $"Temp: 32C at ({lat}, {lon})";
}

public interface IWeatherService {
    string GetWeather(string cityCoordinates); // format: "19.8,75.3"
}


public class WeatherAdapter : IWeatherService{

    private WeatherAPI _weatherApi;

    public WeatherAdapter(WeatherAPI weatherApi){
        _weatherApi = weatherApi;
    }

    struct Coordinates{
        public double lat;
        public double lon;
    }

    // input :  format: "19.8,75.3" -> output: lat=19.8 , lon = 75.3
    private Coordinates decodeCoordinates(string coordinatesString){
        char delimiter = ',';
        string[] parts = coordinatesString.Split(delimiter);

        Coordinates result;
        result.lat = double.Parse(parts[0]);
        result.lon = double.Parse(parts[1]);
        return result;
    }

    public string GetWeather(string cityCoordinates){
        Coordinates coordinates = decodeCoordinates(cityCoordinates);

        return _weatherApi.FetchData(coordinates.lat, coordinates.lon);
    }

}