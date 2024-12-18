using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Windows;

[Serializable]
public class DialogBubbleBehaviour : PlayableBehaviour
{
    public DialogData bubbleData;
    public bool isStart = false;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        DialogUIBubble.Instance.Active(bubbleData, bubbleData.Name);
        isStart = true;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        PlayableDirector director = playable.GetGraph().GetResolver() as PlayableDirector;

        if (!director) return;

        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        //director.Play();
        //if (director.state == PlayState.Playing) director.Pause();
    }
}


[Serializable]
public class DialogBubbleExitBehaviour : DialogBubbleBehaviour
{
    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        DialogUIBubble.Instance.Deactivate();
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }
}
