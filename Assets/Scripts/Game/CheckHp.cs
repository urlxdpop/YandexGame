using System;
using Units;
using UnityEngine;

namespace Game
{

    public class CheckHp : MonoBehaviour
    {
        [SerializeField] private GameObject Base;
        [SerializeField] private GameObject EnemyBase;

        private IHpable _baseHp;
        private IHpable _enemyBaseHp;

        public event EventHandler OnPlayerWin;
        public event EventHandler OnEnemyWin;

        private void Awake()
        {
            _baseHp = Base.GetComponentInChildren<Hp>();
            _enemyBaseHp = EnemyBase.GetComponentInChildren<Hp>();
        }

        private void Start()
        {
            _baseHp.OnHpChangedInPercent += BaseHp_OnHpChangedInPercent;
            _enemyBaseHp.OnHpChangedInPercent += EnemyBaseHp_OnHpChangedInPercent;
        }

        private void BaseHp_OnHpChangedInPercent(float e)
        {
            if (e <= 0)
            {
                OnEnemyWin?.Invoke(this, EventArgs.Empty);
            }
        }

        private void EnemyBaseHp_OnHpChangedInPercent(float e)
        {
            if (e <= 0)
            {
                OnPlayerWin?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
