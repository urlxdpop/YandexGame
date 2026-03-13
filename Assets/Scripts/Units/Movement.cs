using UnityEngine;
using Units.Warrior;

namespace Units.Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private Vector2 _direction;
        private bool _isMoving;

        ICenterController _centerControler;

        private void Awake()
        {
            _centerControler = GetComponent<ICenterController>();
            if (_centerControler != null)
            {
                _centerControler.OnStateChanged += CenterControler_OnStateChanged;
            }
        }

        private void Start()
        {
            _direction = _centerControler.Side == Side.Player ? Vector2.right : Vector2.left;
        }

        private void Update()
        {
            if (_isMoving) Moving();
        }

        private void Moving()
        {
            Vector3 movement = speed * Time.deltaTime * new Vector3(_direction.x, 0, _direction.y);
            transform.Translate(movement, Space.World);
        }

        private void CenterControler_OnStateChanged(WarriorState state)
        {
            if (state == WarriorState.Walking)
            {
                _isMoving = true;
            } else
            {
                _isMoving = false;
            }
        }

        private void OnDestroy()
        {
            if (_centerControler != null)
            {
                _centerControler.OnStateChanged -= CenterControler_OnStateChanged;
            }
        }
    }
}
