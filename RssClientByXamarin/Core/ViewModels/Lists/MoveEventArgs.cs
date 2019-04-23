namespace Core.ViewModels.Lists
{
    public class MoveEventArgs
    {
        public MoveEventArgs(int fromPosition, int position)
        {
            FromPosition = fromPosition;
            ToPosition = position;
        }

        public int FromPosition { get; }

        public int ToPosition { get; }
    }
}
