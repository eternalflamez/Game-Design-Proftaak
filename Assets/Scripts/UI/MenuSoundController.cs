using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuSoundController : MonoBehaviour
{
	[SerializeField]
	private GameObject btnMusic;
	[SerializeField]
	private GameObject btnSounds;

	private Image imgMusic;
	private Image imgSounds;

	private AudioSource musicAudioSource;

	[SerializeField]
	private List<AudioSource> soundsAudioSources;
	
	[SerializeField]
	private Sprite music;
	[SerializeField]
	private Sprite noMusic;
	[SerializeField]
	private Sprite sounds;
	[SerializeField]
	private Sprite noSounds;

	// Use this for initialization
	void Start ()
	{
		TitleMusic titleMusic = (TitleMusic)FindObjectOfType(typeof(TitleMusic));
		musicAudioSource = (AudioSource)titleMusic.GetComponent <AudioSource>();

		imgMusic = (Image)btnMusic.GetComponent<Image> ();
		imgSounds = (Image)btnSounds.GetComponent<Image> ();

		if ( InformationManager.instance.musicSettings == "Music")
		{
			imgMusic.sprite = music;
			musicAudioSource.mute = false;
		}
		else
		{
			imgMusic.sprite = noMusic;
			musicAudioSource.mute = true;
		}

		if (InformationManager.instance.soundsSettings == "Sounds")
		{
			imgSounds.sprite = sounds;
			setSounds(false);
		}
		else
		{
			imgSounds.sprite = noSounds;
			setSounds(true);
		}
	}

	public void setSounds(bool mute)
	{
		Debug.Log ("setSound: " + mute);
		for (int index = 0; index < soundsAudioSources.Count; index++)
		{
			Debug.Log ("AudioListener: " + index);
			soundsAudioSources[index].mute = mute;
		}
	}

	public void btnSounds_Click()
	{
		Debug.Log ("Sounds Clicked");

		if (imgSounds.sprite.name == "Sounds")
		{
			Debug.Log ("Name==Sounds");
			imgSounds.sprite = noSounds;

			setSounds(true);

			InformationManager.instance.soundsSettings = "NoSounds";
		}
		else
		{
			imgSounds.sprite = sounds;

			setSounds(false);

			InformationManager.instance.soundsSettings = "Sounds";
		}
	}
	public void btnMusic_Click()
	{
		Debug.Log ("Music Clicked: " + imgMusic.sprite.name);
		if (imgMusic.sprite.name == "Volume")
		{
			imgMusic.sprite = noMusic;
			
			musicAudioSource.mute = true;

			InformationManager.instance.musicSettings = "NoMusic";
		}
		else
		{
			imgMusic.sprite = music;
			
			musicAudioSource.mute = false;

			InformationManager.instance.musicSettings = "Music";
		}
	}
}
