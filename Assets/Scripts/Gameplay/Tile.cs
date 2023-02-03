using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Tile : MonoBehaviour, IPointerDownHandler
    {
        [ReadOnly]
        public Tile Up;
        [ReadOnly]
        public Tile Down;
        [ReadOnly]
        public Tile LeftUp;
        [ReadOnly]
        public Tile LeftDown;
        [ReadOnly]
        public Tile RightUp;
        [ReadOnly]
        public Tile RightDown;


        #region Reference

        [field: SerializeField]
        public SpriteRenderer SpriteRenderer { get; private set; }

        #endregion

        private Grid m_Grid;

        public void Setup(Grid grid)
        {
            m_Grid = grid;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Grid.SelectedTile = this;
            Debug.Log($"Selected {name}");
        }

        public void Unselect()
        {
            Debug.Log($"Unselected {name}");
            
        }
    }
}