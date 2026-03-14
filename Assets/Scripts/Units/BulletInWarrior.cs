using Units.Combat;
using UnityEngine;

namespace Units
{

    public class BulletInWarrior : MonoBehaviour
    {
        private ICenterController _centerController;
        private Attacking _attacking;

        private void Awake()
        {
            _centerController = GetComponentInParent<ICenterController>();
            _attacking = GetComponentInParent<Attacking>();
        }

        private void Start()
        {
            _attacking.OnAttack += Attacking_OnAttack;
        }

        private void Attacking_OnAttack(object sender, System.EventArgs e)
        {
            if (_centerController.EnemyWarrior != null)
            {
                transform.position = _centerController.EnemyWarrior.Pos;
            }
        }

        private void OnDestroy()
        {
            _attacking.OnAttack -= Attacking_OnAttack;
        }
    }
}
