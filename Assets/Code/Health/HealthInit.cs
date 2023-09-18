using System.Text;
using Code.Main;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Health
{
    public class HealthInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<HealthData> _healthData = default;
        private readonly EcsCustomInject<GameSettings> _gameSettings = default;
        private readonly EcsCustomInject<HealthSettings> _healthSettings = default;
        public void Init(IEcsSystems systems)
        {
            var entity = systems.GetWorld().NewEntity();
            ref var healthData = ref _healthData.Value.Add(entity);

            healthData.HealthStringBuilder = new StringBuilder();
            healthData.HealthDamagedMultiplier = _gameSettings.Value.DamageMultiplier;
            healthData.DefaultHealth = _gameSettings.Value.StartPlayerHealth;
            healthData.CurrentHealth = _gameSettings.Value.StartPlayerHealth;
            healthData.HealthText = _healthSettings.Value.HealthText;

            healthData.HealthStringBuilder.Clear();
            healthData.HealthStringBuilder.Append(healthData.DefaultHealth);
            healthData.HealthText.text = healthData.HealthStringBuilder.ToString();
        }
    }
}