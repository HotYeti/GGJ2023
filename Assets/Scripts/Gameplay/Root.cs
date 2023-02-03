using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public enum RootType
    {
        Main,
        Branch
    }
    
    public class Root : Unit
    {
        public RootType RootType = RootType.Main;
        public List<Root> Branches { get; private set; } = new List<Root>();

        [SerializeField] private SpriteRenderer m_spriteRenderer;

        private void Awake()
        {
            OnSetOwner += SetColor;
        }

        public void SetColor(int id)
        {
            var color = id switch
            {
                1 => Color.green,
                2 => Color.magenta,
                _ => Color.gray
            };

            m_spriteRenderer.color = color;
        }
    }
}