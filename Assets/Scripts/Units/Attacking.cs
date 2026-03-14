using UnityEngine;
using Units.Warrior;
using System.Collections.Generic;
using System;

namespace Units.Combat
{
    public class Attacking : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float attackCooldown;
        [SerializeField] private LayerMask UnitsLayer;

        [Header("AOE Settings")]
        [SerializeField] private bool isAreaDamage;
        [SerializeField] private float areaRadius;

        private bool _isAttacking;
        private float _attackTimer;
        private ICenterController _centerController;

        public event EventHandler OnAttack;

        private void Awake()
        {
            _centerController = GetComponentInParent<ICenterController>();

            _centerController.OnStateChanged += CenterController_OnAttack;

            _attackTimer = attackCooldown;
        }

        private void Update()
        {
            if (_isAttacking) Attack();
        }

        private void Attack()
        {
            if (_attackTimer <= 0)
            {
                if (_centerController.EnemyWarrior != null)
                {
                    if (isAreaDamage)
                    {
                        TakeDamegeArea();
                    } else
                    {
                        _centerController.EnemyWarrior.TakeDamage(damage);
                    }
                    OnAttack?.Invoke(this, EventArgs.Empty);
                }

                _attackTimer = attackCooldown;
            } else
            {
                _attackTimer -= Time.deltaTime;
            }
        }

        private void TakeDamegeArea()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_centerController.EnemyWarrior.Pos, areaRadius, UnitsLayer);

            HashSet<ICenterController> damagedEnemies = new();
            foreach (var hitCollider in hitColliders)
            {
                ICenterController enemy = hitCollider.GetComponentInParent<ICenterController>();
                if (enemy != null && enemy.Side != _centerController.Side && !damagedEnemies.Contains(enemy))
                {
                    enemy.TakeDamage(damage);
                    damagedEnemies.Add(enemy);
                }
            }

        }

        private void CenterController_OnAttack(WarriorState state)
        {
            _isAttacking = (state == WarriorState.Attacking);

            if (!_isAttacking)
            {
                _attackTimer = attackCooldown;
            }
        }

        private void OnDestroy()
        {
            _centerController.OnStateChanged -= CenterController_OnAttack;
        }
    }
}
