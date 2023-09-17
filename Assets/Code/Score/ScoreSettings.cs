using TMPro;
using UnityEngine;

namespace Code.Score
{
    [DisallowMultipleComponent]
    public class ScoreSettings : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI ScoreText;
    }
}