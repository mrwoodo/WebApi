namespace WebApiTest
{
    class Settings
    {
        public string APIServerBaseUrl { get; set; }
        public string APIClientId { get; set; }
        public string APIClientSecret { get; set; }
        public string APIScope { get; set; }

        public Settings()
        {
            APIClientId = "apiClient";
            APIClientSecret = "apiSecret";
            APIScope = "api";
            APIServerBaseUrl = "https://localhost:5000";
        }
    }
}
