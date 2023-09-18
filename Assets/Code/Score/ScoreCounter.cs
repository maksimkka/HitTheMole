using Code.HUD;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Score
{
    public class ScoreCounter : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ScoreData, ChangeScoreRequest>> _scoreData = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _scoreData.Value)
            {
                ref var scoreData = ref _scoreData.Pools.Inc1.Get(entity);
                ref var changeScore = ref _scoreData.Pools.Inc2.Get(entity);
                var calculatedScore = changeScore.MoleHealth * scoreData.ScoreMultiplier;
                scoreData.CurrentScore += calculatedScore;

                if (scoreData.CurrentScore >= scoreData.ScoreToWin)
                {
                    scoreData.CurrentScore = scoreData.ScoreToWin;
                    Time.timeScale = 0;
                    ScreenSwitcher.ShowScreen(ScreenType.Victory);
                }

                UpdateScoreDisplay(ref scoreData);

                _scoreData.Pools.Inc2.Del(entity);
            }
        }
        
        private void UpdateScoreDisplay(ref ScoreData scoreData)
        {
            scoreData.ScoreStringBuilder.Clear();
            scoreData.ScoreStringBuilder.Append(scoreData.CurrentScore);
            scoreData.ScoreStringBuilder.Append(" / ");
            scoreData.ScoreStringBuilder.Append(scoreData.ScoreToWin);

            scoreData.ScoreText.text = scoreData.ScoreStringBuilder.ToString();
        }
    }
}