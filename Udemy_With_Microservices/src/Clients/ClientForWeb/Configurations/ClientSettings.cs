namespace ClientForWeb.Configurations
{
    public class ClientSettings
    {
        public Client WebClient { get; set; }
        public Client WebClientForUser { get; set; }
    }
    public class Client
    {
        public string Client_Id { get; set; }
        public string Client_Secret { get; set; }
    }
}
