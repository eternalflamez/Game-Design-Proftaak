using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
class backNextSfx : MonoBehaviour
{
	private static backNextSfx instance = null;
	public static backNextSfx Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (backNextSfx)FindObjectOfType(typeof(backNextSfx));
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