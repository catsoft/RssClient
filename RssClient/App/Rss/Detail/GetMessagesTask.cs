using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Java.Lang;
using RssClient.App.Base;
using Shared.App.Base.Database;
using Shared.App.Rss;
using Shared.App.Rss.LoadMessages;

namespace RssClient.App.Rss.Detail
{
    public class GetMessagesTask : AsyncTask<RssModel, List<RssMessageModel>, List<RssMessageModel>>
    {
        private readonly RecyclerView _recyclerView;
        private readonly Activity _activity;
        private readonly SwipeRefreshLayout _refreshLayout;

        public GetMessagesTask(RecyclerView recyclerView, Activity activity, SwipeRefreshLayout refreshLayout)
        {
            _recyclerView = recyclerView;
            _activity = activity;
            _refreshLayout = refreshLayout;
        }

        protected override List<RssMessageModel> RunInBackground(params RssModel[] @params)
        {
            var item = @params.First();
            var request = new LoadMessagesRequest(item);
            var @delegate = _activity.GetCommandDelegate<LoadMessagesResponse>(null);
            var command = new LoadMessagesCommand(_activity, LocalDb.Instance, @delegate);
            command.Execute(request);

            item.LoadMessagesFromDb(LocalDb.Instance);
            var messages = item.Messages;
            return messages;
        }

        /// <summary>
        /// If I don't implement this method then OnPostExecute(List<RssMessageModel> messages) will not be invoked
        /// </summary>
        protected override void OnPostExecute(Object result)
        {
            base.OnPostExecute(result);
        }

        protected override void OnPostExecute(List<RssMessageModel> messages)
        {
            base.OnPostExecute(messages);

            _refreshLayout.Refreshing = false;

            if (messages?.Any() == true)
            {
                _activity.ShowValidData();
            }
            else
            {
                _activity.ShowNotValidError("No data");
            }

            var adapter = new RssMessageAdapter(messages ?? new List<RssMessageModel>(), _activity);
            _recyclerView.SetAdapter(adapter);
        }
    }
}