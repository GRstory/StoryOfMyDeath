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
    /// UI�� Ȱ��ȭ �մϴ�. �� �Լ��� ��ӹ޴´ٸ�, base.Active()�� ���� ȣ���ؾ� �մϴ�.
    /// </summary>
    public virtual void Active()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// UI�� ��Ȱ��ȭ �մϴ�. �� �Լ��� ��ӹ޴´ٸ�, base.Deactive()�� �������� ȣ���ؾ� �մϴ�.
    /// </summary>
    public virtual void Deactive()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// ��� �ڽ� ������Ʈ�� Ÿ�Ժ���, ������Ʈ�� �̸����� ��ųʸ��� ����մϴ�.
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
    /// �̹� ��ϵ� ������Ʈ�� Ÿ�Ժ���, Ÿ�� ������ ���� �ҷ��ɴϴ�.
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
