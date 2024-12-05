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
    /// �� �ʱ�ȭ�� �ʿ��� �Լ��Դϴ�.
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator InitialzeSceneUI();

    /// <summary>
    /// �� �ʱ�ȭ�� �÷��̾ �ʱ�ȭ �մϴ�. ���� �̿ϼ� �ڵ���
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator InitialzePlayer();
}
