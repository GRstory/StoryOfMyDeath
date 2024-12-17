using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class DialogBubbleBehaviour : PlayableBehaviour
{
    public DialogData bubbleData;
    public bool isStart = false;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        PlayableDirector director = playable.GetGraph().GetResolver() as PlayableDirector;

        if (!director) return;
        if(director.state == PlayState.Playing) director.Pause();
    }
}
