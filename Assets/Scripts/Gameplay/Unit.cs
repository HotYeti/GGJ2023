using System;
using UnityEngine;

namespace Gameplay
{
    public abstract class Unit : MonoBehaviour
    {
        public int OwnerId { get; private set; } = 0;
        public Tile Tile { get; set; }

        public Dir Dir
        {
            get => _dir;
            set
            {
                _dir = value;
                
                Vector3 angles = transform.localEulerAngles;
                angles.z = -60f * (int)value;
                transform.localEulerAngles = angles;
            }

        }

        public Action<int> OnSetOwner;
        
        private Dir _dir = Dir.None;
        public void SetOwner(int id)
        {
            OwnerId = id;
            OnSetOwner?.Invoke(id);
        }
    }
}