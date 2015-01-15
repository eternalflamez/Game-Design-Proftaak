using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Food
{
    public string name;
    public string description;
    public float carbohydrates;
    public float absorbsionTime;
	private float time;
	private float carbConst;

    public Food()
    {

    }

	public Food(string name, string description, float carbohydrates, float absorbsionTime)
	{
	    this.name = name;
		this.description = description;
	    this.carbohydrates = carbohydrates;
	    this.absorbsionTime = absorbsionTime;
        this.time = absorbsionTime;
	    carbConst = carbohydrates;
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