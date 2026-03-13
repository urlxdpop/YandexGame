using System;
using Units.Warrior;
using UnityEngine;

namespace Units.Base
{
    public class Base : MonoBehaviour, ICenterController
    {
        // Fields
        [SerializeField] private Side side;

        private bool _isDead;

        // Properties
        public Side Side => side;
        public Vector3 Pos => Vector3.zero;

        // Events
        public event EventHandler OnDeath;
        public event Action<float> OnDamageTaken;

        // Implementing ICenterController (non realization)
        public event Action<WarriorState> OnStateChanged { add { } remove { } }
        public ICenterController EnemyWarrior => null;

        public void TakeDamage(float damage)
        {
            if (_isDead) return;
            OnDamageTaken?.Invoke(damage);
        }

        public void Death()
        {
            _isDead = true;
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
