using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class PawnColor
{
    private Color color;
    private string colorString;
	private int colorId;

    public Color getColor()
    {
        return color;
    }

	public int getColorId()
	{
		return colorId;
	}

    public string getColorString()
    {
        return colorString;
    }

    public PawnColor(Color color, string colorString, int colorId)
    {
        this.color = color;
        this.colorString = colorString;
		this.colorId = colorId;
    }
}