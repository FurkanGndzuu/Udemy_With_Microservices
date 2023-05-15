using ClientForWeb.Configurations;
using Microsoft.Extensions.Options;

namespace ClientForWeb.Helpers
{
    public class PhotoHelper
    {
        private readonly ServiceApiSettings _serviceApiSettings;

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public string GetPhotoStockUrl(string photoUrl)
        {
            return $"{_serviceApiSettings.PhotoPath}/photos/{photoUrl}";
        }
    }
}
