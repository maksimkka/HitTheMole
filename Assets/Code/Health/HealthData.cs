using System.Text;
using TMPro;

namespace Code.Health
{
    public struct HealthData
    {
        public TextMeshProUGUI HealthText;
        public StringBuilder HealthStringBuilder;
        public int HealthDamagedMultiplier;
        public int DefaultHealth;
        public int CurrentHealth;
    }
}