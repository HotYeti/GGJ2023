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

        public Dir GetNeighbourDir(Tile neighbour)
        {
            if (neighbour is null)
                return Dir.None;
            
            if (Up == neighbour)
                return Dir.Up;

            if (Down == neighbour)
                return Dir.Down;

            if (LeftUp == neighbour)
                return Dir.LeftUp;

            if (LeftDown == neighbour)
                return Dir.LeftDown;
            
            if (RightUp == neighbour)
                return Dir.RightUp;
            
            if (RightDown == neighbour)
                return Dir.RightDown;
            
            return Dir.None;
        }
    }
}