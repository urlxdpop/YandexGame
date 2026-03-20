using System;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Warrior
{
    public enum WarriorState
    {
        Idle,
        Walking,
        Attacking,
        Dead
    }

    public enum WarriorType
    {
        Pistol,
        Musketeer,
        Gunner,
        Sniper,
        MashineGunner,
        Shielder,
        Tank,
        Artillery
    }

    public enum Side
    {
        Player,
        Enemy
    }

    [SelectionBase]
    public class Warrior : MonoBehaviour, ICenterController
    {
        // Fields
        [SerializeField] private Side side;
        [SerializeField] private WarriorType type;

        private WarriorState _state;
        private float _lastDamage;
        private Vector3 _enemyPos;

        private ICenterController _opponent;
        private readonly List<ICenterController> _targets = new();

        // Properties
        public Side Side => side;
        public ICenterController EnemyWarrior => _opponent;
        public Vector3 Pos => transform.position;
        public Vector3 EnemyPos => _enemyPos;
        public float LastDamage => _lastDamage;

        //Events
        public event Action<WarriorState> OnStateChanged;
        public event EventHandler OnDeath;
        public event Action<float> OnDamageTaken;

        private void OnValidate()
        {
            if (side == Side.Enemy)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        private void Start()
        {
            SetState(WarriorState.Walking);

            if (side == Side.Enemy)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        private void Update()
        {
            ClampToCamera();

            if (_state == WarriorState.Dead) return;
            if (_targets.Count > 0)
            {
                _opponent = _targets[0];
                _enemyPos = _opponent.Pos;
                SetState(WarriorState.Attacking);
            } else
            {
                _opponent = null;
                SetState(WarriorState.Walking);
            }
        }

        public void TakeDamage(float damage)
        {
            _lastDamage = damage;
            if (_state == WarriorState.Dead) return;
            OnDamageTaken?.Invoke(damage);
        }

        public void Death()
        {
            if (_state == WarriorState.Dead) return;
            SetState(WarriorState.Dead);
            OnDeath?.Invoke(this, EventArgs.Empty);
            GetComponent<Collider2D>().enabled = false;
        }

        public void DestroyYourself()
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                _targets[i].OnDeath -= RemoveTarget;
            }
            Destroy(gameObject);
        }

        private void SetState(WarriorState newState)
        {
            if (_state == newState) return;

            _state = newState;
            OnStateChanged?.Invoke(_state);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ICenterController enemyController))
            {
                if (enemyController.Side != side)
                {
                    _targets.Add(enemyController);
                    enemyController.OnDeath += RemoveTarget;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ICenterController enemyController))
            {
                _targets.Remove(enemyController);
                enemyController.OnDeath -= RemoveTarget;
            }
        }

        private void ClampToCamera()
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

        private void RemoveTarget(object sender, EventArgs e)
        {
            if (sender is ICenterController enemyController)
            {
                _targets.Remove(enemyController);

                enemyController.OnDeath -= RemoveTarget;

                if (_opponent == enemyController)
                    _opponent = null;
            }
        }
    }
}
