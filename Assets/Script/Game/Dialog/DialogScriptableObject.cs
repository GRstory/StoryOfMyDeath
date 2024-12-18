using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog_", menuName = "Dialog/Dialog")]

public class DialogScriptableObject : ScriptableObject
{
    public List<LineData> lineList = new List<LineData>();
}

