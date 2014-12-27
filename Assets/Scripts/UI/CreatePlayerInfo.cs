using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class CreatePlayerInfo : MonoBehaviour {
    [SerializeField]
    private Text playerHeader;
    [SerializeField]
    private Text nameError;
    [SerializeField]
    private Text ageError;
    [SerializeField]
    private Text heightError;
    [SerializeField]
    private Text weightError;
    [SerializeField]
    private Text genderError;
    [SerializeField]
    private Text colorError;

    private string pname;
    private int age;
    private int height;
    private float weight;
    private Gender gender;
    private Color color;

    void Start()
    {
        pname = "";
        age = 0;
        height = 0;
        weight = 0;
        gender = Gender.None;
        color = Color.clear;
        playerHeader.text = "Player " + (InformationManager.instance.getPlayerCount() + 1);

        nameError.enabled = false;
        ageError.enabled = false;
        heightError.enabled = false;
        weightError.enabled = false;
        genderError.enabled = false;
        colorError.enabled = false;
    }

    public void setName(string name)
    {
        this.pname = name;
    }

    public void setAge(string age)
    {
        int result;
        if (int.TryParse(age, out result))
        {
            this.age = result;
        }
    }

    public void setHeight(string height)
    {
        int result;
        if (int.TryParse(height, out result))
        {
            this.height = result;
        }
    }

    public void setWeight(string weight)
    {
        float result;
        if (float.TryParse(weight, out result))
        {
            this.weight = result;
        }
    }

    public void setGender(string gender)
    {
        this.gender = (Gender)Enum.Parse(typeof(Gender), gender);
    }

    public void setPlayer()
    {
        bool error = false;

        error = error || (nameError.enabled = (pname == ""));
        error = error || (ageError.enabled = (age == 0));
        error = error || (heightError.enabled = (height == 0));
        error = error || (weightError.enabled = (weight == 0));
        error = error || (genderError.enabled = (gender == Gender.None));
        error = error || (colorError.enabled = (color == Color.clear));

        if(!error)
        {
            InformationManager.instance.addPlayer(pname, age, height, weight, gender, color);
        }
    }

    public void setColor(string color)
    {
        switch (color)
        {
            case "Red":
                this.color = Color.red;
                break;

            case "Yellow":
                this.color = Color.yellow;
                break;

            case "Green":
                this.color = Color.green;
                break;

            case "Blue":
                this.color = Color.blue;
                break;

            case "Purple":
                this.color = new Color(170, 0, 255);
                break;
        }
		else
		{
			//TODO: show error
		}
    }
}
