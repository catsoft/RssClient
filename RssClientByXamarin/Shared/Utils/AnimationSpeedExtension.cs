using System;
using Shared.Configuration;

namespace Shared.Utils
{
    public static class AnimationSpeedExtension
    {
        public static string ToLocaleString(this AnimationSpeed animationSpeed)
        {
            switch (animationSpeed)
            {
                case AnimationSpeed.x0_25:
                    return "0.25x";
                case AnimationSpeed.x0_5:
                    return "0.5x";
                case AnimationSpeed.x:
                    return "x";
                case AnimationSpeed.x2:
                    return "2x";
                case AnimationSpeed.x4:
                    return "4x";
                case AnimationSpeed.x8:
                    return "8x";
            }
            
            throw new NotImplementedException(nameof(AnimationSpeedExtension) + nameof(ToLocaleString));
        }
    }
}