using System;
using System.Collections.Generic;
using Data;
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
        
        private Root m_Head;
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
                1 => ColorData.P1Color,
                2 => ColorData.P2Color,
                _ => Color.gray
            };

            m_spriteRenderer.color = color;
        }

        public void DestroyAllBranches(bool includeSelf)
        {
            int iteration = 0;
            while (Branches.Count > 0)
            {
                Branches[0].DestroyAllBranches(true);

                iteration++;
                if (iteration > 100000)
                    Debug.LogError("Iteration out of range");
            }

            if (includeSelf)
            {
                Destroy(gameObject);
                if(m_Head)
                    m_Head.Branches.Remove(this);
            }
            
        }

        public void AddBranch(Root branch)
        {
            Branches.Add(branch);
            branch.m_Head = this;
        }
    }
}