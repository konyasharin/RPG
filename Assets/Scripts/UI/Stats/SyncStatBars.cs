using System;
using UnityEngine;

namespace UI.Stats
{
    public class SyncStatBars: MonoBehaviour
    {
        [SerializeField] private StatBar[] statBarsByDecreasePlayingOrder;

        private void Start()
        {
            foreach (var statBar in statBarsByDecreasePlayingOrder)
            {
                
            }
        }
        
        private void HandleAnimation(){}
    }
}