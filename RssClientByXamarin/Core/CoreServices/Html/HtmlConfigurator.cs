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
            var str = $@"<!doctype html>
            <html>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
                    <meta http-equiv='Content-Style-Type' content='text/css'>
                </head>
                <body style='font-size: medium;'>
                    {html}
                </body>
            </html>";
            
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            
            return str;
        }
    }
}