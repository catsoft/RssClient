using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Newtonsoft.Json;
using RssClient.App.Base;
using RssClient.App.Rss.Detail;
using RssClient.App.Rss.Edit;
using Shared.App.Base.Database;
using Shared.App.Rss;
using Shared.App.Rss.Edit;
using Shared.App.Rss.Remove;

namespace RssClient.App.Rss.List
{
    public class RssListAdapter : RecyclerView.Adapter
    {
        private const string DeletePositiveTitle = "Yes";
        private const string DeleteNegativeTitle = "No";
        private const string DeleteTitle = "Ara you sure?";

        private readonly Activity _activity;

        public RssListAdapter(IEnumerable<RssModel> items, Activity activity)
        {
            _activity = activity;
            Items = items.OrderByDescending(w => w.CreationTime).ToList();
        }

        public override int ItemCount => Items.Count;
        public List<RssModel> Items { get; }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = Items[position];

            if (holder is RssListViewHolder rssListViewHolder)
            {
                rssListViewHolder.TitleTextView.Text = item.Name;
                rssListViewHolder.SubtitleTextView.Text = item.Rss;
                rssListViewHolder.Item = item;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rss_list_item, parent, false);
            var holder = new RssListViewHolder(view);

            view.Clickable = true;
            view.Click += (sender, args) => { OpenDetailActivity(holder.Item); };

            view.LongClick += (sender, args) => { ItemLongClick(holder.Item, sender); };
            return holder;
        }


        private void ItemLongClick(RssModel holderItem, object sender)
        {
            var menu = new PopupMenu(_activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holderItem, eventArgs);
            menu.Inflate(Resource.Menu.rss_list_item);
            menu.Show();
        }

        private void MenuClick(RssModel holderItem, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.rss_item_edit_action:
                    EditItem(holderItem);
                    break;
                case Resource.Id.rss_item_remove_action:
                    DeleteItem(holderItem);
                    break;
            }
        }

        private void EditItem(RssModel holderItem)
        {
            var intent = new Intent(_activity, typeof(RssEditActivity));
            intent.PutExtra(RssEditActivity.ItemIntentId, JsonConvert.SerializeObject(holderItem));
            _activity.StartActivityForResult(intent, RssListActivity.EditRequestCode);
        }

        private void DeleteItem(RssModel holderItem)
        {
            var builder = new AlertDialog.Builder(_activity);
            builder.SetPositiveButton(DeletePositiveTitle, (sender, args) =>
            {
                var request = new RemoveRssRequest(holderItem);
                var @delegate = _activity.GetCommandDelegate<RemoveRssResponse>(OnSuccessRemove);
                var command = new RemoveRssCommand(LocalDb.Instance, @delegate);

                command.Execute(request);
            });
            builder.SetNegativeButton(DeleteNegativeTitle, (sender, args) => { });
            builder.SetTitle(DeleteTitle);
            builder.Show();
        }

        private void OnSuccessRemove(RemoveRssResponse obj)
        {
            var index = Items.IndexOf(obj.Model);

            Items.RemoveAt(index);
            NotifyItemRemoved(index);
        }

        private void OpenDetailActivity(RssModel holderItem)
        {
            var intent = new Intent(_activity, typeof(RssDetailActivity));
            intent.PutExtra(RssDetailActivity.ItemIntentId, JsonConvert.SerializeObject(holderItem));
            _activity.StartActivity(intent);
        }
    }
}