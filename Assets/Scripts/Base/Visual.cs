using UnityEngine;

namespace Units.Base
{

    [RequireComponent(typeof(Animator))]
    public class Visual : MonoBehaviour
    {
        private Animator _animator;
        private Hp _hp;

        private const string HP = "HP";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _hp = GetComponentInParent<Hp>();
        }

        private void Start()
        {
            _hp.OnHpChangedInPercent += Hp_OnHpChangedInPercent;
        }

        private void Hp_OnHpChangedInPercent(float hpPercent)
        {
            _animator.SetFloat(HP, hpPercent);
        }
    }
}
