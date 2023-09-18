using UnityEngine;
using UnityEngine.UI;

namespace Code.GameMode
{
    [DisallowMultipleComponent]
    public class GameModeSettings : MonoBehaviour
    {
        [field: SerializeField] public Button TimeModeButton { get; private set; }
        [field: SerializeField] public Button SurvivorModeButton { get; private set; }
    }
}