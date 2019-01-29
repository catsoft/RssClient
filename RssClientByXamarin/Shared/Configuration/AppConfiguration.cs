namespace Shared.Configuration
{
    public class AppConfiguration
    {
        public StartPage StartPage { get; set; } = StartPage.RssList;
        public MessagesViewer MessagesViewer { get; set; } = MessagesViewer.Browser;

        /// <summary>
        ///  In millisecond
        /// </summary>
        public int DefaultAnimationTime { get; set; } = 200;

        public AnimationSpeed AnimationSpeed { get; set; } = AnimationSpeed.x;
        public AnimationType AnimationType { get; set; } = AnimationType.From_left;
    }
}