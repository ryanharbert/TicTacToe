using UnityEngine;

namespace GridSystem
{
    [RequireComponent(typeof(Grid))]
    public class GridGenerator : MonoBehaviour
    {
        public Vector2 cellSize;
        public Vector2 cellGap;
        public Vector2Int mapSize;
        public GridTile tilePrefab;

        private Transform tileParent;
        
        public void GenerateGrid(Vector2Int mapSize)
        {
            this.mapSize = mapSize;
            GenerateGrid();
        }
        
        public void GenerateGrid()
        {
            Grid grid = GetComponent<Grid>();
            SetTileParent();
            ClearGrid();
            grid.newTileData = true;

            for (int y = 0; y < mapSize.y; y++)
            {
                for (int x = 0; x < mapSize.x; x++)
                {
                    InstantiateGridTile(tilePrefab, new Vector2Int(x, y));
                }
            }

            foreach (var tileData in grid.editorTileData)
            {
                tileData.Tile.TilesChangedOnGrid(GridUtility.GetNeighbors(grid, tileData.Tile));
            }
        }

        GridTile InstantiateGridTile(GridTile gridTilePrefab, Vector2Int gridPosition, Vector3? offsetPosition = null, Quaternion? rotation = null)
        {
            //Create Tile and Set Position
            GridTile newTile = Instantiate(gridTilePrefab, tileParent);
            newTile.name = "[ " + gridPosition.x + " , " + gridPosition.y + " ]";
            float x = gridPosition.x * (cellSize.x + cellGap.x);
            float y = gridPosition.y * (cellSize.y + cellGap.y);
            newTile.transform.localPosition = new Vector3(x, 0, y);
            newTile.OnCreate(gridPosition);
            
            // Add tile to grid data
            Grid grid = GetComponent<Grid>();
            grid.editorTileData.Add(new GridTilePositionData(gridPosition, newTile));

            return newTile;
        }

        void ClearGrid()
        {
            Grid grid = GetComponent<Grid>();
            foreach (var gridTile in grid.editorTileData)
            {
                if (gridTile.Tile != null)
                {
                    DestroyImmediate(gridTile.Tile.gameObject);
                }
            }
            grid.editorTileData.Clear();
        }

        void SetTileParent()
        {
            Grid grid = GetComponent<Grid>();
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                if (grid.transform.GetChild(i).name == "Tiles")
                {
                    tileParent = grid.transform.GetChild(i);
                    return;
                }
            }
            
            tileParent = new GameObject("Tiles").transform;
            tileParent.parent = grid.transform;
            tileParent.localPosition = Vector3.zero;
        }
            
    }
}