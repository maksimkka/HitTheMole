using System.Text;
using TMPro;

namespace Code.GameTimer
{
    public struct TimerData
    {
        public StringBuilder timerStringBuilder;
        public TextMeshProUGUI TimerText;
        public int DefaultTime;
        public float CurrentTime;
    }
}