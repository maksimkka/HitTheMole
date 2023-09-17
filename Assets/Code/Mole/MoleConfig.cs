using UnityEngine;

namespace Code.Mole
{
    [System.Serializable]
    public class MoleConfig
    {
        [field: SerializeField] public string MoleName { get; private set; }
        [field: SerializeField] public Mesh MolePrefab { get; private set; }
        [field: SerializeField] public Material Color { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public float Lifetime { get; private set; }
    }
}