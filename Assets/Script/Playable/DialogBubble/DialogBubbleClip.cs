using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class DialogBubbleClip : PlayableAsset, ITimelineClipAsset
{
    public DialogBubbleBehaviour template = new DialogBubbleBehaviour ();
    public DialogData dynamicBubbleData;

    public override double duration
    {
        get { return 1f; }
    }

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogBubbleBehaviour>.Create (graph, template);
        DialogBubbleBehaviour clone = playable.GetBehaviour();
        clone.bubbleData = dynamicBubbleData;
        return playable;
    }
}
