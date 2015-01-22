using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject pnlMenu;

	[SerializeField]
	private Text lblMusic;
	[SerializeField]
	private Text lblSound;

	[SerializeField]
	private AudioSource audioSourceMain;
	[SerializeField]
	private AudioSource audioSourceBtnUp;
	[SerializeField]
	private AudioSource audioSourceBtnDown;
	[SerializeField]
	private AudioSource audioSourceBtnLeft;
	[SerializeField]
	private AudioSource audioSourceBtnRight;
	[SerializeField]
	private AudioSource audioSourceBtnDice;
	[SerializeField]
	private AudioSource audioSourceFood;
	[SerializeField]
	private AudioSource audioSourceDrink;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Opens the menu.
	/// </summary>
	public void openMenu()
	{
		pnlMenu.SetActive(true);
	}
	
	/// <summary>
	/// Closes the menu.
	/// </summary>
	public void closeMenu()
	{
		pnlMenu.SetActive(false);
	}
	
	/// <summary>
	/// Loads the aantalbeurtenScreen scene(start new game).
	/// </summary>
	public void clickNewGame()
	{
		Application.LoadLevel("MainMenu");
	}
	
	/// <summary>
	/// Turns the sound on or off depending on current state.
	/// </summary>
	public void clickMusic()
	{
		if (lblMusic.text == "Muziek aan")
		{
			audioSourceMain.mute = false;
			
			lblMusic.text = "Muziek uit";
		}
		else
		{
			audioSourceMain.mute = true;
			
			lblMusic.text = "Muziek aan";
		}
	}

	public void clickSound()
	{
		if (lblSound.text == "Geluid aan")
		{
			audioSourceBtnUp.mute = false;
			audioSourceBtnDown.mute = false;
			audioSourceBtnLeft.mute = false;
			audioSourceBtnRight.mute = false;
			audioSourceBtnDice.mute = false;
			audioSourceFood.mute = false;
			audioSourceDrink.mute = false;
			
			lblSound.text = "Geluid uit";
		}
		else
		{
			audioSourceBtnUp.mute = true;
			audioSourceBtnDown.mute = true;
			audioSourceBtnLeft.mute = true;
			audioSourceBtnRight.mute = true;
			audioSourceBtnDice.mute = true;
			audioSourceFood.mute = true;
			audioSourceDrink.mute = true;
			
			lblSound.text = "Geluid aan";
		}
	}
}
