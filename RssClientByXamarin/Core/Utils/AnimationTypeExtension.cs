using System;
using System.Collections.Generic;
using Core.Configuration.Settings;
using Core.Resources;
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
                    return Strings.AnimationTypeNone;
                case AnimationType.Fade:
                    return Strings.AnimationTypeFade;
                case AnimationType.Explode:
                    return Strings.AnimationTypeExplode;
                case AnimationType.Bottom:
                    return Strings.AnimationTypeBottom;
                case AnimationType.Top:
                    return Strings.AnimationTypeTop;
                case AnimationType.Left:
                    return Strings.AnimationTypeLeft;
                case AnimationType.Right:
                    return Strings.AnimationTypeRight;
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
