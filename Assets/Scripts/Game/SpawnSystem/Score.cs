using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Spawner
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Text text;

        private float _score;
        private float _addedInSecond;
        private bool waited;

        public float ScoreValue => _score;

        private void Start()
        {
            _score = 0;
            _addedInSecond = 5;
        }

        private void Update()
        {
            text.text = ((int)_score).ToString();

            if (!waited) StartCoroutine(AddScore());
        }

        public void GiveScore(float score)
        {
            _score -= score;
        }

        private IEnumerator AddScore()
        {
            waited = true;

            yield return new WaitForSeconds(1);

            waited = false;
            _score += _addedInSecond;
            _addedInSecond += _addedInSecond < 300 ? 0.5f : 0;
        }
    }
}
