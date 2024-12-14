using System.Collections;
using UnityEngine;

public class Scene0Controller : SceneConrollerBase
{
    protected override IEnumerator InitialzeSceneUI()
    {
        yield return new WaitForEndOfFrame();
        UIManager.Instance.ChangeHUDUI<UI_MainScene>();
    }

    protected override IEnumerator InitialzePlayer()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("Scene0���� �÷��̾ �����ϴ�!!");
    }

}
