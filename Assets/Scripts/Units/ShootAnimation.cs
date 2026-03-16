using Units.Combat;
using UnityEngine;

namespace Units
{

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class ShootAnimation : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private IAttackable _attacking;

        private const string SHOOT = "Shoot";

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _attacking = GetComponentInParent<Attacking>();
        }

        private void Start()
        {
            _attacking.OnAttack += Attacking_OnAttack;
        }

        public void SpriteOn()
        {
            _spriteRenderer.enabled = true;
        }

        public void SpriteOff()
        {
            _spriteRenderer.enabled = false;
        }

        private void Attacking_OnAttack(object sender, System.EventArgs e)
        {
            _animator.SetTrigger(SHOOT);
        }

        private void OnDestroy()
        {
            _attacking.OnAttack -= Attacking_OnAttack;
        }
    }
}
