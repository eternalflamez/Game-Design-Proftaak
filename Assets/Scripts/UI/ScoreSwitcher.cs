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
	private GameObject pnlTrophys;

	[SerializeField]
	private GameObject gmoGraph;
	[SerializeField]
	private GameObject gmoJournalForward;
	[SerializeField]
	private GameObject gmoTrophy;
	[SerializeField]
	private GameObject gmoJournalBack;

	[SerializeField]
	private Button btnGraph;
	[SerializeField]
	private Button btnJournalForward;
	[SerializeField]
	private Button btnThrophyForward;
	[SerializeField]
	private Button btnJournalBack;

	public void btnJournalForward_Click()
	{
		pnlGraph.SetActive (false);
		pnlJournal.SetActive (true);
		pnlTrophys.SetActive (false);

		btnGraph.interactable = true;
		btnThrophyForward.interactable = true;

		gmoJournalForward.SetActive (false);
		gmoJournalBack.SetActive (false);
		gmoGraph.SetActive (true);
		gmoTrophy.SetActive (true);
	}
	public void btnGraph_Click()
	{
		pnlGraph.SetActive (true);
		pnlJournal.SetActive (false);
		pnlTrophys.SetActive (false);
		
		btnJournalForward.interactable = true;
		btnGraph.interactable = false;

		gmoGraph.SetActive (true);
		gmoJournalForward.SetActive (true);
		gmoJournalBack.SetActive (false);
		gmoTrophy.SetActive (false);
	}
	public void btnJournalBack_Click()
	{
		pnlGraph.SetActive (false);
		pnlJournal.SetActive (true);
		pnlTrophys.SetActive (false);
		
		btnThrophyForward.interactable = true;
		btnGraph.interactable = true;

		gmoTrophy.SetActive (true);
		gmoGraph.SetActive (true);
		gmoJournalForward.SetActive (false);
		gmoJournalBack.SetActive (false);
	}
	public void btnTrophyForward_Click()
	{
		pnlGraph.SetActive (false);
		pnlJournal.SetActive (false);
		pnlTrophys.SetActive (true);

		btnJournalBack.interactable = true;
		btnThrophyForward.interactable = false;

		gmoGraph.SetActive (false);
		gmoJournalForward.SetActive (false);
		gmoJournalBack.SetActive (true);
		gmoTrophy.SetActive (true);
	}
}
