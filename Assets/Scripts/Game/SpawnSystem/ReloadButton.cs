using UnityEngine;
using UnityEngine.UI;


namespace Game.Spawner
{
    public class ReloadButton : MonoBehaviour
    {
        [SerializeField] private float reloadTime = 1;

        private float _currentReloadTime;
        private bool _isReloading;

        private Button _button;
        private RectTransform _img;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _img = transform.GetChild(0).GetComponent<RectTransform>();
        }

        private void Start()
        {
            _button.onClick.AddListener(StartReloading);
            _img.localScale = new(1, 0, 0);
        }

        private void Update()
        {
            if (_isReloading)
            {
                _currentReloadTime -= Time.deltaTime;

                _img.localScale = new(1, _currentReloadTime, 0);

                if (_currentReloadTime <= 0)
                {
                    _isReloading = false;
                    _button.interactable = true;
                }
            }
        }

        public void StartReloading()
        {
            if (_isReloading) return;
            _isReloading = true;
            _currentReloadTime = reloadTime;
            _button.interactable = false;
        }
    }
}