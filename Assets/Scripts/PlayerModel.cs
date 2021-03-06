﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerModel
{
	private List<Food> digesting;
	private float insulin;
    private float glucose;
	private const float glucosePerCarb = .012f;
	private const float glucoseDropSpeed = 0.8f;

	public float getGlucose()
	{
	    return glucose;
	}

	public PlayerModel(float glucose)
	{
	    digesting = new List<Food>();
	    this.glucose = glucose;
	}

	public void eat(Food f)
	{
        Food newF = new Food(f.getName(), f.getDescription(), f.getCarbs(), f.getAbsorbionTime(), ItemType.None);

        digesting.Add(newF);
	}

	public void useInsulin(float amount)
	{
	    this.insulin += amount;
	}

	/// <summary>
	/// Shifts time for this player.
	/// </summary>
	/// <param name="time">The time to pass, in minutes.</param>
	public void shiftTime(float time)
	{
	    List<Food> ToRemove = new List<Food>();

	    foreach (Food item in digesting)
	    {
	        float carbs = item.getCarbs();
	        float duration = item.getAbsorbionTime();
	        float itemTime = item.getTime();
	        float carbConst = item.getCarbConst();

	        if (itemTime + time > duration)
	        {
	            itemTime = duration;
	        }
	        else
	        {
	            itemTime += time;
	        }

	        float carbsLeft = carbConst * (-Mathf.Log10(itemTime / duration));
            if (carbsLeft > carbs)
            {
                carbsLeft = carbs;
            }
            float carbsAbsorbed = carbs - carbsLeft;

	        glucose += carbsAbsorbed * glucosePerCarb;

            if (itemTime > duration || carbsLeft <= 0)
	        {
	            ToRemove.Add(item);
	        }
	        else
	        {
	            item.setCarbs(carbsLeft);
	            item.setTime(itemTime);
	        }
	    }

	    foreach (Food item in ToRemove)
	    {
	        digesting.Remove(item);
	    }

	    glucose -= (time / 60) * glucoseDropSpeed;

        if (insulin > 0)
        {
            float old = insulin;
            insulin -= 2f * (time / 30);

            if (insulin < 0)
            {
                insulin = 0;
            }

            float used = old - insulin;
            glucose -= used;
            if (glucose < 0)
            {
                glucose = 0;
            }
        }
	}
}