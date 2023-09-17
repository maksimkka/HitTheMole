using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Mole
{
    public class TimerForReturningToPool : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoleData>> _moleDataFilter = default;
        //private readonly EcsFilterInject<Inc<ReturnToPoolRequest>> _returnToPoolRequest = default;
        private readonly EcsPoolInject<ReturnToPoolRequest> _returnToPoolRequest = default;
        private IEcsSystems _systems;

        public void Run(IEcsSystems systems)
        {
            _systems = systems;
            ReturnToPool();
            
        }
        
        private void ReturnToPool()
        {
            foreach (var entity in _moleDataFilter.Value)
            {
                ref var moleData = ref _moleDataFilter.Pools.Inc1.Get(entity);
                
                if(!moleData.MoleGameObject.activeSelf) continue;

                moleData.CurrentLifeTime += Time.deltaTime;
                if (moleData.CurrentLifeTime >= moleData.DefaultLifeTime)
                {
                    //moleData.CurrentLifeTime = 0;
                    SendRequest(entity);
                }
            }
        }

        private void SendRequest(int entity)
        {
            var requestEntity = _systems.GetWorld().NewEntity();
            ref var returnToPoolRequest = ref _returnToPoolRequest.Value.Add(requestEntity);
            //
            // moleData.CurrentLifeTime = 0;
            // moleData.CurrentHealth = moleData.DefaultHealth;
            returnToPoolRequest.EntityReturnedObject = entity;
            //returnToPoolRequest.EntityReturnedObject = moleData.MoleGameObject.GetComponent<Collider>();
        }
    }
}