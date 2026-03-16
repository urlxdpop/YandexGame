using System;
using UnityEngine;
using UnityEngine.UI;


namespace Game.SpawnSystem
{
    public class ReloadButton : MonoBehaviour
    {
        [SerializeField] private float reloadTime = 0.5f;
        [SerializeField] private Spawner spawner;

        private float _currentReloadTime;
        private bool _isReloading;

        private Button _button;
        private RectTransform _img;

        private void Awake()
        {
            _button = GetComponent<Button>();
            foreach (Transform child in transform)
            {
                if (child.CompareTag("CoolDown"))
                {
                    _img = child.GetComponent<RectTransform>();
                    break;
                }
            }
        }

        private void Start()
        {
            spawner.OnSpawn += StartReloading;

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

        public void StartReloading(object o, EventArgs a)
        {
            if (_isReloading) return;
            _isReloading = true;
            _currentReloadTime = reloadTime;
            _button.interactable = false;
        }

        private void OnDestroy()
        {
            spawner.OnSpawn -= StartReloading;
        }
    }
}