using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreSwitcher : MonoBehaviour
{
	[SerializeField]
	private GameObject pnlGraph;
	[SerializeField]
	private GameObject pnlJournal;

	[SerializeField]
	private Button btnGraph;
	[SerializeField]
	private Button btnJournal;

	public void btnJournal_Click()
	{
		pnlGraph.SetActive (false);
		pnlJournal.SetActive (true);

		btnJournal.interactable = false;
		btnGraph.interactable = true;
	}

	public void btnGraph_Click()
	{
		pnlGraph.SetActive (true);
		pnlJournal.SetActive (false);

		btnJournal.interactable = true;
		btnGraph.interactable = false;
	}
}
