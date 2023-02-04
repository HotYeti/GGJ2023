using System;
using System.Collections;
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
        
        public List<Root> Branches { get; private set; } = new List<Root>();

        public int TotalRoots
        {
            get
            {
                int roots = 1;
                
                foreach (var branch in Branches)
                {
                    roots += branch.TotalRoots;
                }

                return roots;
            }
        }
        
        [SerializeField] private SpriteRenderer m_spriteRenderer;

        private Root m_Head;
        private Tile m_Tile;
        
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

        public void SetTile(Tile tile)
        {
            m_Tile = tile;
        }

        public IEnumerator DestroyAllBranches(bool includeSelf)
        {
                int iteration = 0;
                while (Branches.Count > 0)
                {
                    yield return Branches[0].DestroyAllBranches(true);

                    iteration++;
                    if (iteration > 100000)
                        Debug.LogError("Iteration out of range");
                }

                if (includeSelf)
                {
                    Destroy(gameObject);
                    if(m_Head)
                        m_Head.Branches.Remove(this);

                    if (m_Tile)
                        m_Tile.Unit = null;

                    yield return new WaitForSeconds(0.1f);
                }
        }

        public void AddBranch(Root branch)
        {
            Branches.Add(branch);
            branch.m_Head = this;
        }
    }
}