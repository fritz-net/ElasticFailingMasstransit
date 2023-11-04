namespace ElasticFailingMasstransit;

public class RabbitMqSettings
{
    public const string KeyName = "RabbitMqSettings";

    public string Server { get; set; } = "localhost";
    public string Port { get; set; } = "6572";
    public string VirtualHost { get; set; } = "/";
    public string Username { get; set; } = "guest";
    public string Password { get; set; } = "guest";
}