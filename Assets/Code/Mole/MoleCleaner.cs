using Code.Spawner;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Mole
{
    public class MoleCleaner : IEcsRunSystem
    {
        //private readonly EcsFilterInject<Inc<MoleData>> _moleDataFilter = default;
        private readonly EcsFilterInject<Inc<MoleSpawnerData>> _moleSpawnerDataFilter = default;
        private readonly EcsFilterInject<Inc<ReturnToPoolRequest>> _returnToPoolRequestFilter = default;
        private readonly EcsPoolInject<MoleData> _moleData = default;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _moleSpawnerDataFilter.Value)
            {
                ref var moleSpawner = ref _moleSpawnerDataFilter.Pools.Inc1.Get(entity);
                ReturnToPool(ref moleSpawner);
                //moleSpawner.MolesPool.ReturnObject(moleData.MoleGameObject.GetComponent<Collider>());
            }
        }

        private void ReturnToPool(ref MoleSpawnerData moleSpawner)
        {
            foreach (var entity in _returnToPoolRequestFilter.Value)
            {
                ref var returnToPoolRequest = ref _returnToPoolRequestFilter.Pools.Inc1.Get(entity);
                ref var moleData = ref _moleData.Value.Get(returnToPoolRequest.EntityReturnedObject);
                
                moleSpawner.MolesPool.ReturnObject(moleData.MoleGameObject.GetComponent<Collider>());
                _returnToPoolRequestFilter.Pools.Inc1.Del(entity);
            }
        }
    }
}