using System;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class Grid : MonoBehaviour
    {
        [Header("Reference")] [SerializeField] private Tile m_Tile;

        [Header("Settings")] 
        [SerializeField] private Vector2Int m_Size;

        [SerializeField, Min(0f)] private float m_Padding = 0;

        private Vector2 TileSize => m_Tile.SpriteRenderer.size * (1 + m_Padding);

        private Tile[,] m_Tiles;

        public Tile SelectedTile
        {
            get => _selectedTile;

            set
            {
                var previousTile = _selectedTile;
                _selectedTile = value;

                if(previousTile)
                    previousTile.Unselect();

                if(_selectedTile)
                    _selectedTile.Select();
                
                OnTileSelect?.Invoke(previousTile, _selectedTile);
            }
        }

        public Action<Tile, Tile> OnTileSelect;

        private Tile _selectedTile = null;

        public void GenerateGrid()
        {
            m_Tiles = new Tile[m_Size.x, m_Size.y];
            
            for (int y = 0; y < m_Size.y; y++)
            {
                for (int x = 0; x < m_Size.x; x++)
                {
                    var newTile = GenerateTile($"Tile {x}, {y}");

                    newTile.Setup(this);
                    
                    var offset = x % 2 == 1 ? TileSize.y / 2 : 0f;
                    newTile.transform.localPosition = new Vector3(x * TileSize.x * 3 / 4 - (TileSize.x * (m_Size.x - 1) * 3f / 8f), y * TileSize.y + offset - (TileSize.y * (m_Size.y - 1) / 2f));
                   
                    m_Tiles[x, y] = newTile;
                }
            }
            
            SetNeighbours();
        }

        private Tile GenerateTile(string tileName)
        {
            var tile = Instantiate(m_Tile, transform, false);
            tile.name = tileName;
            
            return tile;
        }

        private void SetNeighbours()
        {
            for (int y = 0; y < m_Size.y; y++)
            {
                for (int x = 0; x < m_Size.x; x++)
                {
                    var tile = m_Tiles[x, y];

                    if (y + 1 < m_Size.y)
                        tile.Up = m_Tiles[x, y + 1];

                    if (y - 1 >= 0)
                        tile.Down = m_Tiles[x, y - 1];

                    var odd = x % 2 == 0;
                    
                    if (x + 1 < m_Size.x)
                    {
                        if (odd)
                        {
                            tile.RightUp = m_Tiles[x + 1, y];

                            if (y - 1 >= 0)
                                tile.RightDown = m_Tiles[x + 1, y - 1];
                        }
                        else
                        {

                            if (y + 1 < m_Size.y)
                                tile.RightUp = m_Tiles[x + 1, y + 1];

                            tile.RightDown = m_Tiles[x + 1, y];
                        }
                    }

                    if (x - 1 >= 0)
                    {
                        if (odd)
                        {
                            tile.LeftUp = m_Tiles[x - 1, y];
                            
                            if (y - 1 >= 0)
                                tile.LeftDown = m_Tiles[x - 1, y - 1];
                        }
                        else
                        {
                            if (y + 1 < m_Size.y)
                                tile.LeftUp = m_Tiles[x - 1, y + 1];

                            tile.LeftDown = m_Tiles[x - 1, y];
                        }
                    }
                    
                }
            }
        }
        
        public bool OutOfMoves()
        {
            for (int y = 0; y < m_Size.y; y++)
            {
                for (int x = 0; x < m_Size.x; x++)
                {
                    var tile = m_Tiles[x, y];
                    if (tile.Unit && tile.Unit.OwnerId == GameManager.Instance.ActivePlayer)
                    {
                        if (tile.GetMovables().Count > 0 || tile.GetAttackables().Count() > 0)
                            return false;
                    }
                }
            }
            return true;
        }
    }
}