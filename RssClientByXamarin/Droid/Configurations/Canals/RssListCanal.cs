using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Core.Resources;

namespace Droid.Configurations.Canals
{
    public class RssListCanal
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public RssListCanal(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public RssListCanal()
        {

        }

        public static RssListCanal DefaultInit(RssListCanal canal)
        {
            canal.Id = Guid.NewGuid().ToString();
            canal.Name = Strings.RssListCanalName;
            canal.Description = Strings.RssListCanalDescription;

            return canal;
        }

        public Notification GenerateNotification(Context context)
        {
            return new NotificationCompat.Builder(context, Id)
                .SetContentTitle(Strings.NotificationsRssListTitle)
                .SetContentText(Strings.NotificationsRssListText)
                .SetContentInfo(Strings.NotificationsRssListInfo)
                .SetAutoCancel(false)
                .SetSmallIcon(Resource.Drawable.baseline_rss_feed_24)
                .SetPriority(NotificationCompat.PriorityLow)
                .Build();
        }
    }
}