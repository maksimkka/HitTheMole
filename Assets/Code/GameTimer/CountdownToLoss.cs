using Code.GameMode;
using Code.HUD;
using Code.Logger;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.GameTimer
{
    public class CountdownToLoss : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TimerData, TimeModeMarker>> _timerData = default;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _timerData.Value)
            {
                ref var timerData = ref _timerData.Pools.Inc1.Get(entity);
                
                timerData.CurrentTime -= Time.deltaTime;

                if (timerData.CurrentTime <= 0f)
                {
                    timerData.CurrentTime = 0f;
                    Time.timeScale = 0;
                    ScreenSwitcher.ShowScreen(ScreenType.Defeat);
                }

                int seconds = Mathf.FloorToInt(timerData.CurrentTime);

                timerData.timerStringBuilder.Clear();
                timerData.timerStringBuilder.Append(seconds);
                timerData.TimerText.text = timerData.timerStringBuilder.ToString();
            }
        }
    }
}