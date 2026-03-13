using UnityEngine;

public class Hp : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float armor = 1f;

    private float currentHp;

    ICenterController _centerController;

    private void Awake()
    {
        currentHp = maxHp;
        _centerController = GetComponentInParent<ICenterController>();
        _centerController.OnDamageTaken += TakeDamage;
    }

    private void TakeDamage(float damage)
    {
        float effectiveDamage = damage / armor;
        currentHp -= effectiveDamage;
        if (currentHp <= 0)
        {
            currentHp = 0;
            _centerController.Death();
        }
    }

    private void OnDestroy()
    {
        if (_centerController != null)
        {
            _centerController.OnDamageTaken -= TakeDamage;
        }
    }
}
