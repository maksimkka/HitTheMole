using Code.Mole;
using UnityEngine;

namespace Code.Spawner
{
    [DisallowMultipleComponent]
    public class MolesSpawnerSettings : MonoBehaviour
    {
        [field: SerializeField] public MoleConfig[] MoleConfigs { get; private set; }
        [field: SerializeField] public GameObject DefaultSpawnObject { get; private set; }
        [field: SerializeField, Range(0.5f, 5f)] public float SpawnDelay { get; private set; }
    }
}