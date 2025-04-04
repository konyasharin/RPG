using R3;
using UnityEngine;

namespace Actors.Stats
{
    public abstract class Stat<T> where T : StatSettings
    {
        public ReadOnlyReactiveProperty<int> CurrentStatReactive => CurrentStatReactiveInternal;
        public float Percentage => (float)CurrentStatReactiveInternal.CurrentValue / Settings.maxValue;
        
        protected readonly T Settings;
        protected readonly ReactiveProperty<int> CurrentStatReactiveInternal;
        
        protected Stat(T settings)
        {
            Settings = settings;
            CurrentStatReactiveInternal = new ReactiveProperty<int>(settings.maxValue);
        }

        public virtual void Add(int value)
        {
            if (value >= 0) 
                CurrentStatReactiveInternal.Value = Clamp(CurrentStatReactiveInternal.CurrentValue + value);
        }

        public virtual void Reduce(int value)
        {
            if (value >= 0) 
                CurrentStatReactiveInternal.Value = Clamp(CurrentStatReactiveInternal.CurrentValue - value);
        }

        public virtual void Set(int value)
        {
            CurrentStatReactiveInternal.Value = Clamp(value);
        }

        private int Clamp(int value)
        {
            return Mathf.Clamp(value, 0, Settings.maxValue);
        }
    }
}