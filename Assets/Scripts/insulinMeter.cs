using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class insulinMeter : MonoBehaviour 
{


	public int currentInsulin;
	public int startingInsulin;
	public Slider insulinSlider;
	public GameObject findInsulin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	



	}


	void Awake () {
		currentInsulin = startingInsulin;

	}





	public void collectInsulin (int amount)
	{
		//foundInsulin = true;
		amount = 10;
		currentInsulin += amount;
		insulinSlider.value = currentInsulin;
		Debug.Log (currentInsulin);
	}


}
