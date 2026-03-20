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
        public Vector3 EnemyPos => Vector3.zero;

        private void OnValidate()
        {
            if (side == Side.Enemy)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        private void Update()
        {
            ClampToCamera();
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
            GetComponent<Collider2D>().enabled = false;
        }

        void ClampToCamera()
        {
            Camera cam = Camera.main;

            float height = cam.orthographicSize;
            float width = height * cam.aspect;

            Vector3 pos = transform.position;

            Vector3 camPos = cam.transform.position;

            pos.x = Mathf.Clamp(pos.x, camPos.x - width, camPos.x + width);
            pos.y = Mathf.Clamp(pos.y, camPos.y - height, camPos.y + height);

            transform.position = pos;
        }

        public void DestroyYourself()
        {
            Destroy(gameObject);
        }
    }
}
