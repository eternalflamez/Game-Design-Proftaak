using UnityEngine;
using System.Collections;

public class Player
{
	public float insulinReserves = 8;

	public string name;
	public int age = 0;
	public int height;
	public double weight;
	public string gender;

	public PlayerModel model;
	public Pawn pawn;

	public Food lastEaten;

	public void setInfo(string name, int age, int height, double weight, string gender)
	{
		this.name = name;
		this.age = age;
		this.height = height;
		this.weight = weight;
		this.gender = gender;

		model = new PlayerModel(5);
	}

	public void eat(Food food)
	{
		model.eat(food);
	}

	public void walk(int minutes)
	{
		model.shiftTime (minutes);
	}

	public void addInsulinReserves(float insulin)
	{
		this.insulinReserves += insulin;
	}
	
	public void useInsulinReserves(float amount)
	{
		this.insulinReserves -= amount;
		model.useInsulin (amount);
	}
}
