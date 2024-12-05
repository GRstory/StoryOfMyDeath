using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TextUI_Base : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private TMP_Text _dialogText;
    [SerializeField] private Image _characterImage;

    public int CharacterId;

    public void ActiveImage(bool active = true)
    {
        if (!active)
        {
            _characterImage.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void SetImage(Sprite sprite = null, bool active = true)
    {
        if(!active)
        {
            _characterImage.color = new Color(1f, 1f, 1f, 0f);
        }

        if (_characterImage != null)
        {
            _characterImage.sprite = sprite;
        }
    }

    public void SetDialogText(string text)
    {
        if (_dialogText != null)
        {
            _dialogText.text = text;
            LayoutRebuilder.ForceRebuildLayoutImmediate(_dialogText.rectTransform);
        }
    }
}
