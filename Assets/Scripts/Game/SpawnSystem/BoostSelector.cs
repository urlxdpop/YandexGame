using Units.Warrior;
using UnityEngine;

namespace Game.SpawnSystem
{
    public class BoostSelector : MonoBehaviour, IBoostSelectable
    {
        [SerializeField] private WarriorType _warriorType;

        private int _boostNext;
        private int _boostIndex;

        private BoostType[] _boostsType;
        private BoostType[] _boostsChoose;
        private TimerToBoost _timerToBoost;

        private const int NumBoostsTypes = 6;

        public WarriorType WarriorType => _warriorType;
        public BoostType[] BoostsChoose => _boostsChoose;

        public event System.Action<BoostType, BoostType> OnBoostsDisplayRequested;
        public event System.Action<int, BoostType> OnBoostSelected;
        public event System.Action<IBoostSelectable> OnReadyChoose;

        private void Start()
        {
            _boostsChoose = new BoostType[NumBoostsTypes / 2];
            for (int i = 0; i < _boostsChoose.Length; i++)
            {
                _boostsChoose[i] = BoostType.None;
            }

            _boostsType = (BoostType[])System.Enum.GetValues(typeof(BoostType));
            ShuffleBoostType();
            _timerToBoost = GetComponentInParent<TimerToBoost>();

            _timerToBoost.OnBoostGet += TimerToBoost_OnBoostGet;

            _boostNext = _boostsType.Length - 2;
        }

        public void RightButton()
        {
            SelectBoost(_boostsType[_boostNext]);
            _boostNext-=2;
        }

        public void LeftButton()
        {
            SelectBoost(_boostsType[_boostNext-1]);
            _boostNext-=2;
        }

        private void SelectBoost(BoostType boostType)
        {
            _boostsChoose[_boostIndex] = boostType;
            OnBoostSelected?.Invoke(_boostIndex, boostType);
            _boostIndex++;
        }

        private void TimerToBoost_OnBoostGet(WarriorType obj)
        {
            if (obj != _warriorType) return;

            if (_boostNext < 1) return;

            BoostType rightBoost = _boostsType[_boostNext];
            BoostType leftBoost = _boostsType[_boostNext - 1];

            OnBoostsDisplayRequested?.Invoke(rightBoost, leftBoost);
            OnReadyChoose?.Invoke(this);
        }

        private void ShuffleBoostType()
        {
            for (int i = _boostsType.Length - 2; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (_boostsType[j], _boostsType[i]) = (_boostsType[i], _boostsType[j]);
            }
        }

        private void OnDestroy()
        {
            if (_timerToBoost != null)
            {
                _timerToBoost.OnBoostGet -= TimerToBoost_OnBoostGet;
            }
        }
    }
}