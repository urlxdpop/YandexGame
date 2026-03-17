using UnityEngine;
using UnityEngine.UI;

namespace Game.SpawnSystem
{
    public class VisualBoostButtons : MonoBehaviour
    {
        [SerializeField] private Sprite[] _boostSprites;
        [SerializeField] private Image[] _boostChooseImages;
        [SerializeField] private GameObject _rightButton;
        [SerializeField] private GameObject _leftButton;
        [SerializeField] private BoostSelector _boostSelector;

        private void Start()
        {
            foreach (var boostChooseImage in _boostChooseImages)
            {
                boostChooseImage.enabled = false;
            }

            _boostSelector.OnBoostsDisplayRequested += BoostSelector_OnBoostsDisplayRequested;
            _boostSelector.OnBoostSelected += BoostSelector_OnBoostSelected;

            OffButtons(-1, BoostType.None);
        }

        private void BoostSelector_OnBoostsDisplayRequested(BoostType rightBoost, BoostType leftBoost)
        {
            _rightButton.SetActive(true);
            _leftButton.SetActive(true);

            _rightButton.GetComponent<Image>().sprite = _boostSprites[(int)rightBoost];
            _leftButton.GetComponent<Image>().sprite = _boostSprites[(int)leftBoost];
        }

        private void BoostSelector_OnBoostSelected(int index, BoostType boostType)
        {
            OffButtons(index, boostType);
        }

        private void OffButtons(int index, BoostType boostType)
        {
            _rightButton.SetActive(false);
            _leftButton.SetActive(false);

            if (index == -1) return;

            _boostChooseImages[index].enabled = true;
            _boostChooseImages[index].sprite = _boostSprites[(int)boostType];
        }

        private void OnDestroy()
        {
            if (_boostSelector != null)
            {
                _boostSelector.OnBoostsDisplayRequested -= BoostSelector_OnBoostsDisplayRequested;
                _boostSelector.OnBoostSelected -= BoostSelector_OnBoostSelected;
            }
        }
    }
}
