using System.Collections.Generic;
using UnityEngine;

namespace Code.Grid
{
    public struct GridData
    {
        public int[,] CellsEntity;
        public GameObject CellPrefab;
        public Transform GridTransform;
        public int GridSideSize;
        public float CellSize;
        public float CellSpacing;
    }
}