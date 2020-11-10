using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    [RequireComponent(typeof(GridGenerator))]
    public class Grid : MonoBehaviour
    {
        public static Grid Instance;

        public static Action<GridObject> ObjectCreated;

        #region TILES
        /// <summary>
        /// Saved TileData used by the editor
        /// </summary>
        public List<GridTilePositionData> editorTileData = new List<GridTilePositionData>();

        public bool newTileData = false;

        /// <summary>
        /// Access to the Grid Tiles of the Grid. This should be used instead of EditorTileData, which is only used for serializing the data.
        /// </summary>
        public Dictionary<Vector2Int, GridTile> Tiles
        {
            get
            {
                if (tiles != null && !newTileData)
                {
                    return tiles;
                }
                else
                {
                    SetDictionaryFromData();
                    return tiles;
                }
            }
        }
        private Dictionary<Vector2Int, GridTile> tiles;

        void SetDictionaryFromData()
        {
            tiles = new Dictionary<Vector2Int, GridTile>();
            foreach (GridTilePositionData d in editorTileData)
            {
                tiles.Add(d.Position, d.Tile);
            }

            newTileData = false;
        }
        #endregion


        #region GRID OBJECTS
        public List<GridObject> GridObjects => gridObjects;
        private readonly List<GridObject> gridObjects = new List<GridObject>();

        public GridObject InstantiateGridObject(GridObject gridObjectPrefab, Vector2Int gridPosition, Vector3? offsetPosition = null, Quaternion? rotation = null)
        {
            if (Tiles.TryGetValue(gridPosition, out GridTile tile))
            {
                //Create Object on a tile
                GridObject newObject = Instantiate(gridObjectPrefab, tile.transform);
                newObject.OnCreate(tile);
            
                // Add to list of Grid Objects
                gridObjects.Add(newObject);
                
                // Broadcast that object was created to everything
                ObjectCreated?.Invoke(newObject);

                return newObject;
            }
            else
            {
                Debug.LogError("Tried to instantiate a Grid Object at (" + gridPosition.x + " , " + gridPosition.y + ") and could not find a Tile at that position");
                return null;
            }
        }

        public void ClearGridObjects()
        {
            foreach (var gridObject in gridObjects)
            {
                gridObject.OnClear();
                Destroy(gridObject.gameObject);
            }
            gridObjects.Clear();
        }
        #endregion

        #region MONOBEHAVIOURS
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("GridSystem is not currently setup to support multiple grids.");
            }

            Instance = this;
            
            SetDictionaryFromData();
        }
        #endregion
    }
}