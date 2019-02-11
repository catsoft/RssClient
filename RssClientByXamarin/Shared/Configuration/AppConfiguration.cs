using System;

namespace Shared.Configuration
{
    public class AppConfiguration
    {
        public StartPage StartPage { get; set; } = StartPage.RssList;
        public MessagesViewer MessagesViewer { get; set; } = MessagesViewer.App;

        /// <summary>
        ///  In millisecond
        /// </summary>
        public int DefaultAnimationTime { get; set; } = 200;

        public AnimationSpeed AnimationSpeed { get; set; } = AnimationSpeed.X;
        public AnimationType AnimationType { get; set; } = AnimationType.OnlyFade;

        public AppTheme AppTheme { get; set; } = AppTheme.Default;

        public bool HideReadMessages { get; set; }
        public bool LoadAndShowImages { get; set; } = true;

        public int GetCalculationAnimationTime()
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