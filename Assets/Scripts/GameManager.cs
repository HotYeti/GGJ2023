using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Helpers;
using UnityEngine;
using Grid = Gameplay.Grid;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Grid m_Grid;
    [SerializeField] private Root m_RootReference;
    [SerializeField] private Root m_BranchReference;
    
    public int ActivePlayer
    {
        get => _activePlayer;

        private set
        {
            Debug.Log($"Turn change from {_activePlayer} to {value}");
            _activePlayer = value;
            UIManager.Instance.AnimatePlayer(_activePlayer);
            UIManager.Instance.NameChange(_activePlayer);
        }
    }

    public int Enemy => ActivePlayer == 1 ? 2 : 1;

    private int _activePlayer = 0; // 0 => natural, 1 => player1, 2 => player2/bot
    
    public Root[] Roots = new Root[]{null, null};

    private int[] _scores = new int[]{0, 0};
    
    // Play button'una basınca çalışıyor
    public void StartGame()
    {
        UIManager.Instance.StartGame();
        m_Grid.GenerateGrid();
        m_Grid.OnTileSelect += TileSelected;
        Debug.Log("Game Started");
        ActivePlayer = 1;
        StoryManager.Instance.StartJourneyPopup();
    }

    public void TileSelected(Tile previous, Tile current)
    {
        // İlk hamle
        if (ActivePlayer > 0 && Roots[ActivePlayer - 1] == null)
        {
            if (TryPlaceRoot(current))
            {
                EndTurn();
            }
        }

        if (previous && current && previous.Unit && previous.Unit.OwnerId == ActivePlayer)
        {
            TryPlaceBranch(previous, current);
        }
    }

    private bool TryPlaceRoot(Tile target)
    {
        if(!target ||target.Unit)
            return false;
        
        Root newRoot = Instantiate(m_RootReference, target.transform, false);
        target.Unit = newRoot;
        newRoot.SetTile(target);
            
        newRoot.SetOwner(ActivePlayer);

        Roots[ActivePlayer - 1] = newRoot;
        return true;
    }

    private void TryPlaceBranch(Tile head, Tile target)
    {
        StartCoroutine(_TryPlaceBranch());
        
        IEnumerator _TryPlaceBranch()
        {
            Dir currentDir = head.GetNeighbourDir(target);

            if (currentDir == Dir.None)
                yield break;

            if (!head || head.Unit is not Root previousRoot)
                yield break;

            var movables = head.GetMovables();
            var attackables = head.GetAttackables();
            
            if (movables.Count < 1 && attackables.Count < 1)
                yield break;

            if (!movables.Contains(target) && !attackables.Contains(target))
                yield break;
        
            if (attackables.Contains(target) && target.Unit is Root currentRoot)
            {
                var player = ActivePlayer;

                if (currentRoot.RootType is RootType.Main)
                {
                    Debug.Log(player);
                    _scores[player - 1]++;
                }
                
                _activePlayer = 0;
                yield return currentRoot.DestroyAllBranches(true);
                yield return target.TriggerBomb();
                _activePlayer = player;
            }

            if (target.IsExploded)
            {
                target.IsExploded = false;
            }
            else
            {
                Root newBranch = Instantiate(m_BranchReference, target.transform, false);
                target.Unit = newBranch;
                newBranch.SetTile(target);
            
                newBranch.SetOwner(ActivePlayer);
                newBranch.Dir = currentDir;
            
                previousRoot.AddBranch(newBranch);
            }
            
            
            EndTurn();
        }
            
        
    }

    public void EndTurn()
    {
        StartCoroutine(_EndTurn());

        IEnumerator _EndTurn()
        {
            m_Grid.SelectedTile = null;
            yield return null;
            ActivePlayer = ActivePlayer == 1 ? 2 : 1;

            UIManager.Instance.UpdateRootsCount(1, Roots[0] ? Roots[0].TotalRoots : 0);
            UIManager.Instance.UpdateRootsCount(2, Roots[1] ? Roots[1].TotalRoots : 0);
            UIManager.Instance.UpdatePlayerScore(1, _scores[0]);
            UIManager.Instance.UpdatePlayerScore(2, _scores[1]);

            CheckEndGame();
        }
    }


    private void CheckEndGame()
    {
        int winner = 0;

        for (int i = 0; i < _scores.Length; i++)
        {
            if (_scores[i] > 2)
                winner = i + 1;
        }
        
        if(winner != 0)
            EndGame(winner);

        if (m_Grid.OutOfMoves())
        {
            if (_scores[0] > _scores[1])
            {
                EndGame(1);
            }
            else if (_scores[0] < _scores[1])
            {
                EndGame(2);
            }
            else
            {
                if (Roots[0].TotalRoots > Roots[1].TotalRoots)
                {
                    EndGame(1);
                }
                else if (Roots[0].TotalRoots < Roots[1].TotalRoots)
                {
                    EndGame(2);
                }
                else
                {
                    EndGame(0); // Tie
                }
            }
        }
    }


    private void EndGame(int id)
    {
        if (id == 0)
        {
            Debug.Log("Tie game");
        }
        else
        {
            Debug.Log($"{id} is winner");
        }
    }
    
}
