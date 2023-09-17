using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Cell
{
    public class CellsCleaner : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellData>> _cellDataFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _cellDataFilter.Value)
            {
                ref var cellData = ref _cellDataFilter.Pools.Inc1.Get(entity);
                if(cellData.MoleInCell == null) continue;

                if (!cellData.MoleInCell.activeSelf)
                {
                    cellData.MoleInCell = null;
                }
            }
        }
    }
}