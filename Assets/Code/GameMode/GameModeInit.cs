using Code.GameTimer;
using Code.Health;
using Code.HUD;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.GameMode
{
    public class GameModeInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<TimeModeMarker> _timeModeMarker = default;
        private readonly EcsFilterInject<Inc<TimerData>> _timerDataFilter = default;
        private readonly EcsFilterInject<Inc<HealthData>> _healthDataFilter = default;
        private readonly EcsPoolInject<SurvivalModeMarker> _survivalModeMarker = default;
        private readonly EcsCustomInject<GameModeSettings> _gameModeSettings = default;

        public void Init(IEcsSystems systems)
        {
            _gameModeSettings.Value.TimeModeButton.onClick.AddListener(SelectTimeMode);
            _gameModeSettings.Value.SurvivorModeButton.onClick.AddListener(SelectSurvivorMode);
        }

        private void SelectTimeMode()
        {
            foreach (var entity in _timerDataFilter.Value)
            {
                ref var timerData = ref _timerDataFilter.Pools.Inc1.Get(entity);
                timerData.TimerText.gameObject.SetActive(true);
                _timeModeMarker.Value.Add(entity);
                ScreenSwitcher.ShowScreen(ScreenType.Game);
                Time.timeScale = 1;
            }
        }

        private void SelectSurvivorMode()
        {
            foreach (var entity in _healthDataFilter.Value)
            {
                ref var healthData = ref _healthDataFilter.Pools.Inc1.Get(entity);
                healthData.HealthText.gameObject.SetActive(true);
                _survivalModeMarker.Value.Add(entity);
                ScreenSwitcher.ShowScreen(ScreenType.Game);
                Time.timeScale = 1;
            }
        }
    }
}