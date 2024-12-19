using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StandingNPC))]
public class DialogEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var dialogPlayer = (StandingNPC)target;
        if(GUILayout.Button("Play"))
        {
            //dialogPlayer.Test_PlayTimeline();
        }
    }
}
