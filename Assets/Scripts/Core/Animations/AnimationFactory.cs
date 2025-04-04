namespace Core.Animations
{
    public static class AnimationFactory
    {
        public static AnimationInfo<T> Create<T>(float from, float to, float duration, T type)
        {
            return new AnimationInfo<T>
            {
                From = from,
                To = to,
                Duration = duration,
                Type = type
            };
        }
    }
}