using System;

namespace Actors.Stats
{
    public abstract class Stat<T> where T : StatSettings
    {
        public float CurrentValue { get; protected set; }
        public float Percentage => CurrentValue / Settings.maxValue;
        public event Action OnChange;
        
        protected readonly T Settings;
        
        protected Stat(T settings)
        {
            Settings = settings;
            CurrentValue = settings.maxValue;
        }

        public virtual void Add(float value)
        {
            CurrentValue += value;
            OnUpdate();
        }

        public virtual void Reduce(float value)
        {
            CurrentValue -= value;
            OnUpdate();
        }

        public virtual void Set(float value)
        {
            CurrentValue = value;
            OnUpdate();
        }
        
        private void OnUpdate()
        {
            OnCheckValue();
            OnChange?.Invoke();
        }

        protected virtual void OnCheckValue()
        {
            if (CurrentValue <= 0)
            {
                Set(0);
            }

            if (CurrentValue > Settings.maxValue)
            {
                CurrentValue = Settings.maxValue;
            }
        }
    }
}