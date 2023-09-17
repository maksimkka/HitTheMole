using Code.Main;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Score
{
    public class ScoreInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<ScoreData> _scoreData = default;
        private readonly EcsCustomInject<GameSettings> _gameSettings = default;
        private readonly EcsCustomInject<ScoreSettings> _scoreSettings = default;
        public void Init(IEcsSystems systems)
        {
            var entity = systems.GetWorld().NewEntity();
            ref var scoreData = ref _scoreData.Value.Get(entity);
            scoreData.CurrentScore = 0;
            scoreData.ScoreToWin = _gameSettings.Value.NumberScoreToWin;
            scoreData.ScoreText = _scoreSettings.Value.ScoreText;
            scoreData.ScoreText.text = scoreData.CurrentScore.ToString();
        }
    }
}