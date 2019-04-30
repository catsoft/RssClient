using System;
using System.Collections.Generic;
using Core.Configuration.Settings;
using JetBrains.Annotations;

namespace Core.Extensions
{
    public static class AnimationSpeedExtension
    {
        [CanBeNull]
        public static string ToLocaleString(this AnimationSpeed animationSpeed)
        {
            switch (animationSpeed)
            {
                case AnimationSpeed.X025:
                    return "0.25x";
                case AnimationSpeed.X033:
                    return "0.33x";
                case AnimationSpeed.X05:
                    return "0.5x";
                case AnimationSpeed.X:
                    return "x";
                case AnimationSpeed.X2:
                    return "2x";
                case AnimationSpeed.X3:
                    return "3x";
                case AnimationSpeed.X4:
                    return "4x";
                case AnimationSpeed.Max:
                    return "max";
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationSpeed), animationSpeed, null);
            }
        }
        
        public static List<AnimationSpeed> GetAnimationSpeeds()
        {
            return new List<AnimationSpeed>()
            {
                AnimationSpeed.X025,
                AnimationSpeed.X033,
                AnimationSpeed.X05,
                AnimationSpeed.X,
                AnimationSpeed.X2,
                AnimationSpeed.X3, 
                AnimationSpeed.X4, 
                AnimationSpeed.Max,
            };
        }
    }
}
