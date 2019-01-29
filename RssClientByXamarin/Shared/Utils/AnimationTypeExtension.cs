using System;
using Shared.Configuration;

namespace Shared.Utils
{
    public static class AnimationTypeExtension
    {
        public static string ToLocaleString(this AnimationType animationType)
        {
            switch (animationType)
            {
                // TODO вернуть до удобочитаемого вида
                case AnimationType.None:
                    return "None";
                case AnimationType.OnlyFade:
                    return "OnlyFade";
                case AnimationType.ExitFadeEnterFromBottom:
                    return "ExitFade_EnterFromBottom";
                case AnimationType.ExitToBottomEnterFromBottom:
                    return "OnlySlide";
                case AnimationType.ExitToBottomEnterFade:
                    return "ExitToBottom_EnterFade";
            }
            
            throw new NotImplementedException(nameof(AnimationTypeExtension) + nameof(ToLocaleString));
        }
    }
}