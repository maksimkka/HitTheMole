using TMPro;
using UnityEngine;

namespace Code.Health
{
    [DisallowMultipleComponent]
    public class HealthSettings : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI HealthText { get; private set; }
    }
}