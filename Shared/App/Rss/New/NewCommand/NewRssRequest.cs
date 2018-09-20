using System;
using Shared.App.Base.Command;

namespace Shared.App.Rss.New.NewCommand
{
    public class NewRssRequest
    {
        public string Name { get; set; }
        public string Rss { get; set; }

        public NewRssRequest()
        {
            
        }

        public NewRssRequest(string name, string rss)
        {
            Name = name;
            Rss = rss;
        }

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
