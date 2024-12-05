using UnityEngine;

public class DialogController
{
    private int _npcId;

    public void Start(int npcId)
    {
        UIManager.Instance.ChangeHUDUI<UI_Cinematic>();

        _npcId = npcId;
    }

    public void Stop()
    {
        UIManager.Instance.ChangeHUDUI<UI_HUD>();
    }

    public void Show(int index)
    {
        UI_Cinematic cinematic = UIManager.Instance.GetRegisteredUI<UI_Cinematic>();
        if (cinematic == null)
        {
            Debug.Log("CinematicUI is not registered");
            return;
        }

        string line = DialogDBManager.GetText(_npcId, index);
    }
}
