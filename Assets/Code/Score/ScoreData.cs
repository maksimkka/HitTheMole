using System.Text;
using TMPro;

namespace Code.Score
{
    public struct ScoreData
    {
        public TextMeshProUGUI ScoreText;
        public StringBuilder ScoreStringBuilder;
        public int ScoreMultiplier;
        public int CurrentScore;
        public int ScoreToWin;
    }
}