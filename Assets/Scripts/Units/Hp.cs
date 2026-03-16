using UnityEngine;
using System;

namespace Units
{

    public class Hp : MonoBehaviour, IHpable
    {
        [SerializeField] private float maxHp = 100f;
        [SerializeField] private float armor = 1f;

        private float _currentHp;

        private ICenterController _centerController;

        public event Action<float> OnHpChangedInPercent;

        private void Awake()
        {
            _currentHp = maxHp;
            _centerController = GetComponentInParent<ICenterController>();
            _centerController.OnDamageTaken += TakeDamage;
        }

        public void BoostDefence(float boost)
        {
            armor += boost;
        }

        public void BoostHp(float boost)
        {
            maxHp += boost;
            _currentHp += boost;
        }

        private void TakeDamage(float damage)
        {
            float effectiveDamage = damage / armor;
            _currentHp -= effectiveDamage;
            OnHpChangedInPercent?.Invoke(_currentHp / maxHp);
            if (_currentHp <= 0)
            {
                _currentHp = 0;
                _centerController.Death();
            }
        }

        private void OnDestroy()
        {
            if (_centerController != null)
            {
                _centerController.OnDamageTaken -= TakeDamage;
            }
        }
    }
}
