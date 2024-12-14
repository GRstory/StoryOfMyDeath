using System.Collections;
using UnityEngine;

public abstract class SceneConrollerBase : MonoBehaviour
{
    protected virtual void Start()
    {
        StartCoroutine(InitialzeSceneUI());
        StartCoroutine(InitialzePlayer());
    }

    /// <summary>
    /// 씬 초기화시 필요한 함수입니다.
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator InitialzeSceneUI();

    /// <summary>
    /// 씬 초기화시 플레이어를 초기화 합니다. 현재 미완성 코드임
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator InitialzePlayer();
}
