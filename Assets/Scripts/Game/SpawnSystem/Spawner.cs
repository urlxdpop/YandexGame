using DG.Tweening;
using UnityEngine;

namespace Game.Spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float minSpawnY;
        [SerializeField] private float maxSpawnY;
        [SerializeField] private float movingSpeed;
        [SerializeField] private Score score;
        
        private bool _isMoving;

        private void Update()
        {
            if (!_isMoving)
            {
                Move();
            }
        }

        public void Spawn(GameObject obj, float score)
        {
            if (this.score.ScoreValue < score)
            {
                return;
            }
            this.score.GiveScore(score);
            Instantiate(obj, transform.position, Quaternion.identity);
        }

        private void Move()
        {
            _isMoving = true;
            transform.DOMoveY(maxSpawnY, movingSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOMoveY(minSpawnY, movingSpeed).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _isMoving = false;
                });
            });
        }
    }
}
