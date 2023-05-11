namespace DiscountService.API.Configs
{
    public static class Configurations
    {
        public static string GetConnectionString()
        {
            ConfigurationManager configurationManager = new();

            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("SqlServer");
        }
    }
}
