using System;
using System.Linq;
using System.ServiceModel.Syndication;
using Shared.App.Base.Database;

namespace RssClient.App.Rss.Detail
{
    public class RssMessageModel : Entity
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }

        public int PrimaryKeyRssModel { get; set; }

        public RssMessageModel(SyndicationItem syndicationItem, int primaryKey)
        {
            Title = syndicationItem.Title?.Text;
            Text = syndicationItem.Summary.Text;
            CreationDate = syndicationItem.PublishDate.Date;
            Url = syndicationItem.Links?.FirstOrDefault()?.Uri?.AbsoluteUri;

            PrimaryKeyRssModel = primaryKey;
        }

        public RssMessageModel()
        {
            
        }
    }
}