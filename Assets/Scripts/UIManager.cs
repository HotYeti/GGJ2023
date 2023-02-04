using UnityEngine;
using TMPro;
public class UIManager : Helpers.Singleton<UIManager>
{
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
            Debug.Log("Player 1 Animation Started");
            m_SecondPlayer.gameObject.GetComponent<Animator>().enabled = false;
            m_FirstPlayer.gameObject.GetComponent<Animator>().enabled = true;
            m_FirstPlayer.gameObject.GetComponent<Animator>().Play("FirstAnimateUI");
        }
        else if (playerNumber == 2)
        {
            Debug.Log("Player 2 Animation Started");
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
            CurrentPlayerName.color = new Color(254, 122, 142);
        }
        else if (playerNumber == 2)
        {
            CurrentPlayerName.text = SecondPlayerNameUIZone.text;
            CurrentPlayerName.color = new Color(137, 234, 179);
        }
    }

    public void UpdateRootsCount(int id, int count)
    {
        if (id == 1)
            FirstPlayerRootCountText.text = $"Roots: {count.ToString()}";
        else if (id == 2)
            SecondPlayerRootCountText.text = $"Roots: {count.ToString()}";
    }

    public void UpdatePlayerScore(int id, int score)
    {
        if (id == 1)
            FirstPlayerScoreText.text = $"Score: {score.ToString()}";
        else if (id == 2)
            SecondPlayerScoreText.text = $"Score: {score.ToString()}";
    }
}
