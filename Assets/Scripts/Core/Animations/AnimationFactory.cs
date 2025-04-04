namespace Core.Animations
{
    public static class AnimationFactory
    {
        public static AnimationInfo Create(float from, float to, float duration, NumericAnimationType type)
        {
            return new AnimationInfo
            {
                From = from,
                To = to,
                Duration = duration,
                Type = type
            };
        }
    }
}