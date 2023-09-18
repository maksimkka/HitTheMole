using Code.Effects;
using Code.GameMode;
using Code.Health;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Mole
{
    public class TimerForReturningToPool : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoleData>> _moleDataFilter = default;
        private readonly EcsFilterInject<Inc<HealthData, SurvivalModeMarker>, Exc<ChangeHealthRequest>> _healthFilter = default;
        private readonly EcsPoolInject<ReturnToPoolRequest> _returnToPoolRequest = default;
        private readonly EcsPoolInject<ChangeHealthRequest> _changeHealthRequest = default;
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
                    ParticleSystemController.GetParticle(EffectType.MoleHid, moleData.MoleGameObject.transform.position);
                    SendChangeHealthRequest(moleData.DefaultHealth);
                    SendRequest(entity);
                }
            }
        }

        private void SendRequest(int entity)
        {
            var requestEntity = _systems.GetWorld().NewEntity();
            ref var returnToPoolRequest = ref _returnToPoolRequest.Value.Add(requestEntity);
            
            returnToPoolRequest.EntityReturnedObject = entity;
        }
        
        private void SendChangeHealthRequest(int moleHealth)
        {
            foreach (var entity in _healthFilter.Value)
            {
                ref var changeScoreRequest = ref _changeHealthRequest.Value.Add(entity);
                changeScoreRequest.MoleHealth = moleHealth;
            }
        }
    }
}