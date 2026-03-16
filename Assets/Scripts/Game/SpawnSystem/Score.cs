using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SpawnSystem
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private float multiplier = 0.3f;

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
            if (text) text.text = ((int)_score).ToString();

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
            _addedInSecond += _addedInSecond < 300 ? multiplier : 0;
        }
    }
}
