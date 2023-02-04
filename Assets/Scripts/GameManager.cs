using Gameplay;
using Helpers;
using JetBrains.Annotations;
using UnityEngine;
using Grid = Gameplay.Grid;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Grid m_Grid;
    [SerializeField] private Root m_RootReference;
    public int ActivePlayer
    {
        get => _activePlayer;

        private set
        {
            Debug.Log($"Turn change from {_activePlayer} to {value}");
            _activePlayer = value;
            UIManager.Instance.AnimatePlayer(_activePlayer);
        }
    }

    private int _activePlayer = 0; // 0 => natural, 1 => player1, 2 => player2/bot
    
    public Root[] Roots = new Root[]{null, null};
    
    // Play button'una basınca çalışıyor
    public void StartGame()
    {
        MainMenuManager.Instance.StartGame();
        m_Grid.GenerateGrid();
        m_Grid.OnTileSelect += TileSelected;
        Debug.Log("Game Started");
        ActivePlayer = 1;
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
    }

    public void EndTurn()
    {
        ActivePlayer = ActivePlayer == 1 ? 2 : 1;
        m_Grid.SelectedTile = null;
    }
    
}
