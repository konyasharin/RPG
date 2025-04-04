using System;
using UnityEngine;

namespace Core.Animations
{
    public static class NumericTweener
    {
        public static Func<AnimationInfo, float, float> GetInterpolationByAnimationType(NumericAnimationType type)
        {
            return type switch
            {
                NumericAnimationType.Linear => GetLinearInterpolation,
                NumericAnimationType.EaseOut => GetEaseOutInterpolation,
                NumericAnimationType.EaseIn => GetEaseInInterpolation,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private static float GetLinearInterpolation(AnimationInfo info, float time)
        {
            return Mathf.Lerp(info.From, info.To, time / info.Duration);
        }

        private static float GetEaseOutInterpolation(AnimationInfo info, float time)
        {
            return Mathf.Lerp(info.From, info.To, 1 - Mathf.Pow(1 - (time / info.Duration), 2));
        }
        
        private static float GetEaseInInterpolation(AnimationInfo info, float time)
        {
            return Mathf.Lerp(info.From, info.To, Mathf.Pow(time / info.Duration, 2));
        }
    }
}