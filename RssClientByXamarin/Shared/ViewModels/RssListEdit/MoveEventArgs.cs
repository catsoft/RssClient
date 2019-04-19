namespace Shared.ViewModels.RssListEdit
{
    public class MoveEventArgs<TItem>
    {
        public MoveEventArgs(TItem item, int fromPosition, int position)
        {
            Item = item;
            FromPosition = fromPosition;
            ToPosition = position;
        }

        public TItem Item { get; }
        public int FromPosition { get; }
        public int ToPosition { get; }
    }
}
