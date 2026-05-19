using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    private float _timer;

    void OnEnable() => _timer = attackCooldown; // ← ataca inmediatamente al entrar en rango

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= attackCooldown)
        {
            _timer = 0f;
            PlayerHealth.Instance?.TakeDamage();
        }
    }
}