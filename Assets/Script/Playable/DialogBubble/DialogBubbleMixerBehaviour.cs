using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogBubbleMixerBehaviour : PlayableBehaviour
{
    // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        int inputCount = playable.GetInputCount ();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight <= 0f) continue;

            ScriptPlayable<DialogBubbleBehaviour> inputPlayable = (ScriptPlayable<DialogBubbleBehaviour>)playable.GetInput(i);
            DialogBubbleBehaviour input = inputPlayable.GetBehaviour();

            // Use the above variables to process each frame of this playable.
            if (!input.isStart)
            {
                input.isStart = true;
                if (input.bubbleData == null) return;
                //DialogUIBubble.Instance.Active(input.bubbleData, input.bubbleData.Name);
            }
        }
    }
}
