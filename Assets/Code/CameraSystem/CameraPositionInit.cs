using Cinemachine;
using Code.Grid;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.CameraSystem
{
    public class CameraPositionInit : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CenterGridRequest>> _centerGridRequest = default; 
        private readonly EcsCustomInject<CameraFollowSettings> _cameraFollowSettings = default;
        private IEcsSystems _systems;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _centerGridRequest.Value)
            {
                ref var centerGridRequest = ref _centerGridRequest.Pools.Inc1.Get(entity);
                var newPositionY = centerGridRequest.GridSize * _cameraFollowSettings.Value.PositionMultiplier;

                var transpose = _cameraFollowSettings.Value.FollowCamera.GetCinemachineComponent<CinemachineTransposer>();
                _cameraFollowSettings.Value.CenterGridPoint.transform.position = centerGridRequest.CenterPosition;
                transpose.m_FollowOffset.y = newPositionY;
                _centerGridRequest.Pools.Inc1.Del(entity);
            }
        }
    }
}