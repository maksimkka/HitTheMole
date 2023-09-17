using Code.Cell;
using Code.Grid;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Spawner
{
    public class MoleSpawner : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoleSpawnerData>> _moleSpawnerDataFilter = default;
        private readonly EcsFilterInject<Inc<CellData>> _cellDataFilter = default;
        private readonly EcsFilterInject<Inc<EmptyCellsData>> _emptyCellsDataFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _moleSpawnerDataFilter.Value)
            {
                ref var moleSpawner = ref _moleSpawnerDataFilter.Pools.Inc1.Get(entity);
            }
        }

        private void Reload(ref MoleSpawnerData moleSpawner)
        {
            moleSpawner.CurrentTimeUntilSpawn += Time.deltaTime;
            if (moleSpawner.CurrentTimeUntilSpawn >= moleSpawner.SpawnDelay)
            {
                moleSpawner.CurrentTimeUntilSpawn = 0;
                FillEmptyCellArray();
                //Shoot(ref weapon);
            }
        }

        private void FillEmptyCellArray()
        {
            foreach (var emptyCellEntity in _emptyCellsDataFilter.Value)
            {
                ref var emptyCells = ref _emptyCellsDataFilter.Pools.Inc1.Get(emptyCellEntity);
                emptyCells.EmptyCellsEntity.Clear();

                foreach (var cellEntity in _cellDataFilter.Value)
                {
                    ref var cellData = ref _cellDataFilter.Pools.Inc1.Get(cellEntity);

                    if (cellData.MoleInCell == null)
                    {
                        emptyCells.EmptyCellsEntity.Add(cellEntity);
                    }
                }
            }
        }

        private void FindEmptyCell()
        {

            foreach (var entity in _cellDataFilter.Value)
            {
                ref var cell = ref _cellDataFilter.Pools.Inc1.Get(entity);
                if (cell.MoleInCell != null) continue;
            }
        }
    }
}