using System;
using System.Collections;
using DefaultNamespace;
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
            if(!current ||current.Unit)
                return;
            
            Root newRoot = Instantiate(m_RootReference, current.transform, false);
            current.Unit = newRoot;
            newRoot.SetOwner(ActivePlayer);

            Roots[ActivePlayer - 1] = newRoot;
            EndTurn();
        }

        if (previous && current && previous.Unit && previous.Unit.OwnerId == ActivePlayer)
        {
            Dir currentDir = previous.GetNeighbourDir(current);

            if (currentDir == Dir.None)
                return;

            if (!previous || previous.Unit is not Root previousRoot)
                return;

            var movables = previous.GetMovables();
            var attackables = previous.GetAttackables();
            
            if (movables.Count < 1 && attackables.Count < 1)
                return;

            if (!movables.Contains(current) && !attackables.Contains(current))
                return;
            
            if (attackables.Contains(current) && current.Unit is Root currentRoot)
            {
                currentRoot.DestroyAllBranches(true);
            }
            
            Root newBranch = Instantiate(m_BranchReference, current.transform, false);
            current.Unit = newBranch;
            
            newBranch.SetOwner(ActivePlayer);
            newBranch.Dir = currentDir;
            
            previousRoot.AddBranch(newBranch);
            
            EndTurn();
        }
    }

    public void EndTurn()
    {
        StartCoroutine(_EndTurn());

        IEnumerator _EndTurn()
        {
            yield return null;
            ActivePlayer = ActivePlayer == 1 ? 2 : 1;
            m_Grid.SelectedTile = null;
        }
    }
    
    
    
}
