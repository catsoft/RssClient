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
                case AnimationType.Fade:
                    return "Fade";
                case AnimationType.From_left:
                    return "From left";
                case AnimationType.From_right:
                    return "From right";
                case AnimationType.From_bottom:
                    return "From bottom";
            }
            
            throw new NotImplementedException(nameof(AnimationTypeExtension) + nameof(ToLocaleString));
        }
    }
}