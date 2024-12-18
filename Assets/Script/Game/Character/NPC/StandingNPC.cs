using I2.Loc;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class StandingNPC : MonoBehaviour, IInteraction, ICharacter
{
    [SerializeField] private DialogScriptableObject _dialogSO;
    [SerializeField] private string _characterId = "";
    [SerializeField] private Color _characterColor = Color.black;

    private CapsuleCollider2D _capsuleCollider;
    private PlayableDirector _director;
    private List<DialogData> _bubbleDataList = new List<DialogData>();

    public string CharacterId { get => _characterId; }
    public Color DialogColor { get => _characterColor; }

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _director = GetComponent<PlayableDirector>();

        NPCManager.RegisterNPC(this, gameObject);
        CreateBubbleData();
        SetClip();
    }

    public void Interaction()
    {
        if (_director == null || _director.state == PlayState.Playing) return;
        _director.Play();
    }

    public void OnPlayerTriggerEnter()
    {
        
    }

    public void OnPlayerTriggerExit()
    {
        
    }

    private void CreateBubbleData()
    {
        if(!_dialogSO) return;

        foreach (LineData line in _dialogSO.lineList)
        {
            string dialog = LocalizationManager.GetTranslation("Dialog/"+line.dialogId);
            string name = LocalizationManager.GetTranslation(line.npcId);
            _bubbleDataList.Add(new DialogData(dialog, line.npcId));
        }
    }

    private void SetClip()
    {
        if (!_director) return;
        TimelineAsset timelineAsset = _director.playableAsset as TimelineAsset;
        var track = timelineAsset.GetRootTracks()
            .OfType<DialogBubbleTrack>()
            .FirstOrDefault();

        if (!track) return;
        int index = 0;
        foreach (var clip in track.GetClips())
        {
            if (clip.asset is DialogBubbleClip dialogBubbleClip && _bubbleDataList.Count >= index)
            {
                if (dialogBubbleClip is DialogBubbleExitClip) continue;
                dialogBubbleClip.dynamicBubbleData = _bubbleDataList[index];
                index++;
            }
        }
    }

    public void Test_PlayTimeline()
    {
        /*if (_director.state == PlayState.Playing || !DialogUIBubble.Instance.IsFinish())
        {
            double currentTime = _director.time;
            TimelineAsset timeline = _director.playableAsset as TimelineAsset;

            var track = timeline.GetRootTracks()
            .OfType<DialogBubbleTrack>()
            .FirstOrDefault();

            DialogUIBubble.Instance._dialogTextWriter.SkipTypewriter();
        }
        else
        {
            _director.Play();
            _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
*/

        if(_director.state != PlayState.Playing)
        {
            _director.Evaluate();
            _director.Play();
            _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
        else
        {
            double speed = _director.playableGraph.GetRootPlayable(0).GetSpeed();
            bool finish = DialogUIBubble.Instance.IsFinish();
            if (_director.playableGraph.GetRootPlayable(0).GetSpeed() != 0 || !DialogUIBubble.Instance.IsFinish())
            {
                double currentTime = _director.time;
                TimelineAsset timeline = _director.playableAsset as TimelineAsset;

                var track = timeline.GetRootTracks()
                .OfType<DialogBubbleTrack>()
                .FirstOrDefault();

                DialogUIBubble.Instance._dialogTextWriter.SkipTypewriter();
            }
            else
            {
                _director.Play();
                _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            }
        }
    }
}
