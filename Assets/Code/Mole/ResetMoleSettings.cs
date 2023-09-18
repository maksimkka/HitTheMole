using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Mole
{
    public class ResetMoleSettings : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ReturnToPoolRequest>> _returnToPoolRequestFilter = default;
        private readonly EcsPoolInject<MoleData> _moleData = default;
        public void Run(IEcsSystems systems)
        {
            ResetMole();
        }

        private void ResetMole()
        {
            foreach (var entity in _returnToPoolRequestFilter.Value)
            {
                ref var returnToPoolRequest = ref _returnToPoolRequestFilter.Pools.Inc1.Get(entity);
                ref var moleData = ref _moleData.Value.Get(returnToPoolRequest.EntityReturnedObject);
                moleData.CurrentLifeTime = 0;
                moleData.CurrentHealth = moleData.DefaultHealth;
            }
        }
    }
}