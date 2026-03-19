using System.Collections.Generic;


public class HttpRequestBuilder{

    private HTTPRequest _request = new HTTPRequest();

    public HttpRequestBuilder addUrl(string url){
        _request.URL = url;
        return this;

    }
    public HttpRequestBuilder addMethod(string method){
        _request.Method = method;
        return this;
    }
    public HttpRequestBuilder addBody(string body){
        _request.Body = body;
        return this;

    }
    public HttpRequestBuilder addHeaders(Dictionary<string, string> headers){
        _request.Headers = headers;
        return this;
    }

    public HTTPRequest build() {
        if (string.IsNullOrWhiteSpace(_request.URL))
            throw new InvalidOperationException("URL is required.");

        if (string.IsNullOrWhiteSpace(_request.Method))
            throw new InvalidOperationException("Method is required.");

        return _request;
    }

}


public class HTTPRequest
{
    public string URL { get; set; }
    public string Method { get; set; }
    public string Body { get; set; }
    public Dictionary<string,string> Headers { get; set; }

    public override string ToString() =>
        $"{URL} url | {Method} method | Body: {Body} | Headers: {Headers}";
}

public class Program
{
    public static void Main(string[] args)
    {
        HTTPRequest request = new HttpRequestBuilder().addUrl("www").addBody("Body").addMethod("POST").build();
        Console.WriteLine(request);
    }
}