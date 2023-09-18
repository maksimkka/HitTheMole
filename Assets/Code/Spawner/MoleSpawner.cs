using Code.Cell;
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

        private float _currentTimeUntilSpawn;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _moleSpawnerDataFilter.Value)
            {
                ref var moleSpawner = ref _moleSpawnerDataFilter.Pools.Inc1.Get(entity);
                Reload(ref moleSpawner);
            }
        }

        private void Reload(ref MoleSpawnerData moleSpawner)
        {
            moleSpawner.CurrentTimeUntilSpawn += Time.deltaTime;

            if (moleSpawner.CurrentTimeUntilSpawn >= moleSpawner.SpawnDelay)
            {
                moleSpawner.CurrentTimeUntilSpawn = 0;
                FillEmptyCellArray();
                SpawnMole(ref moleSpawner);
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
        
        private void SpawnMole(ref MoleSpawnerData moleSpawner)
        {
            foreach (var emptyCellEntity in _emptyCellsDataFilter.Value)
            {
                ref var emptyCells = ref _emptyCellsDataFilter.Pools.Inc1.Get(emptyCellEntity);
                if(emptyCells.EmptyCellsEntity.Count == 0) return;
                int randomEmptyCellIndex = Random.Range(0, emptyCells.EmptyCellsEntity.Count);

                ref var cellData = ref _cellDataFilter.Pools.Inc1.Get(emptyCells.EmptyCellsEntity[randomEmptyCellIndex]);

                var mole = moleSpawner.MolesPool.GetObject(cellData.CellPosition, Quaternion.identity);
                cellData.MoleInCell = mole.gameObject;
            }
        }
    }
}