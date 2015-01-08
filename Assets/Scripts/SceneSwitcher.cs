using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {

	public GameObject leftButton;
	public GameObject rightButton;
    public Text playerCount;
	
	public void GoToScene(string changeScene)
	{
        if (changeScene == "BoardGame")
        {
            TitleMusic.Instance.Destroy();
        }

		Application.LoadLevel (changeScene);
		//audio.PlayOneShot("buttonclick"); 
	}

    public void SetTurnSlider(float turns)
    {
        InformationManager.instance.setMaxTurns(turns);
        playerCount.text = turns.ToString();
    }

    public void SetPlayerCount(int players)
    {
        InformationManager.instance.setPlayerCount(players);
        GoToScene("PlayerInfo");
    }
}
