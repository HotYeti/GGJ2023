using UnityEngine;
public class UIManager : Helpers.Singleton<UIManager>
{
    [SerializeField] private GameObject m_FirstPlayer;
    [SerializeField] private GameObject m_SecondPlayer;
    public void AnimatePlayer(int playerNumber)
    {
        if (playerNumber == 1)
        {
            Debug.Log("Player 1 Animation Started");
            m_SecondPlayer.gameObject.GetComponent<Animator>().enabled = false;
            m_FirstPlayer.gameObject.GetComponent<Animator>().Play("FirstAnimateUI");
        }
        else if (playerNumber == 2)
        {
            Debug.Log("Player 2 Animation Started");
            m_FirstPlayer.gameObject.GetComponent<Animator>().enabled = false;
            m_SecondPlayer.gameObject.GetComponent<Animator>().Play("SecondAnimateUI");
        }
    }
}
