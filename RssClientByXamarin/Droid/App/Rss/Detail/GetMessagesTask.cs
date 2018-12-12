//using System.Collections.Generic;
//using System.Linq;
//using Android.App;
//using Android.OS;
//using Android.Support.V4.Widget;
//using Android.Support.V7.Widget;
//using Android.Views;
//using Database;
//using Database.Rss;
//using Java.Lang;
//using RssClient.App.Base;

//namespace RssClient.App.Rss.Detail
//{
// TODO Избавиться от этой таски anroid
//    public class GetMessagesTask : AsyncTask<RssModel, IEnumerable<RssMessageModel>, IEnumerable<RssMessageModel>>
//    {
//        private readonly RecyclerView _recyclerView;
//        private readonly ShimmerActivity _shimmerActivity;
//        private readonly SwipeRefreshLayout _refreshLayout;

//        public GetMessagesTask(RecyclerView recyclerView, ShimmerActivity shimmerActivity, SwipeRefreshLayout refreshLayout)
//        {
//            _recyclerView = recyclerView;
//            _shimmerActivity = shimmerActivity;
//            _refreshLayout = refreshLayout;
//        }

//        protected override void OnPreExecute()
//        {
//            base.OnPreExecute();

//            _shimmerActivity.ShimmerViewContainer.StartShimmerAnimation();
//            _shimmerActivity.ShimmerViewContainer.Visibility = ViewStates.Visible;
//        }

//        protected override IEnumerable<RssMessageModel> RunInBackground(params RssModel[] @params)
//        {
//            var item = @params.First();
//            var request = new LoadMessagesRequest(item);
//            var @delegate = _shimmerActivity.GetCommandDelegate<LoadMessagesResponse>(null);
//            var command = new LoadMessagesCommand(LocalDb.Instance, @delegate);
//            command.Execute(request);

//            item.LoadMessagesFromDb(LocalDb.Instance);
//            var messages = item.Messages;
//            return messages;
//        }

//        /// <summary>
//        /// If I don't implement this method then OnPostExecute(List<RssMessageModel> messages) will not be invoked
//        /// </summary>
//        protected override void OnPostExecute(Object result)
//        {
//            base.OnPostExecute(result);
//        }

//        protected override void OnPostExecute(IEnumerable<RssMessageModel> messages)
//        {
//            _shimmerActivity.ShimmerViewContainer.StopShimmerAnimation();
//            _shimmerActivity.ShimmerViewContainer.Visibility = ViewStates.Gone;

//            _refreshLayout.Refreshing = false;

//            if (messages?.Any() == true)
//            {
//                _shimmerActivity.ShowValidData();
//            }
//            else
//            {
//                _shimmerActivity.ShowNotValidError("No data");
//            }

//            var adapter = new RssMessageAdapter(messages ?? new List<RssMessageModel>(), _shimmerActivity);
//            _recyclerView.SetAdapter(adapter);
//        }
//    }
//}