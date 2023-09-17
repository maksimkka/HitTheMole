using System.Collections.Generic;
using Code.Cell;
using Code.Logger;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Grid
{
    public class GridInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<GridData> _gridData = default;
        private readonly EcsPoolInject<CellData> _cellData = default;
        private readonly EcsPoolInject<EmptyCellsData> _emptyCellsData = default;
        private readonly EcsCustomInject<GridSettings> _gridSettings = default;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            var entity = _world.NewEntity();
            ref var gridData = ref _gridData.Value.Add(entity);
            
            gridData.CellPrefab = _gridSettings.Value.CellPrefab;
            gridData.GridTransform = _gridSettings.Value.gameObject.transform;
            gridData.CellSpacing = _gridSettings.Value.CellSpacing;
            gridData.GridSideSize = _gridSettings.Value.GridSideSize;

            CreateCells(ref gridData);
        }
        
        private void CreateCells(ref GridData gridData)
        {
            ref var emptyCells = ref _emptyCellsData.Value.Add(_world.NewEntity());
            emptyCells.EmptyCellsEntity = new List<int>();
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
                    
                    var cellEntity = _world.NewEntity();
                    //emptyCells.EmptyCellsEntity.Add(cellEntity);
                    ref var cellData = ref _cellData.Value.Add(cellEntity);
                    cellData.CellPosition = spawnedObject.transform.position;
                }
            }
        }
    }
}