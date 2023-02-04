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

    [SerializeField] private TextMeshProUGUI CurrentPlayerName;

    [SerializeField] private GameObject MainMenuAssets;
    
    public void StartGame()
    {
        FirstPlayerNameUIZone.text = FirstPlayerNameText.text;
        SecondPlayerNameUIZone.text = SecondPlayerNameText.text;
        MainMenuAssets.SetActive(false);
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
            CurrentPlayerName.text = "Current Player: " + FirstPlayerNameUIZone.text;
        }
        else if (playerNumber == 2)
        {
            CurrentPlayerName.text = "Current Player: " + SecondPlayerNameUIZone.text;
        }
    }
}
