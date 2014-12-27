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

    public Color getColor()
    {
        return color;
    }

    public string getColorString()
    {
        return colorString;
    }

    public PawnColor(Color color, string colorString)
    {
        this.color = color;
        this.colorString = colorString;
    }
}