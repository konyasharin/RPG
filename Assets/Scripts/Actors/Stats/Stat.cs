using System;
using R3;
using UnityEngine;

namespace Actors.Stats
{
    public abstract class Stat<T> where T : StatSettings
    {
        public ReadOnlyReactiveProperty<int> CurrentStat => _currentStat;
        public float Percentage => (float)_currentStat.CurrentValue / Settings.maxValue;
        
        protected readonly T Settings;
        private readonly ReactiveProperty<int> _currentStat;
        
        protected Stat(T settings)
        {
            Settings = settings;
            _currentStat = new ReactiveProperty<int>(settings.maxValue);
        }

        public virtual void Add(int value)
        {
            _currentStat.Value += value;
            OnUpdate();
        }

        public virtual void Reduce(int value)
        {
            _currentStat.Value -= value;
            OnUpdate();
        }

        public virtual void Set(int value)
        {
            _currentStat.Value = value;
            OnUpdate();
        }
        
        private void OnUpdate()
        {
            OnCheckValue();
        }

        protected virtual void OnCheckValue()
        {
            if (_currentStat.CurrentValue < 0)
            {
                Set(0);
            }

            if (_currentStat.CurrentValue > Settings.maxValue)
            {
                _currentStat.Value = Settings.maxValue;
            }
        }
    }
}