using Code.Pools;
using UnityEngine;

namespace Code.Spawner
{
    public struct MoleSpawnerData
    {
        public ObjectPool<Collider> MolesPool;
        public float SpawnDelay;
        public float CurrentTimeUntilSpawn;
    }
}