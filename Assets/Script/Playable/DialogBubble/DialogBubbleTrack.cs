using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0f, 0.5f, 1f)]
[TrackClipType(typeof(DialogBubbleClip))]
public class DialogBubbleTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<DialogBubbleMixerBehaviour>.Create (graph, inputCount);
    }
}
