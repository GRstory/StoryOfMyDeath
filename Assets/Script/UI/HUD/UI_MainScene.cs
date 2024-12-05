using UnityEngine;

public class UI_MainScene : UI_HUD
{
    public override void Active()
    {
        base.Active();
    }

    public override void Deactive()
    {
        base.Deactive();
    }

    public void StartButton()
    {
        SceneManagerEx.Instance.LoadScene("Scene1");
    }
}
