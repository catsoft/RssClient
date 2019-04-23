using System.Collections.Generic;

namespace Core.Repositories.Feedly
{
    public class FeedlyRssDomainModel
    {
        public List<string> DeliciousTags { get; set; }
        public string FeedId { get; set; }

        public string Title { get; set; }

//        public string LastUpdated { get; set; }
        public double Subscribers { get; set; }

        public double Velocity { get; set; }

        public string Website { get; set; }

        public double Score { get; set; }

        public double Coverage { get; set; }

        public double CoverageScore { get; set; }

        public double EstimatedEngagement { get; set; }

        public string Hint { get; set; }

        public string Scheme { get; set; }

        public string Language { get; set; }

        public string ContentType { get; set; }

        public string Description { get; set; }

        public string CoverUrl { get; set; }

        public string IconUrl { get; set; }

        public bool Partial { get; set; }

        public string VisualUrl { get; set; }

        public string CoverColor { get; set; }

        public double Art { get; set; }
    }
}
