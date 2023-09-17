using UnityEngine;

namespace Code.Main
{
    public class GameSettings : MonoBehaviour
    {
        [field: SerializeField, Range(1, 10)] public int DamageMultiplierReceived { get; private set; }
        [field: SerializeField, Min(1)] public int StartPlayerHealth { get; private set; }
        [field: SerializeField, Min(1)] public int NumberScoreToWin { get; private set; }
        [field: SerializeField, Min(1)] public int StartTimer { get; private set; }
    }
}