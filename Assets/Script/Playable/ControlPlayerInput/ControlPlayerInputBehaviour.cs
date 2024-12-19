using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ControlPlayerInputBehaviour : PlayableBehaviour
{
    private bool _isStart = false;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);

        InputManagerEx.SetPlayerInput(false);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        base.OnBehaviourPause(playable, info);

        if (info.effectivePlayState == PlayState.Paused && Mathf.Approximately((float)playable.GetTime(), (float)playable.GetDuration()))
        {
            InputManagerEx.SetPlayerInput(true);
        }

        if(_isStart) InputManagerEx.SetPlayerInput(true);
        _isStart = true;
        
    }
}
