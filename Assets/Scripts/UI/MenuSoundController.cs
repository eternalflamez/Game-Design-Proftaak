using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuSoundController : MonoBehaviour
{
	[SerializeField]
	private GameObject gmoSound;
	private Button btnSound;
	private Image imgSound;
	private AudioSource titleAudioSource;
	
	[SerializeField]
	private Sprite sound;
	[SerializeField]
	private Sprite noSound;

	// Use this for initialization
	void Start ()
	{
		btnSound = gmoSound.transform.GetComponent<Button>();
		imgSound = gmoSound.transform.GetComponent<Image>();

		TitleMusic titleMusic = (TitleMusic)FindObjectOfType(typeof(TitleMusic));
		titleAudioSource = (AudioSource)titleMusic.GetComponent <AudioSource>();

		if ( InformationManager.instance.soundSetting == "Sound")
		{
			imgSound.sprite = sound;
			titleAudioSource.mute = false;
		}
		else
		{
			imgSound.sprite = noSound;
			titleAudioSource.mute = true;
		}
	}
	
	public void btnSound_Click()
	{
		if (imgSound.sprite.name == "Volume")
		{
			imgSound.sprite = noSound;
			
			titleAudioSource.mute = true;

			InformationManager.instance.soundSetting = "NoSound";
		}
		else
		{
			imgSound.sprite = sound;
			
			titleAudioSource.mute = false;

			InformationManager.instance.soundSetting = "Sound";
		}
	}
}
