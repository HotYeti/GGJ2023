using System.Collections;
using System.Collections.Generic;
using Data;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Tile : MonoBehaviour, IPointerDownHandler
    {
        public Tile Up;
        public Tile Down;
        public Tile LeftUp;
        public Tile LeftDown;
        public Tile RightUp;
        public Tile RightDown;

        public bool IsExploded { get; set; }
        
        
        #region Reference

        [field: SerializeField]
        public SpriteRenderer SpriteRenderer { get; private set; }


        [SerializeField] private Bomb m_BombReference; 

        #endregion

        public Unit Unit
        {
            get => m_Unit;
            set
            {
                m_Unit = value;
            } 
        }

        public List<Tile> Neighbours => new List<Tile>() { Up, Down, LeftDown, LeftUp, RightDown, RightUp };

        private Grid m_Grid;
        private Unit m_Unit;
        private Bomb m_Bomb;

        public void Setup(Grid grid)
        {
            m_Grid = grid;
            m_Bomb = Instantiate(m_BombReference, transform, false);
            m_Bomb.Tile = this;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log($"Selected {name}");
            m_Grid.SelectedTile = this;
        }

        public void Select()
        {
            if (!Unit || Unit.OwnerId == GameManager.Instance.ActivePlayer)
            {
                SpriteRenderer.color = ColorData.SelectedTile;

                foreach (var movable in GetMovables())
                {
                    movable.SpriteRenderer.color = ColorData.MovableTile;
                }

                foreach (var attackable in GetAttackables())
                {
                    attackable.SpriteRenderer.color = ColorData.AttackableTile;
                }
            }
        }

        public void Unselect()
        {
            //Debug.Log($"Unselected {name}");
            SpriteRenderer.color = Color.white;

            foreach (var neighbour in Neighbours)
            {
                if (!neighbour)
                    continue;
                
                neighbour.SpriteRenderer.color = Color.white;
            }
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

        public List<Tile> GetMovables()
        {
            if (!IsPlayerUnitAndBranchable())
                return new List<Tile>();

            List<Tile> movables = new List<Tile>(){Up, Down, LeftDown, LeftUp, RightDown, RightUp};

            for (int i = 0; i < movables.Count; i++)
            {
                Tile tile = movables[i];

                if (!tile || tile.Unit)
                {
                    movables.RemoveAt(i);
                    i--;
                    continue;
                }
            }
            return movables;
        }

        public List<Tile> GetAttackables()
        {
            if (!IsPlayerUnitAndBranchable())
                return new List<Tile>();
            
            List<Tile> attackables = new List<Tile>(){Up, Down, LeftDown, LeftUp, RightDown, RightUp};
            
            for (int i = 0; i < attackables.Count; i++)
            {
                Tile tile = attackables[i];

                if (!tile || tile.Unit is not Root tileRoot)
                {
                    attackables.RemoveAt(i);
                    i--;
                    continue;
                }

                int enemy = Unit.OwnerId == 1 ? 2 : 1;

                if (tileRoot.OwnerId != enemy)
                {
                    attackables.RemoveAt(i);
                    i--;
                    continue;
                }

                Dir attackDir = GetNeighbourDir(tile);
                
                if ((int)Unit.Dir % 3 != (int)attackDir % 3)
                {
                    //Debug.Log("Hedef'e bakmıyorum.");
                    attackables.RemoveAt(i);
                    i--;
                    continue;
                }
                
                if ((int)tileRoot.Dir % 3 == (int)attackDir % 3)
                {
                    //Debug.Log("Hedef bana bakıyor.");
                    attackables.RemoveAt(i);
                    i--;
                    continue;
                }
            }
            return attackables;
        }

        public bool IsPlayerUnitAndBranchable()
        {
            if (!Unit || Unit is not Root thisRoot || Unit.OwnerId is not 1 and not 2)
                return false;
            
            switch (thisRoot.RootType)
            {
                case RootType.Root:
                    if (thisRoot.Branches.Count < 3)
                        return true;
                    break;
                case RootType.Branch:
                    if(thisRoot.Branches.Count < 2)
                        return true;
                    break;
            }

            return false;
        }

        public void ResetBomb() => m_Bomb.ResetTrigger();

        public IEnumerator TriggerBomb()
        { 
            yield return m_Bomb.Trigger();
        }
        
    }
}