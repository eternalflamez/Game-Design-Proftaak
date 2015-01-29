using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField]
	private GameObject pnlMenu;
	[SerializeField]
	private GameObject pnlAbout;
	[SerializeField]
	private GameObject pnlRules;

	public void btnAbout_Click()
	{
		pnlMenu.SetActive (false);
		pnlAbout.SetActive (true);
		pnlRules.SetActive(false);
	}
	public void btnRules_Click()
	{
		pnlMenu.SetActive (false);
		pnlAbout.SetActive (false);
		pnlRules.SetActive (true);
	}
	public void btnBack_click()
	{
		pnlMenu.SetActive (true);
		pnlAbout.SetActive (false);
		pnlRules.SetActive (false);
	}

	public void btnQuit_Click()
	{
		Application.Quit ();
	}
}
