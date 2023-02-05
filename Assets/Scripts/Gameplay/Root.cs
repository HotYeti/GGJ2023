using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Gameplay
{
    public enum RootType
    {
        Root,
        Branch
    }
    
    public class Root : Unit
    {
        public RootType RootType = RootType.Root;
        
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
        [SerializeField] private LineRenderer m_LineRenderer;

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

            if(m_spriteRenderer)
                m_spriteRenderer.color = color;
            
            if (m_LineRenderer)
            {
                m_LineRenderer.startColor = color;
                m_LineRenderer.endColor = color;
            }
        }

        public void SetTile(Tile tile)
        {
            m_Tile = tile;
        }

        public IEnumerator DestroyAllBranches(bool includeSelf, bool countScore = true)
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
                    if(m_Head)
                        m_Head.Branches.Remove(this);

                    if (m_Tile)
                        m_Tile.Unit = null;

                    if (countScore && RootType is RootType.Root)
                    {
                        yield return GameManager.Instance.AddScore(OwnerId == 1 ? 2 : 1);
                    }
                    else
                    {
                        Destroy(gameObject);
                        yield return new WaitForSeconds(0.1f);
                    }
                    
                }
        }

        public void AddBranch(Root branch)
        {
            Branches.Add(branch);
            branch.m_Head = this;

            if (branch.m_LineRenderer)
            {
                var headPos = branch.m_Head.transform.position;
                var branchPos = branch.m_Tile.transform.position;

                branch.m_LineRenderer.SetPosition(0, headPos);
                branch.m_LineRenderer.SetPosition(1, branchPos + (branchPos - headPos).normalized * 0.35f);
            }
        }
    }
}