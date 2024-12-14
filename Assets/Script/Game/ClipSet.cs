using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(menuName = "Animation/ClipSet", fileName = "ClipSet_")]
public class ClipSet : ScriptableObject
{
    [SerializeField] private List<ClipSetData> _clipsetList = new List<ClipSetData>();
    private Dictionary<string, AnimationClip> _clipDict = new Dictionary<string, AnimationClip>();

    public AnimationClip GetClip(string key)
    {
        return _clipsetList.FirstOrDefault(data => data.key == key).clip;
    }
}

[Serializable]
public class ClipSetData
{
    public string key;
    public AnimationClip clip;
}
