using Code.Logger;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Mole
{
    public class MoleHitter : IEcsRunSystem
    {
        private readonly EcsPoolInject<ReturnToPoolRequest> _returnToPoolRequestFilter = default;
        private readonly EcsFilterInject<Inc<MoleData>> _moleDataFilter = default;
        private readonly EcsCustomInject<Camera> _camera;
        private IEcsSystems _systems;

        public void Run(IEcsSystems systems)
        {
            _systems = systems;
            FindingObjectUsingRay();
        }

        private void FindingObjectUsingRay()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _camera.Value.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out var hit)) return;
                if (hit.transform.gameObject.layer != Layers.Mole) return;

                HitHandler(hit.transform.gameObject.GetHashCode());
            }
        }

        private void HitHandler(int hashCodeHitObject)
        {
            foreach (var entity in _moleDataFilter.Value)
            {
                ref var moleData = ref _moleDataFilter.Pools.Inc1.Get(entity);
                
                if(moleData.MoleGameObject.GetHashCode() != hashCodeHitObject) continue;

                moleData.CurrentHealth -= 1;

                if (moleData.CurrentHealth <= 0)
                {
                    var collider = moleData.MoleGameObject.GetComponent<Collider>();
                    $"{collider.gameObject.name}".Colored(Color.cyan).Log();
                    SendRequest(entity);
                }
            }
        }
        
        private void SendRequest(int entity)
        {
            var requestEntity = _systems.GetWorld().NewEntity();
            ref var returnToPoolRequest = ref _returnToPoolRequestFilter.Value.Add(requestEntity);
            returnToPoolRequest.EntityReturnedObject = entity;
        }
    }
}