using System;
using UnityEngine;

public interface IHealth
{
    int Health { get; }
    int MaxHealth { get; }
    event Action OnDead;

    int GetDamage(int amount);

    void RecoverHealth();

    void Die(Death deathType);
}
