using System.Collections.Generic;
using System.Linq;
using Shared.Database;
using Shared.Database.Rss;

namespace Shared.Repository.RssRecommended
{
    public class RssRecommendedRepository : IRssRecommendedRepository
    {
        private readonly RealmDatabase _realmDatabase;
        private readonly IMapper<RssRecommendationModel, RssRecommendedData> _mapper;

        public RssRecommendedRepository(RealmDatabase realmDatabase, IMapper<RssRecommendationModel, RssRecommendedData> mapper)
        {
            _realmDatabase = realmDatabase;
            _mapper = mapper;

            InitIfEmpty();
        }

        private void InitIfEmpty()
        {
            if (!GetAll().Any())
            {
                _realmDatabase.MainThreadRealm.Write(() =>
                {
                    var newsList = new List<RssRecommendationModel>()
                    {
                        new RssRecommendationModel("http://online.wsj.com/xml/rss/3_7011.xml"),
                        new RssRecommendationModel("http://rss.cnn.com/rss/cnn_topstories.rss"),
                        new RssRecommendationModel("http://feeds.feedburner.com/time/topstories"),
                        new RssRecommendationModel("http://rss.nytimes.com/services/xml/rss/nyt/HomePage.xml"),
                        new RssRecommendationModel("https://www.yahoo.com/news/rss/topstories"),
                        new RssRecommendationModel("http://feeds.washingtonpost.com/rss/world"),
                        new RssRecommendationModel("https://www.vox.com/rss/index.xml"),
                        new RssRecommendationModel("http://feeds.bbci.co.uk/news/world/rss.xml"),
                        new RssRecommendationModel("https://www.huffingtonpost.com/section/front-page/feed"),
                        new RssRecommendationModel("http://rss.cnn.com/rss/cnn_world.rss"),
                        new RssRecommendationModel("http://abcnews.go.com/abcnews/topstories"),
                        new RssRecommendationModel("http://feeds.reuters.com/reuters/topNews?irpc=69"),
                        new RssRecommendationModel("http://www.drudgereportfeed.com/rss.xml"),
                        new RssRecommendationModel("http://www.marketwatch.com/rss/topstories/"),
                        new RssRecommendationModel("https://www.salon.com/feed/"),
                        new RssRecommendationModel("http://www.newyorker.com/services/rss/feeds/everything.xml"),
                        new RssRecommendationModel("http://www.dailymail.co.uk/articles.rss"),
                        new RssRecommendationModel("https://nypost.com/feed/"),
                    };
                    newsList.ForEach(w => w.Category = Categories.News);

                    var sportsList = new List<RssRecommendationModel>()
                    {
                        new RssRecommendationModel("http://feeds.bbci.co.uk/sport/rss.xml?edition=int#"),
                        new RssRecommendationModel("http://www.espn.com/espn/rss/news"),
                        new RssRecommendationModel("http://www.nba.com/rss/nba_rss.xml"),
                        new RssRecommendationModel("http://www.nfl.com/rss/rsslanding?searchString=home"),
                        new RssRecommendationModel("https://www.yahoo.com/news/rss/sports"),
                        new RssRecommendationModel(
                            "http://newsrss.bbc.co.uk/rss/sportonline_world_edition/front_page/rss.xml"),
                        new RssRecommendationModel("https://www.westhesportsguy.com/feeds/posts/default"),
                        new RssRecommendationModel("https://api.foxsports.com/v1/rss"),
                        new RssRecommendationModel("https://talksport.com/rss/sports-news/all/feed"),
                        new RssRecommendationModel("https://rss.cbssports.com/rss/headlines/"),
                        new RssRecommendationModel("https://www.fifa.com/rss/index.xml"),
                        new RssRecommendationModel("http://nusports.com/rss_feeds.aspx"),
                        new RssRecommendationModel("https://www.sport1.de/news.rss"),
                        new RssRecommendationModel("https://www.triathlonmag.com.au/rss"),
                        new RssRecommendationModel("https://www.issf-sports.org/rss/news.html"),
                    };
                    sportsList.ForEach(w => w.Category = Categories.Sport);

                    var technologyList = new List<RssRecommendationModel>()
                    {
                        new RssRecommendationModel("http://feeds.feedburner.com/Techcrunch"),
                        new RssRecommendationModel("http://feeds.wired.com/wired/index"),
                        new RssRecommendationModel("http://feeds.nytimes.com/nyt/rss/Technology"),
                        new RssRecommendationModel("http://rss.macworld.com/macworld/feeds/main"),
                        new RssRecommendationModel("http://feeds.pcworld.com/pcworld/latestnews"),
                        new RssRecommendationModel("http://www.techworld.com/news/rss"),
                        new RssRecommendationModel("https://lifehacker.com/rss"),
                        new RssRecommendationModel("http://feeds.feedburner.com/readwriteweb"),
                        new RssRecommendationModel("http://www.engadget.com/rss-full.xml"),
                        new RssRecommendationModel("http://readwrite.com/feed/"),
                        new RssRecommendationModel("http://feeds.mashable.com/Mashable"),
                        new RssRecommendationModel("http://feeds.feedburner.com/oreilly/radar/atom"),
                        new RssRecommendationModel("https://gizmodo.com/rss"),
                        new RssRecommendationModel("https://www.technologyreview.com/topnews.rss"),
                        new RssRecommendationModel("https://venturebeat.com/2013/09/05/venturebeat-rss/"),
                        new RssRecommendationModel("http://www.recode.net/rss/index.xml"),
                        new RssRecommendationModel("http://www.computerworld.com/index.rss"),
                        new RssRecommendationModel("http://feeds.feedburner.com/Makeuseof"),
                        new RssRecommendationModel("http://www.cnet.com/rss/news"),
                        new RssRecommendationModel("http://feeds.howtogeek.com/HowToGeek"),
                    };
                    technologyList.ForEach(w => w.Category = Categories.Technology);

                    var businessList = new List<RssRecommendationModel>()
                    {
                        new RssRecommendationModel("http://feeds.feedburner.com/AtlanticBusinessChannel"),
                        new RssRecommendationModel("http://feeds.feedburner.com/entrepreneur/latest"),
                        new RssRecommendationModel("http://feeds.harvardbusiness.org/harvardbusiness/"),
                        new RssRecommendationModel("http://freakonomics.com//feed/"),
                        new RssRecommendationModel("http://feeds.feedburner.com/TheBigPicture"),
                        new RssRecommendationModel("http://feeds.feedburner.com/venturebeat/SZYF"),
                        new RssRecommendationModel("http://fortune.com/feed/"),
                        new RssRecommendationModel("http://www.economist.com/rss/the_world_this_week_rss.xml"),
                        new RssRecommendationModel("http://www.thenonprofittimes.com/feed/"),
                        new RssRecommendationModel("http://www.sbnonline.com/feed/"),
                        new RssRecommendationModel("https://www.mckinsey.com/insights/rss.aspx"),
                        new RssRecommendationModel("https://homebusinessmag.com/feed/"),
                        new RssRecommendationModel("http://feeds2.feedburner.com/businessinsider"),
                        new RssRecommendationModel("http://feeds.feedburner.com/CalculatedRisk"),
                        new RssRecommendationModel("https://www.huffingtonpost.com/section/business/feed"),
                    };
                    businessList.ForEach(w => w.Category = Categories.Business);

                    var politicsList = new List<RssRecommendationModel>()
                    {
                        new RssRecommendationModel(
                            "http://www.slate.com/articles/news_and_politics/politics.teaser.all.10.rss/"),
                        new RssRecommendationModel("http://www.worldaffairsjournal.org/essay-feed.xml"),
                        new RssRecommendationModel("http://feeds.foxnews.com/foxnews/politics"),
                        new RssRecommendationModel("http://rss.cnn.com/rss/cnn_allpolitics.rss"),
                        new RssRecommendationModel("http://feeds.reuters.com/Reuters/PoliticsNews"),
                        new RssRecommendationModel("http://rssfeeds.usatoday.com/TP-OnPolitics"),
                        new RssRecommendationModel("http://www.washingtonexaminer.com/rss/politics"),
                        new RssRecommendationModel("http://online.wsj.com/xml/rss/3_7087.xml"),
                        new RssRecommendationModel("https://www.thenation.com/feed/?post_type=article"),
                        new RssRecommendationModel("http://dailysignal.com//feed/"),
                        new RssRecommendationModel("http://www.msnbc.com/feeds/latest"),
                        new RssRecommendationModel("https://www.politico.com/rss/politics.xml"),
                        new RssRecommendationModel("https://www.realwire.com/rss/feeds.asp?cat=Politics"),
                    };
                    politicsList.ForEach(w => w.Category = Categories.Politics);

                    var gamingList = new List<RssRecommendationModel>()
                    {
                        new RssRecommendationModel("http://old-hard.ru/rss"),
                        new RssRecommendationModel("https://www.gamespot.com/feeds/mashup/"),
                        new RssRecommendationModel("http://www.nintendolife.com/feeds/latest"),
                        new RssRecommendationModel("http://www.joystiq.com/rss.xml"),
                        new RssRecommendationModel("http://www.indiegames.com/atom.xml"),
                        new RssRecommendationModel("http://feeds.arstechnica.com/arstechnica/gaming/"),
                        new RssRecommendationModel("https://www.polygon.com/rss/index.xml"),
                        new RssRecommendationModel("http://toucharcade.com/feed/"),
                        new RssRecommendationModel("http://www.gameinformer.com/p/rss.aspx"),
                        new RssRecommendationModel("http://news.xbox.com/feed"),
                        new RssRecommendationModel("https://www.reddit.com/r/gamers/.rss"),
                        new RssRecommendationModel("http://www.vg247.com/feed/"),
                        new RssRecommendationModel("https://mynintendonews.com/feed"),
                        new RssRecommendationModel("https://www.rockpapershotgun.com/feed"),
                        new RssRecommendationModel("https://www.pcgamesn.com/rss"),
                        new RssRecommendationModel("http://www.operationsports.com/rss.xml"),
                        new RssRecommendationModel("https://kotaku.com/rss"),
                        new RssRecommendationModel("http://feeds.videogamer.com/rss/allupdates.xml"),
                        new RssRecommendationModel("http://www.pushsquare.com/feeds/latest"),
                    };
                    gamingList.ForEach(w => w.Category = Categories.Gaming);

                    var allList = new List<RssRecommendationModel>();
                    allList.AddRange(newsList);
                    allList.AddRange(sportsList);
                    allList.AddRange(technologyList);
                    allList.AddRange(businessList);
                    allList.AddRange(politicsList);
                    allList.AddRange(gamingList);

                    for (var i = 0; i < allList.Count; i++)
                        allList[i].Position = i;

                    allList.ForEach(w => _realmDatabase.MainThreadRealm.Add(w));

                });
            }
        }

        public IEnumerable<RssRecommendedData> GetAll()
        {
            return _realmDatabase.MainThreadRealm.All<RssRecommendationModel>().OrderBy(w => w.Position).ToList()
                .Select(_mapper.Transform);
        }

        public IEnumerable<RssRecommendedData> GetAllByCategory(Categories categories)
        {
            var category = (int) categories;
            return _realmDatabase.MainThreadRealm.All<RssRecommendationModel>().Where(w => w.CategoryInt == category)
                .OrderBy(w => w.Position).ToList().Select(_mapper.Transform);
        }

        public IEnumerable<Categories> GetCategories()
        {
            return _realmDatabase.MainThreadRealm.All<RssRecommendationModel>().OrderBy(w => w.Position).ToList()
                .Select(w => w.Category).Distinct();
        }
    }
}