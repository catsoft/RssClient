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
                    return "Fade";
                case AnimationType.ExitFadeEnterFromBottom:
                    return "Exit fade enter from bottom";
                case AnimationType.ExitToBottomEnterFromBottom:
                    return "Slide";
                case AnimationType.ExitToBottomEnterFade:
                    return "Exit to bottom enter fade";
            }
            
            throw new NotImplementedException(nameof(AnimationTypeExtension) + nameof(ToLocaleString));
        }
    }
}