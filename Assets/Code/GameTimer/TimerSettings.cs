using TMPro;
using UnityEngine;

namespace Code.GameTimer
{
    public class TimerSettings : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI TimerText { get; private set; }
    }
}