using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Game.UI
{
    public class Final : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _youImage;
        [SerializeField] private GameObject _winImage;
        [SerializeField] private GameObject _loseImage;

        private CheckHp _checkHp;
        private PauseMenu _pause;

        private void Awake()
        {
            _checkHp = GetComponentInParent<CheckHp>();
            _pause = GetComponentInParent<PauseMenu>();

        }

        private void Start()
        {
            _youImage.SetActive(false);
            _winImage.SetActive(false);
            _loseImage.SetActive(false);

            _checkHp.OnPlayerWin += CheckHp_OnPlayerWin;
            _checkHp.OnEnemyWin += CheckHp_OnEnemyWin;
        }

        private void CheckHp_OnEnemyWin(object sender, System.EventArgs e)
        {
            EnemyWin();
        }

        private void CheckHp_OnPlayerWin(object sender, System.EventArgs e)
        {
            PlayerWin();
        }

        private void EnemyWin()
        {
            _panel.SetActive(true);
            _pause.Final();
            _youImage.SetActive(true);
            _loseImage.SetActive(true);

            _youImage.transform.DOMoveY(0, 0.5f).SetEase(Ease.Linear);
            _loseImage.transform.DOMoveY(0, 0.5f).SetEase(Ease.Linear);

            StartCoroutine(BackToMenu());
        }

        private void PlayerWin()
        {
            _panel.SetActive(true);
            _pause.Final();
            _youImage.SetActive(true);
            _winImage.SetActive(true);

            _youImage.transform.DOMoveY(0, 0.5f).SetEase(Ease.Linear);
            _winImage.transform.DOMoveY(0, 0.5f).SetEase(Ease.Linear);

            StartCoroutine(BackToMenu());
        }

        private IEnumerator BackToMenu()
        {
            yield return new WaitForSeconds(3f);
            _pause.LoadMenu();
        }

        private void OnDestroy()
        {
            _checkHp.OnPlayerWin -= CheckHp_OnPlayerWin;
            _checkHp.OnEnemyWin -= CheckHp_OnEnemyWin;
        }
    }
}
