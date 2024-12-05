using System.Collections;
using UnityEngine;

public class Scene1Controller : SceneConrollerBase
{
    protected override IEnumerator InitialzePlayer()
    {
        yield return null;
    }

    protected override IEnumerator InitialzeSceneUI()
    {
        yield return new WaitForEndOfFrame();

        UIManager.Instance.ChangeHUDUI<UI_Dialog>();
    }
}
