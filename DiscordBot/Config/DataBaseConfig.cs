namespace DiscordBot.Host.Config
{
    public class DataBaseConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string DBName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            return $"Host={Host};Port={Port};Database={DBName};Username={UserName};Password={Password};";
        }
    }
}
