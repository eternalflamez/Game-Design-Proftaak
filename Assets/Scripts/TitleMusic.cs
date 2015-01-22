using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
class TitleMusic : MonoBehaviour
{
	private static TitleMusic instance = null;
	public static TitleMusic Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (TitleMusic)FindObjectOfType(typeof(TitleMusic));
			}
			return instance;
		}
	}
	
	void Awake ()
	{
		if (Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	public void Destroy()
	{
		Destroy (gameObject);
	}
}