using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData
{
    public string Dialog;
    public string Name;
    public Color Color;

    public DialogData(string dialog, string name, Color color)
    {
        Dialog = dialog;
        Name = name;
        Color = color;
    }   

    public DialogData(string dialog, string name)
    {
        Dialog = dialog;
        Name = name;
        Color = Color.black;
    }

    public DialogData(string dialog)
    {
        Dialog = dialog;
        Name = "";
        Color = Color.black;
    }
}
