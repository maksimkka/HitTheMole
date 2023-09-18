using Code.HUD;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Health
{
    public class HealthChanger : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HealthData, ChangeHealthRequest>> _healthDataFilter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _healthDataFilter.Value)
            {
                ref var healthData = ref _healthDataFilter.Pools.Inc1.Get(entity);
                ref var changeHealth = ref _healthDataFilter.Pools.Inc2.Get(entity);
                var calculatedHealth = changeHealth.MoleHealth * healthData.HealthDamagedMultiplier;
                healthData.CurrentHealth -= calculatedHealth;

                if (healthData.CurrentHealth <= 0)
                {
                    healthData.CurrentHealth = 0;
                    Time.timeScale = 0;
                    ScreenSwitcher.ShowScreen(ScreenType.Defeat);
                }

                UpdateHealthDisplay(ref healthData);

                _healthDataFilter.Pools.Inc2.Del(entity);
            }
        }
        
        private void UpdateHealthDisplay(ref HealthData healthData)
        {
            healthData.HealthStringBuilder.Clear();
            healthData.HealthStringBuilder.Append(healthData.CurrentHealth);

            healthData.HealthText.text = healthData.HealthStringBuilder.ToString();
        }
    }
}