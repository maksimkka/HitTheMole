using UnityEngine;
using UnityEngine.UI;

namespace Code.GameMode
{
    public class GameModeSettings : MonoBehaviour
    {
        [field: SerializeField] public Button TimeModeButton { get; private set; }
        [field: SerializeField] public Button SurvivorModeButton { get; private set; }
    }
}