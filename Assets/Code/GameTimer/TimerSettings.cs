using TMPro;
using UnityEngine;

namespace Code.GameTimer
{
    [DisallowMultipleComponent]
    public class TimerSettings : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI TimerText { get; private set; }
    }
}