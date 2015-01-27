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
	void Start ()
	{
		if (InformationManager.instance.musicSettings == "Music")
		{
			setMusic(false);

			lblMusic.text = "Muziek uit";
		}
		else
		{
			setMusic(true);

			lblMusic.text = "Muziek aan";
		}

		if (InformationManager.instance.soundsSettings == "Sounds")
		{
			setSounds(false);

			lblSound.text = "Geluid uit";
		}
		else
		{
			setSounds(true);

			lblSound.text = "Geluid aan";
		}
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
        InformationManager.instance.ResetGame();
		Application.LoadLevel("MainMenu");
	}
	
	/// <summary>
	/// Turns the sound on or off depending on current state.
	/// </summary>
	public void clickMusic()
	{
		if (lblMusic.text == "Muziek aan")
		{
			//audioSourceMain.mute = false;

			setMusic(false);

			lblMusic.text = "Muziek uit";
		}
		else
		{
			//audioSourceMain.mute = true;

			setMusic(true);

			lblMusic.text = "Muziek aan";
		}
	}

	public void clickSound()
	{
		if (lblSound.text == "Geluid aan")
		{
			//audioSourceBtnUp.mute = false;
			//audioSourceBtnDown.mute = false;
			//audioSourceBtnLeft.mute = false;
			//audioSourceBtnRight.mute = false;
			//audioSourceBtnDice.mute = false;
			//audioSourceFood.mute = false;
			//audioSourceDrink.mute = false;

			setSounds(false);

			lblSound.text = "Geluid uit";
		}
		else
		{
			//audioSourceBtnUp.mute = true;
			//audioSourceBtnDown.mute = true;
			//audioSourceBtnLeft.mute = true;
			//audioSourceBtnRight.mute = true;
			//audioSourceBtnDice.mute = true;
			//audioSourceFood.mute = true;
			//audioSourceDrink.mute = true;

			setSounds (true);

			lblSound.text = "Geluid aan";
		}
	}

	public void setSounds(bool mute)
	{
		audioSourceBtnUp.mute = mute;
		audioSourceBtnDown.mute = mute;
		audioSourceBtnLeft.mute = mute;
		audioSourceBtnRight.mute = mute;
		audioSourceBtnDice.mute = mute;
		audioSourceFood.mute = mute;
		audioSourceDrink.mute = mute;
	}

	public void setMusic(bool mute)
	{
		audioSourceMain.mute = mute;
	}
}
