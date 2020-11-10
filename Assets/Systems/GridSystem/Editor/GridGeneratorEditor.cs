using UnityEditor;
using UnityEngine;

namespace GridSystem
{
    [CustomEditor(typeof(GridGenerator))]
    public class GridGeneratorEditor : Editor
    {
        private GridGenerator generator;
        private Grid grid;

        private Transform tileParent;
        
        public void OnEnable()
        {
            generator = (GridGenerator) target;
            grid = generator.GetComponent<Grid>();
            
            SetTileParent();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Grid", GUILayout.Height(25)))
            {
                GenerateGrid();
            }
        }

        void GenerateGrid()
        {
            Undo.RecordObject(grid, "Before Generate");
            
            ClearGrid();
            grid.newTileData = true;

            for (int y = 0; y < generator.mapSize.y; y++)
            {
                for (int x = 0; x < generator.mapSize.x; x++)
                {
                    InstantiateGridTile(generator.tilePrefab, new Vector2Int(x, y));
                }
            }

            foreach (var tileData in grid.editorTileData)
            {
                tileData.Tile.TilesChangedOnGrid(GridUtility.GetNeighbors(grid, tileData.Tile));
            }
            
            EditorUtility.SetDirty(grid);
            
            Debug.Log("Generated Grid!");
        }

        GridTile InstantiateGridTile(GridTile gridTilePrefab, Vector2Int gridPosition, Vector3? offsetPosition = null, Quaternion? rotation = null)
        {
            //Create Tile and Set Position
            GridTile newTile = Instantiate(gridTilePrefab, tileParent);
            newTile.name = "[ " + gridPosition.x + " , " + gridPosition.y + " ]";
            float x = gridPosition.x * (generator.cellSize.x + generator.cellGap.x);
            float y = gridPosition.y * (generator.cellSize.y + generator.cellGap.y);
            newTile.transform.localPosition = new Vector3(x, 0, y);
            newTile.OnCreate(gridPosition);
            
            // Add tile to grid data
            grid.editorTileData.Add(new GridTilePositionData(gridPosition, newTile));

            return newTile;
        }

        void ClearGrid()
        {
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