using Units.Warrior;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SpawnSystem
{
    public enum BoostType
    {
        Damage,
        Range,
        Speed,
        AttackSpeed,
        Defence,
        Hp,
        None
    }

    public class Boosts : MonoBehaviour
    {
        [SerializeField] private Sprite[] _boostImages;
        [SerializeField] private Image[] _boostChooseImages;
        [SerializeField] private GameObject _rightButton;
        [SerializeField] private GameObject _leftButton;
        [SerializeField] private WarriorType _warriorType;

        private int _boostNext;
        private int _boostIndex;

        private BoostType[] _boostsType;
        private BoostType[] _boostsChoose;
        private TimerToBoost _timerToBoost;

        private const int NumBoostsTypes = 6;

        public WarriorType WarriorType => _warriorType;
        public BoostType[] BoostsChoose => _boostsChoose;

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

            _boostNext = NumBoostsTypes - 1;

            OffButtons();

            foreach (var boostChooseImage in _boostChooseImages)
            {
                boostChooseImage.enabled = false;
            }
        }

        public void RightButton()
        {
            _boostsChoose[_boostIndex++] = _boostsType[_boostNext + 2];
            OffButtons();
        }

        public void LeftButton()
        {
            _boostsChoose[_boostIndex++] = _boostsType[_boostNext + 1];
            OffButtons();
        }

        private void TimerToBoost_OnBoostGet(WarriorType obj)
        {
            if (obj != _warriorType) return;

            _rightButton.SetActive(true);
            _leftButton.SetActive(true);

            _rightButton.GetComponent<Image>().sprite = _boostImages[(int)_boostsType[_boostNext--]];
            _leftButton.GetComponent<Image>().sprite = _boostImages[(int)_boostsType[_boostNext--]];
        }

        private void ShuffleBoostType()
        {
            for (int i = _boostsType.Length - 2; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (_boostsType[j], _boostsType[i]) = (_boostsType[i], _boostsType[j]);
            }
        }

        private void OffButtons()
        {
            _rightButton.SetActive(false);
            _leftButton.SetActive(false);

            if (_boostIndex == 0) return;
            _boostChooseImages[_boostIndex - 1].enabled = true;
            _boostChooseImages[_boostIndex - 1].sprite = _boostImages[(int)_boostsChoose[_boostIndex - 1]];
        }

        private void OnDestroy()
        {
            _timerToBoost.OnBoostGet -= TimerToBoost_OnBoostGet;
        }
    }
}
