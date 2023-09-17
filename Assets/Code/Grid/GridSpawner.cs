using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Grid
{
    public class GridSpawner : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GridData>> _gridDataFilter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _gridDataFilter.Value)
            {
                ref var gridData = ref _gridDataFilter.Pools.Inc1.Get(entity);
            }
        }


        private void SpawnGrid(ref GridData gridData)
        {
            var defaultSpawnPosition = gridData.GridTransform.position;
            
            for (int x = 0; x < gridData.GridSideSize; x++)
            {
                for (int y = 0; y < gridData.GridSideSize; y++)
                {
                    var currentSpawnPos = new Vector3(
                        defaultSpawnPosition.x + x * 2,
                        defaultSpawnPosition.y,
                        defaultSpawnPosition.z + y * 2
                    );

                    var spawnedObject = Object.Instantiate(gridData.CellPrefab, currentSpawnPos, Quaternion.identity);
                }
            }
        }
    }
}