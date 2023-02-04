using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Bomb : Unit
    {
        [SerializeField] private int m_ExplodeLimit = 3;

        private int _triggerCount = 0;

        public void ResetTrigger()
        {
            _triggerCount = 0;
            transform.localScale = Vector3.one * (0.2f * (_triggerCount + 1));
        }
        
        public IEnumerator Trigger()
        {
            Debug.Log($"{Tile.name} bomb triggered!");
            
            _triggerCount = (_triggerCount + 1) % m_ExplodeLimit;
            transform.localScale = Vector3.one * (0.2f * _triggerCount);
            
            if (_triggerCount % m_ExplodeLimit == 0)
            {
                yield return Explode();   
            }
        }

        private IEnumerator Explode()
        {
            Tile.IsExploded = true;
            Debug.Log("Explode");
            
            if (Tile.Unit is Root root)
            {
                yield return root.DestroyAllBranches(true);
                ResetTrigger();
            }
            
            print($"{Tile.name}");
            foreach (var neighbour in Tile.Neighbours)
            {
                print($"{neighbour.name}");
                if(!neighbour || !neighbour.Unit || neighbour.Unit is not Root neighbourRoot)
                    continue;

                yield return neighbourRoot.DestroyAllBranches(true);
                neighbour.ResetBomb();
            }
            
            

            
        }
    }
}