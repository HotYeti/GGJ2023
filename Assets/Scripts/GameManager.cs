using Helpers;
using UnityEngine;
using Grid = Gameplay.Grid;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Grid m_Grid;
    public void StartGame()
    {
        MainMenuManager.Instance.StartGame();
        m_Grid.GenerateGrid();
        Debug.Log("Game Started");
    }
}
