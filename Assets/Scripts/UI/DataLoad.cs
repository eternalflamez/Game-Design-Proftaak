using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DataLoad : MonoBehaviour
{
	[SerializeField]
	private Text txtTurns;
	[SerializeField]
	private Slider sldTurns;

	[SerializeField]
	private List<GameObject> playerButtons;

	[SerializeField]
	private Button btnNext;

	public static DataLoad instance;
	
	void Awake()
	{
		instance = this;
	}
	
	// Use this for initialization
	void Start ()
	{
		if (InformationManager.instance != null)
		{
			if (Application.loadedLevelName == "aantalBeurtenScreen")
			{
				setTurns();
			}
		}
	}

	private void setTurns()
	{
		txtTurns.text = InformationManager.instance.getMaxTurns ().ToString();
		sldTurns.value = InformationManager.instance.getMaxTurns ();
	}

	public void setPlayers()
	{
		int playerCount = (int)InformationManager.instance.getPlayerCount ();

		if (playerCount > 0)
		{
			int pnlNumber = playerCount - 1;

			PanelVisibility pnlVisibility = (PanelVisibility) playerButtons[pnlNumber].GetComponent<PanelVisibility>();
			pnlVisibility.setPanelVisibility();
			btnNext.interactable = true;
		}
	}
}
