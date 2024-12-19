using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ControlPlayerInputClip : PlayableAsset, ITimelineClipAsset
{
    public ControlPlayerInputBehaviour template = new ControlPlayerInputBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ControlPlayerInputBehaviour>.Create (graph, template);
        ControlPlayerInputBehaviour clone = playable.GetBehaviour ();
        return playable;
    }
}
