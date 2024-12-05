using System;
using UnityEngine;

[Serializable]
public class Death
{
    public DeathReason Reason;
    public string DeathTextKey;
}

public enum DeathReason
{
    Fall,
}