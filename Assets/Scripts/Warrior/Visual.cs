using DG.Tweening;
using UnityEngine;

namespace Units.Warrior
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Visual : MonoBehaviour
    {
        [SerializeField] private GameObject gun;
        [SerializeField] private GameObject hemlet;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private ICenterController _centerController;

        private float _lastDamage = 10;
        private float _impulse;

        private const string WALK = "Walk";
        private const string ATTACK = "Attack";
        private const string DEATH = "Death";

        private void Start()
        {
            // Component initialization
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _centerController = GetComponentInParent<ICenterController>();

            // Subscribe to events
            _centerController.OnStateChanged += CenterController_OnStateChanged;

            // Misc
            _impulse = _centerController.Side == Side.Enemy ? 1 : -1;
            _impulse = Random.Range(_impulse * 0.5f, _impulse * 2f);
        }

        private void CenterController_OnStateChanged(WarriorState state)
        {
            switch (state)
            {
                case WarriorState.Walking:
                    _animator.SetTrigger(WALK);
                    break;
                case WarriorState.Attacking:
                    _animator.SetTrigger(ATTACK);
                    break;
                case WarriorState.Dead:
                    Death();
                    _animator.SetTrigger(DEATH);
                    break;
            }
        }

        private void Death()
        {
            _lastDamage = Mathf.Max(10, _centerController.LastDamage);

            gun.GetComponent<Animator>().enabled = false;
            Rigidbody2D rb = gun.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(new Vector2(5 * _impulse, Mathf.Abs(2 * _impulse)) * _lastDamage);
            rb.AddTorque(_impulse * _lastDamage * 20);

            rb = hemlet.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(new Vector2(_impulse, Mathf.Abs(5 * _impulse)) * _lastDamage);
            rb.AddTorque(_impulse * _lastDamage * 10);

            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(new Vector2(5 * _impulse, 0) * _lastDamage);

            _spriteRenderer.DOFade(0, 4f).OnComplete(() => _centerController.DestroyYourself());
        }

        private void OnDestroy()
        {
            _centerController.OnStateChanged -= CenterController_OnStateChanged;
        }
    }
}
