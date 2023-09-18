using System.Text;
using Code.Main;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.GameTimer
{
    public class TimerInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<TimerData> _timerData = default;
        private readonly EcsCustomInject<TimerSettings> _timerSettings = default;
        private readonly EcsCustomInject<GameSettings> _gameSettings = default;
        public void Init(IEcsSystems systems)
        {
            var entity = systems.GetWorld().NewEntity();
            ref var timerData = ref _timerData.Value.Add(entity);
            timerData.timerStringBuilder = new StringBuilder();
            timerData.TimerText = _timerSettings.Value.TimerText;
            timerData.DefaultTime = _gameSettings.Value.StartTimer;
            timerData.CurrentTime = _gameSettings.Value.StartTimer;

            timerData.timerStringBuilder.Clear();
            timerData.timerStringBuilder.Append(timerData.DefaultTime);
            timerData.TimerText.text = timerData.timerStringBuilder.ToString();
        }
    }
}