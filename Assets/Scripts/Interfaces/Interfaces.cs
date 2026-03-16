using System;
using UnityEngine;
using Units.Warrior;
using Game.SpawnSystem;

public interface ICenterController
{
    public event Action<WarriorState> OnStateChanged;
    public event EventHandler OnDeath;
    public event Action<float> OnDamageTaken;
    public Side Side { get; }
    public ICenterController EnemyWarrior { get; }
    public Vector3 Pos { get; }
    public Vector3 EnemyPos { get; }
    public float LastDamage { get; }   

    public void TakeDamage(float damage);
    public void Death();
    public void DestroyYourself();
}

public interface IAttackable
{
    public event EventHandler OnAttack;

    public void BoostAttack(float boost);
    public void BoostAttackSpeed(float boost);
}

public interface IMovable
{
    public void BoostSpeed(float boost);
}

public interface IHpable
{
    public event Action<float> OnHpChangedInPercent;

    public void BoostDefence(float boost);
    public void BoostHp(float boost);
}

public interface IBoostable
{
    public void SetBoost(BoostType[] boost);
}
