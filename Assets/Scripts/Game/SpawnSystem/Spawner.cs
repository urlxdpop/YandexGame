using DG.Tweening;
using System;
using Units.Warrior;
using UnityEngine;

namespace Game.SpawnSystem
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float minSpawnY;
        [SerializeField] private float maxSpawnY;
        [SerializeField] private float movingSpeed;
        [SerializeField] private Score score;
        
        private bool _isMoving;
        
        public event EventHandler OnSpawn;

        private void Update()
        {
            if (!_isMoving)
            {
                Move();
            }
        }

        public void Spawn(GameObject obj, float score, BoostType[] boostTypes)
        {
            if (this.score.ScoreValue < score)
            {
                return;
            }

            OnSpawn?.Invoke(this, EventArgs.Empty);
            this.score.GiveScore(score);
            GameObject warrior = Instantiate(obj, transform.position, Quaternion.identity);

            if (warrior.TryGetComponent(out IBoostable boost))
            {
                boost.SetBoost(boostTypes);
            }
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
