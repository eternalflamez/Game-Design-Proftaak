using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField]
	private GameObject pnlMenu;
	[SerializeField]
	private GameObject pnlAbout;

	public void btnAbout_Click()
	{
		pnlMenu.SetActive (false);
		pnlAbout.SetActive (true);
	}
	public void btnBack_click()
	{
		pnlMenu.SetActive (true);
		pnlAbout.SetActive (false);
	}

	public void btnQuit_Click()
	{
		Application.Quit ();
	}
}
