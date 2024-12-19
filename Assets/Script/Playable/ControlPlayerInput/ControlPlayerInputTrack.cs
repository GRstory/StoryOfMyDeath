using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.2485139f, 1f, 0f)]
[TrackClipType(typeof(ControlPlayerInputClip))]
public class ControlPlayerInputTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<ControlPlayerInputMixerBehaviour>.Create (graph, inputCount);
    }
}
