using Game.SpawnSystem;
using UnityEngine;

namespace Units.Warrior
{
    public class Boost : MonoBehaviour, IBoostable
    {
        [SerializeField] private float boostAttack;
        [SerializeField] private float boostRange;
        [SerializeField] private float boostSpeed;
        [SerializeField] private float boostAttackSpeed;
        [SerializeField] private float boostDefence;
        [SerializeField] private float boostHp;
        [SerializeField] private BoxCollider2D boostCollider;
        
        private IAttackable _attackable;
        private IMovable _movable;
        private IHpable _hp;

        private void Awake()
        {
            _attackable = GetComponent<IAttackable>();
            _movable = GetComponent<IMovable>();
            _hp = GetComponent<IHpable>();
        }

        public void SetBoost(BoostType[] boost)
        {
            foreach (var item in boost)
            {
                ActivatedBoost(item);
            }
        }

        private void ActivatedBoost(BoostType boost)
        {
            switch (boost)
            {
                case BoostType.Damage:
                    _attackable.BoostAttack(boostAttack);
                    break;
                case BoostType.Range:
                    BoostRange();
                    break;
                case BoostType.Speed:
                    _movable.BoostSpeed(boostSpeed);
                    break;
                case BoostType.AttackSpeed:
                    _attackable.BoostAttackSpeed(boostAttackSpeed);
                    break;
                case BoostType.Defence:
                    _hp.BoostDefence(boostDefence);
                    break;
                case BoostType.Hp:
                    _hp.BoostHp(boostHp);
                    break;
            }
        }

        private void BoostRange()
        {
            boostCollider.size += new Vector2(boostRange, boostRange);
        }
    } 
}
