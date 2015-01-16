using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Player
{
    private int id;
    private float insulinReserves = 8;

    public string name;
    public int age = 0;
    public int height;
    public float weight;
    public Gender gender;
	private int usedSugar = 0;
	private int maxInsulin = 10;

    private PlayerModel model;
    private Pawn pawn;
    private Color pawnColor;

	public bool skipsTurn = false;

	public Food lastEaten;

    public int getId()
    {
        return id;
    }

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

	public float getInsulineReserve()
	{
		return insulinReserves;
	}

    public void setPawn(Pawn p)
    {
        this.pawn = p;
        this.pawn.setColor(pawnColor);
    }

    public Pawn getPawn()
    {
        return pawn;
    }

    public string getFileName()
    {
        return this.name + "-" + this.age + "-" + this.gender.ToString().ToCharArray()[0];
    }

	public void setInfo(int id, string name, int age, int height, float weight, Gender gender, Color color, int maxInsulin)
	{
        this.id = id;
		this.name = name;
		this.age = age;
		this.height = height;
		this.weight = weight;
		this.gender = gender;
        this.pawnColor = color;
		this.maxInsulin = maxInsulin;

		model = new PlayerModel(5);
	}

	public int getUsedSugar()
	{
		return usedSugar;
	}
	public void useSugar()
	{
		usedSugar++;
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
		if (this.insulinReserves < maxInsulin)
		{
			this.insulinReserves += insulin;
		}
	}
	
	public bool useInsulinReserves(float amount)
	{
		if ((this.insulinReserves - amount) > 0)
		{
			this.insulinReserves -= amount;
			model.useInsulin (amount);

			return true;
		}

		return false;
	}
}
