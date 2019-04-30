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
                <body style='font-size: 1em; width: fit-content'>
                    {html}
                </body>

                <script>
                    images = document.getElementsByTagName('img');
                    for(i=0; i<images.length; i++)
                        images[i].style.width = '100%';
                </script>
            </html>";
            
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            
            return str;
        }
    }
}