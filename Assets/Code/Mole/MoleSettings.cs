using UnityEngine;

namespace Code.Mole
{
    [DisallowMultipleComponent]
    public class MoleSettings : MonoBehaviour
    {
        [field: SerializeField] public int HP { get; private set; }
        [field: SerializeField] public float lifetime { get; private set; }
    }
}