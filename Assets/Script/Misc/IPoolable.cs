using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable<T>
{
    void Active(T t);
    void Deactivate();
}
