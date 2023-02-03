using System;
using UnityEngine;

namespace Gameplay
{
    public abstract class Unit : MonoBehaviour
    {
        public int OwnerId { get; private set; } = 0;
        public Tile Tile { get; set; }

        public Action<int> OnSetOwner;
        public void SetOwner(int id)
        {
            OwnerId = id;
            OnSetOwner?.Invoke(id);
        }
    }
}