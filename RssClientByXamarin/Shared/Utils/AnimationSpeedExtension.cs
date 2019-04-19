#region

using System;
using JetBrains.Annotations;
using Shared.Configuration.Settings;

#endregion

namespace Shared.Utils
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
    }
}
