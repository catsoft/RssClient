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
                case AnimationType.FromLeftToRight:
                    return "From left to right";
                case AnimationType.FromRightToLeft:
                    return "From right to left";
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
            }
        }
    }
}