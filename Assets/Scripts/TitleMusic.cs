using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(AudioSource))]
class titleMusic : MonoBehaviour
{
	private static titleMusic instance = null;
	public static titleMusic Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (titleMusic)FindObjectOfType(typeof(titleMusic));
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