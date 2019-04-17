namespace Shared.ViewModels.RssListEdit
{
    public class MoveEventArgs<TItem>
    {
        public TItem Item { get; }
        public int FromPosition { get; }
        public int ToPosition { get; }

        public MoveEventArgs(TItem item, int fromPosition, int position)
        {
            Item = item;
            FromPosition = fromPosition;
            ToPosition = position;
        }
    }
}