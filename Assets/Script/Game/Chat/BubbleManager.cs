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
        //���� Bubble�� �ڷ�ƾ ���� �� �ؽ�Ʈ ������ ����
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

        //Bubble ����
        GameObject bubbleObject = Instantiate(_bubblePrefap, transform);
        bubbleObject.transform.SetAsFirstSibling();

        Bubble newBubble = bubbleObject.GetComponent<Bubble>();
        if (newBubble == null) return;

        //BubbleList�� �߰�
        _bubbleList.AddLast(newBubble);
        if (_bubbleList.Count > _maxBubbleCount)
        {
            Bubble oldBubble = _bubbleList.First.Value;
            _bubbleList.RemoveFirst();

            Destroy(oldBubble.gameObject);
        }

        //Bubble ����
        int i = 0;
        foreach (Bubble bubble in _bubbleList.Reverse())
        {
            bubble.SetElement(_bubbleAlphaList[i], _bubbleFontSizeList[i], _bubbleHeightList[i]);
            i++;
        }

        //Text ����
        newBubble.SetText(dialog);
    }

}
