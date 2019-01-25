using System;
using Android.Content;
using Shared.Database.Rss;

namespace Droid.Services.Helpers
{
    public static class CategoriesExtension
    {
        public static string ToLocaleString(this Categories categories, Context context)
        {
            switch (categories)
            {
                case Categories.News:
                    return context.GetText(Resource.String.category_news);
                case Categories.Sport:
                    return context.GetText(Resource.String.category_sport);
                case Categories.Technology:
                    return context.GetText(Resource.String.category_technology);
                case Categories.Business:
                    return context.GetText(Resource.String.category_business);
                case Categories.Politics:
                    return context.GetText(Resource.String.category_politics);
                case Categories.Gaming:
                    return context.GetText(Resource.String.category_gaming);
            }

            return "";
        }
    }
}