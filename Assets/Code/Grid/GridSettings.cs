using UnityEngine;

namespace Code.Grid
{
    public class GridSettings : MonoBehaviour
    {
        [field: SerializeField] public GameObject CellPrefab { get; private set; }
        [field: SerializeField] public int CellSpacing { get; private set; }
        [field: SerializeField, Range(2, 5)] public int GridSideSize { get; private set; }
    }
}