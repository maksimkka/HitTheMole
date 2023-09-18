using System.Collections.Generic;
using Code.Cell;
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
        private readonly EcsPoolInject<CenterGridRequest> _centerGridRequest = default;
        private readonly EcsCustomInject<GridSettings> _gridSettings = default;
        private EcsWorld _world;
        private List<Transform> _cells;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _cells = new List<Transform>();
            var entity = _world.NewEntity();
            ref var gridData = ref _gridData.Value.Add(entity);

            gridData.CellPrefab = _gridSettings.Value.CellPrefab;
            gridData.GridTransform = _gridSettings.Value.gameObject.transform;
            gridData.GridSideSize = _gridSettings.Value.GridSideSize;

            CreateCells(ref gridData);
            var centerGridEntity = _world.NewEntity();
            ref var centerGridRequest = ref _centerGridRequest.Value.Add(centerGridEntity);
            centerGridRequest.CenterPosition = FindGridCenter();
            centerGridRequest.GridSize = gridData.GridSideSize;
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
                    ref var cellData = ref _cellData.Value.Add(cellEntity);
                    cellData.CellPosition = spawnedObject.transform.position;
                    _cells.Add(spawnedObject.transform);
                }
            }
        }

        private Vector3 FindGridCenter()
        {
            var totalX = 0f;
            var totalZ = 0f;
            var numberOfObjects = 0;

            foreach (var objectTransform in _cells)
            {
                totalX += objectTransform.position.x;
                totalZ += objectTransform.position.z;
                numberOfObjects++;
            }

            var centerX = totalX / numberOfObjects;
            var centerZ = totalZ / numberOfObjects;
            var centerPosition = new Vector3(centerX, 0f, centerZ);
            return centerPosition;
        }
    }
}