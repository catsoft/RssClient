using System;
using System.Collections.Generic;
using Core.Configuration.Settings;
using JetBrains.Annotations;

namespace Core.Utils
{
    public static class AnimationTypeExtension
    {
        [CanBeNull]
        public static string ToLocaleString(this AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.None:
                    return "None";
                case AnimationType.Fade:
                    return "Fade";
                case AnimationType.Explode:
                    return "Explode";
                case AnimationType.Bottom:
                    return "Bottom";
                case AnimationType.Top:
                    return "Top";
                case AnimationType.Left:
                    return "Left";
                case AnimationType.Right:
                    return "Right";
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
            }
        }
        
        [NotNull]
        public static List<AnimationType> GetAnimationTypes()
        {
            return new List<AnimationType>()
            {
                AnimationType.None,
                AnimationType.Fade,
                AnimationType.Explode,
                AnimationType.Left,
                AnimationType.Right,
                AnimationType.Top,
                AnimationType.Bottom
            };
        }
    }
}
