using System.Text;

class UrlFormatter {

    public bool Https { get; set; } = false;
    public string Server { get; set; } = "localhost";
    public int Port { get; set; } = 80;
    public string Folder { get; set; }
    public string Controller { get; set; }
    public string Values { get; set; }

    public string Url {
        get {
            var url = new StringBuilder();
            url.Append("http");
            if (Https) url.Append("s");
            url.Append("://");
            url.Append($"{Server}");
            url.Append($":{Port}/");
            if (Folder != null)
                url.Append($"{Folder}/");
            url.Append($"{Controller}");
            if (Values != null)
                url.Append($"/{Values}");
            return url.ToString();
        }
    }

    public UrlFormatter() { }
}