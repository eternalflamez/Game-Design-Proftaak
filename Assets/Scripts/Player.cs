using UnityEngine;
using System.Collections;

public class Player
{
	public float insulinReserves = 8;

    private string name;
    private int age = 0;
    private int height;
    private float weight;
    private Gender gender;

    private PlayerModel model;
    private Pawn pawn;

	public Food lastEaten;

    public string getName()
    {
        return name;
    }

    public int getAge()
    {
        return age;
    }

    public int getHeight()
    {
        return height;
    }

    public float getWeight()
    {
        return weight;
    }

    public Gender getGender()
    {
        return gender;
    }

    public PlayerModel getModel()
    {
        return model;
    }

    public Pawn getPawn()
    {
        return pawn;
    }

	public void setInfo(string name, int age, int height, float weight, Gender gender)
	{
		this.name = name;
		this.age = age;
		this.height = height;
		this.weight = weight;
		this.gender = gender;

        // TODO: set pawn color
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
