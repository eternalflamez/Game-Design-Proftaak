using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CreatePlayerInfo : MonoBehaviour {
    // (string name, int age, int height, float weight, Gender gender)
    private string pname;
    private int age;
    private int height;
    private float weight;
    private Gender gender;

    void Start()
    {
        pname = "";
        age = 0;
        height = 0;
        weight = 0;
        gender = Gender.None;
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
        // TODO:
    }

    public void setPlayer()
    {
        if (pname != "" && age != 0 && height != 0 && weight != 0 && gender != Gender.None)
        {
            InformationManager.instance.addPlayer(pname, age, height, weight, gender);
        }
    }
}
