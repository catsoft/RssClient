using System.Text;
using Core.Configuration.Settings;
using Core.Repositories.Configurations;

namespace Core.CoreServices.Html
{
    public class HtmlConfigurator : IHtmlConfigurator
    {
        private readonly IConfigurationRepository _configurationRepository;

        public HtmlConfigurator(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        
        public string ConfigureHtml(string html)
        {
            var builder = new StringBuilder(html);

            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            
            return builder.ToString();
        }
    }
}