using System;
using Shared.App.Base.Command;

namespace Shared.App.Rss.New.Command
{
    public class NewRssRequest
    {
        public NewRssRequest()
        {
            
        }

        public NewRssRequest(string name, string rss)
        {
            Name = name;
            Rss = rss;
        }

        public string Name { get; set; }
        public string Rss { get; set; }

        public bool IsValid(Action<NewRssField, Error> errorAction)
        {
            var isError = false;

            if (string.IsNullOrEmpty(Name))
            {
                errorAction(NewRssField.Name, new Error(nameof(RssAppString.NameIsRequered), RssAppString.NameIsRequered));
                isError = true;
            }

            if (string.IsNullOrEmpty(Rss))
            {
                errorAction(NewRssField.Rss, new Error(nameof(RssAppString.RssIsRequered), RssAppString.RssIsRequered));
                isError = true;
            }

            return !isError;
        }
    }
}
