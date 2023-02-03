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

        public Unit Unit
        {
            get => m_Unit;
            set => m_Unit = value; //TODO: Eski Unit varsa işlem yapılabilir veya bug vardır
        }

        private Grid m_Grid;
        private Unit m_Unit;

        public void Setup(Grid grid)
        {
            m_Grid = grid;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Grid.SelectedTile = this;
            Debug.Log($"Selected {name}");
            SpriteRenderer.color = Color.gray;
        }

        public void Unselect()
        {
            Debug.Log($"Unselected {name}");
            SpriteRenderer.color = Color.white;
        }
    }
}