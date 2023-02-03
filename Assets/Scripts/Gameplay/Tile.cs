using Unity.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Tile : MonoBehaviour
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
    }
}