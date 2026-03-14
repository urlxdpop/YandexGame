using DG.Tweening;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _minSpawnY;
    [SerializeField] private float _maxSpawnY;
    [SerializeField] private float _movingSpeed;

    private bool _isMoving;

    private void Update()
    {
        if (!_isMoving)
        {
            Move();
        }
    }

    

    private void Move()
    {
        _isMoving = true;
        transform.DOMoveY(_maxSpawnY, _movingSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMoveY(_minSpawnY, _movingSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                _isMoving = false;
            });
        });
    }


}
