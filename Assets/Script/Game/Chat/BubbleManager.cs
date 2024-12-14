using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private int _maxBubbleCount = 3;
    [SerializeField] GameObject _bubblePrefap;
    [SerializeField] float[] _bubbleFontSizeList = { 0.32f, 0.25f, 0.20f };
    [SerializeField] float[] _bubbleAlphaList = { 1.00f, 0.65f, 0.30f };
    [SerializeField] float[] _bubbleHeightList = { 0.55f, 0.45f, 0.40f };

    private LinkedList<Bubble> _bubbleList = new LinkedList<Bubble>();

    public bool CanAddBubble()
    {
        if (_bubbleList.Count > 0)
        {
            return _bubbleList.Last.Value.IsFinish;
        }
        else return true;
    }

    public void AddBubble(string dialog)
    {
        //이전 Bubble의 코루틴 정지 및 텍스트 사이즈 조절
        if (_bubbleList.Count > 0)
        {
            Bubble prevBubble = _bubbleList.Last.Value;
            if (prevBubble != null)
            {
                if(!prevBubble.IsFinish)
                {
                    return;
                }
                prevBubble.StopBlink();
            }
        }

        //Bubble 생성
        GameObject bubbleObject = Instantiate(_bubblePrefap, transform);
        bubbleObject.transform.SetAsFirstSibling();

        Bubble newBubble = bubbleObject.GetComponent<Bubble>();
        if (newBubble == null) return;

        //BubbleList에 추가
        _bubbleList.AddLast(newBubble);
        if (_bubbleList.Count > _maxBubbleCount)
        {
            Bubble oldBubble = _bubbleList.First.Value;
            _bubbleList.RemoveFirst();

            Destroy(oldBubble.gameObject);
        }

        //Bubble 수정
        int i = 0;
        foreach (Bubble bubble in _bubbleList.Reverse())
        {
            bubble.SetElement(_bubbleAlphaList[i], _bubbleFontSizeList[i], _bubbleHeightList[i]);
            i++;
        }

        //Text 설정
        newBubble.SetText(dialog);
    }

}
