using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Food
{
	private string name;
	private string description;
	private float carbohydrates;
	private float absorbsionTime;
	private float time;
	private float carbConst;

	public Food(string name, string description, float carbohydrates, float absorbsionTime)
	{
	    this.name = name;
	    this.carbohydrates = carbohydrates;
	    this.absorbsionTime = absorbsionTime;
	    carbConst = carbohydrates / 4;
	}

	public string getName()
	{
	    return name;
	}
	public string getDescription()
	{
		return description;
	}
	public float getCarbs()
	{
	    return carbohydrates;
	}

	public void setCarbs(float carbs)
	{
	    this.carbohydrates = carbs;
	}

	public float getAbsorbionTime()
	{
	    return absorbsionTime;
	}

	public float getTime()
	{
	    return time;
	}

	public void setTime(float time)
	{
	    this.time = time;
	}

	public float getCarbConst()
	{
	    return this.carbConst;
	}
}