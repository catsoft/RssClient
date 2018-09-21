
using System;
using Shared.App.Base.Command;
using Shared.App.Rss.New;

namespace Shared.App.Rss.Edit
{
    public class EditRssRequest
    {
        public EditRssRequest()
        {

        }

        public EditRssRequest(RssModel model, string name, string rss)
        {
            Model = model;
            Name = name;
            Rss = rss;
        }

        public RssModel Model { get; set; }
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
