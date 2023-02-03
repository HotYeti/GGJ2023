using Helpers;
using UnityEngine;
using TMPro;
public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField] private TextMeshProUGUI FirstPlayerNameText;
    [SerializeField] private TextMeshProUGUI SecondPlayerNameText;
    [SerializeField] private TextMeshProUGUI FirstPlayerNameUIZone;
    [SerializeField] private TextMeshProUGUI SecondPlayerNameUIZone;

    [SerializeField] private GameObject MainMenuAssets;
    
    public void StartGame()
    {
        FirstPlayerNameUIZone.text = FirstPlayerNameText.text;
        SecondPlayerNameUIZone.text = SecondPlayerNameText.text;
        MainMenuAssets.SetActive(false);
    }

}
