using System;

namespace Core.Configuration.Settings
{
    public class AppConfiguration
    {
        public StartPage StartPage { get; set; } = StartPage.RssList;

        public MessagesViewer MessagesViewer { get; set; } = MessagesViewer.App;

        /// <summary>
        ///     In millisecond
        /// </summary>
        public int DefaultAnimationTime { get; set; } = 235;

        public AnimationSpeed AnimationSpeed { get; set; } = AnimationSpeed.X;

        public AnimationType AnimationEnter { get; set; } = AnimationType.Right;
        
        public AnimationType AnimationExit { get; set; } = AnimationType.Left;

        public bool IsDelay { get; set; } = true;

        public AppTheme AppTheme { get; set; } = AppTheme.Default;

        public bool HideReadMessages { get; set; }

        public bool LoadAndShowImages { get; set; } = true;

        public bool IsShowPush { get; set; } = true;

        public bool IsAutoUpdate { get; set; } = true;

        /// <summary>
        /// In milliseconds
        /// </summary>
        public int AutoUpdateInterval { get; set; } = 1000 * 60 * 15;

        public int CalculateAnimationTime()
        {
            var defaultTime = DefaultAnimationTime;
            switch (AnimationSpeed)
            {
                case AnimationSpeed.X025:
                    return defaultTime * 4;
                case AnimationSpeed.X033:
                    return defaultTime * 3;
                case AnimationSpeed.X05:
                    return defaultTime * 2;
                case AnimationSpeed.X:
                    return defaultTime;
                case AnimationSpeed.X2:
                    return defaultTime / 2;
                case AnimationSpeed.X3:
                    return defaultTime / 3;
                case AnimationSpeed.X4:
                    return defaultTime / 4;
                case AnimationSpeed.Max:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
