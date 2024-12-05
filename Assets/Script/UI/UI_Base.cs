using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UI_Base : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] protected string _tableName = "";

    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        UIManager.Instance.RegisterUI(this);

        EventHandler.OnSceneChangeEvent += Deactive;
        Deactive();
    }

    /// <summary>
    /// UI를 활성화 합니다. 이 함수를 상속받는다면, base.Active()를 먼저 호출해야 합니다.
    /// </summary>
    public virtual void Active()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// UI를 비활성화 합니다. 이 함수를 상속받는다면, base.Deactive()를 마지막에 호출해야 합니다.
    /// </summary>
    public virtual void Deactive()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// 모든 자식 오브젝트를 타입별로, 오브젝트의 이름으로 딕셔너리에 등록합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }

    /// <summary>
    /// 이미 등록된 오브젝트를 타입별로, 타입 순서에 따라 불러옵니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idx"></param>
    /// <returns></returns>
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }
}
