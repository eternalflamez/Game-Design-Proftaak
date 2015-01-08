using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gatherInsulin : MonoBehaviour {

	public Scrollbar InsulinBar;
	public float Insulin = 100;

	public void Amount(float value)
	{
		Insulin -= value;
		InsulinBar.size = Insulin/100f;
	}

	public void UseInsulin(float value)
	{
		Insulin += value;
		InsulinBar.size = Insulin * 100f;
	}
}
