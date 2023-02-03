using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI FirstPlayerNameText;
    [SerializeField] private TextMeshProUGUI SecondPlayerNameText;
    [SerializeField] private TextMeshProUGUI FirstPlayerNameUIZone;
    [SerializeField] private TextMeshProUGUI SecondPlayerNameUIZone;
    
    public void StartGame()
    {
        FirstPlayerNameUIZone.text = FirstPlayerNameText.text;
        SecondPlayerNameUIZone.text = SecondPlayerNameText.text;
    }

}
