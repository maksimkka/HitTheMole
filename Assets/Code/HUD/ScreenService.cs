using System.Collections.Generic;
using UnityEngine;

namespace Code.HUD
{
    [DisallowMultipleComponent]
    public class ScreenService : MonoBehaviour
    {
        [field: SerializeField] public List<ScreenView> screens { get; private set; }
    }
}