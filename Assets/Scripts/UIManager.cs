using Data;
using UnityEngine;
using TMPro;
public class UIManager : Helpers.Singleton<UIManager>
{
    [SerializeField] private Camera m_Camera;
    
    [SerializeField] private GameObject m_FirstPlayer;
    [SerializeField] private GameObject m_SecondPlayer;
    
    [SerializeField] private TextMeshProUGUI FirstPlayerNameText;
    [SerializeField] private TextMeshProUGUI SecondPlayerNameText;
    [SerializeField] private TextMeshProUGUI FirstPlayerNameUIZone;
    [SerializeField] private TextMeshProUGUI SecondPlayerNameUIZone;

    [SerializeField] private TextMeshProUGUI FirstPlayerScoreText;
    [SerializeField] private TextMeshProUGUI SecondPlayerScoreText;
    
    [SerializeField] private TextMeshProUGUI FirstPlayerRootCountText;
    [SerializeField] private TextMeshProUGUI SecondPlayerRootCountText;
    
    [SerializeField] private TextMeshProUGUI CurrentPlayerName;
    [SerializeField] private TextMeshProUGUI FirstTimer;
    [SerializeField] private TextMeshProUGUI SecondTimer;


    [SerializeField] private GameObject MainMenuAssets;
    
    public void StartGame()
    {
        FirstPlayerNameUIZone.text = FirstPlayerNameText.text;
        SecondPlayerNameUIZone.text = SecondPlayerNameText.text;
        MainMenuAssets.SetActive(false);
        
        UpdateRootsCount(1, 0);
        UpdateRootsCount(2, 0);
    }
    public void AnimatePlayer(int playerNumber)
    {
        if (playerNumber == 1)
        {
            m_SecondPlayer.gameObject.GetComponent<Animator>().enabled = false;
            m_FirstPlayer.gameObject.GetComponent<Animator>().enabled = true;
            m_FirstPlayer.gameObject.GetComponent<Animator>().Play("FirstAnimateUI");
        }
        else if (playerNumber == 2)
        {
            m_FirstPlayer.gameObject.GetComponent<Animator>().enabled = false;
            m_SecondPlayer.gameObject.GetComponent<Animator>().enabled = true;
            m_SecondPlayer.gameObject.GetComponent<Animator>().Play("SecondAnimateUI");
        }
    }

    public void NameChange(int playerNumber)
    {
        if (playerNumber == 1)
        {
            CurrentPlayerName.text = FirstPlayerNameUIZone.text;
            CurrentPlayerName.color = ColorData.P1Color;
            FirstTimer.color = ColorData.P1Color;
            m_Camera.backgroundColor = ColorData.P1Color;
        }
        else if (playerNumber == 2)
        {
            CurrentPlayerName.text = SecondPlayerNameUIZone.text;
            CurrentPlayerName.color = ColorData.P2Color;
            SecondTimer.color = ColorData.P2Color;
            m_Camera.backgroundColor = ColorData.P2Color;
        }
    }

    public void UpdateRootsCount(int id, int count)
    {
        if (id == 1)
            FirstPlayerRootCountText.text = count.ToString();
        else if (id == 2)
            SecondPlayerRootCountText.text = count.ToString();
    }

    public void UpdatePlayerScore(int id, int score)
    {
        if (id == 1)
            FirstPlayerScoreText.text = score.ToString();
        else if (id == 2)
            SecondPlayerScoreText.text = score.ToString();
    }
}
