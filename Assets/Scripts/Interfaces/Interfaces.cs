using System;
using UnityEngine;
using Units.Warrior;

public interface ICenterController
{
    public event Action<WarriorState> OnStateChanged;
    public event EventHandler OnDeath;
    public event Action<float> OnDamageTaken;
    public Side Side { get; }
    public ICenterController EnemyWarrior { get; }
    public Vector3 Pos { get; }

    public void TakeDamage(float damage);
    public void Death();
}
