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
        Debug.Log("Scene0에는 플레이어가 없습니다!!");
    }

}
