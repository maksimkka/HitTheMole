using Code.Logger;
using Code.Mole;
using Code.Pools;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Spawner
{
    public class MoleSpawnerInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<MoleData> _moleData = default;
        private readonly EcsPoolInject<MoleSpawnerData> _moleSpawnerData = default;
        private readonly EcsCustomInject<MolesSpawnerSettings> _spawnerSettings = default;
        private readonly EcsCustomInject<PoolCommonParent> _poolCommonParent = default;
        private EcsWorld _world;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            var entity = _world.NewEntity();
            ref var moleSpawnerData = ref _moleSpawnerData.Value.Add(entity);
            var molePrefab = _spawnerSettings.Value.DefaultSpawnObject.GetComponent<Collider>();

            var molesPool = new ObjectPool<Collider>(molePrefab, _poolCommonParent.Value.transform, 45, 
                initWithInEcs: InitMoles);

            moleSpawnerData.MolesPool = molesPool;
            moleSpawnerData.SpawnDelay = _spawnerSettings.Value.SpawnDelay;
            
        }

        private void InitMoles(Collider moleCollider)
        {
            var entity = _world.NewEntity();
            ref var moleData = ref _moleData.Value.Add(entity);
            int randomIndex = Random.Range(0, _spawnerSettings.Value.MoleConfigs.Length);
            var config = _spawnerSettings.Value.MoleConfigs[randomIndex];
            moleCollider.name = config.MoleName;
            moleCollider.GetComponent<MeshFilter>().mesh = config.MolePrefab;
            moleCollider.GetComponent<MeshRenderer>().material = config.Color;
            moleData.MoleGameObject = moleCollider.gameObject;
            moleData.DefaultLifeTime = config.Lifetime;
            moleData.CurrentHealth = config.Health;
            moleData.DefaultHealth = config.Health;
        }
    }
}