using System;
using Units.Warrior;
using UnityEngine;

namespace Units.Base
{

    [SelectionBase]
    public class Base : MonoBehaviour, ICenterController
    {
        // Fields
        [SerializeField] private Side side;

        private bool _isDead;

        // Properties
        public Side Side => side;
        public Vector3 Pos => transform.position;

        // Events
        public event EventHandler OnDeath;
        public event Action<float> OnDamageTaken;

        // Implementing ICenterController (non realization)
        public event Action<WarriorState> OnStateChanged { add { } remove { } }
        public ICenterController EnemyWarrior => null;
        public float LastDamage => 0;

        private void OnValidate()
        {
            if (side == Side.Enemy)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

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

        public void DestroyYourself()
        {
            Destroy(gameObject);
        }
    }
}
