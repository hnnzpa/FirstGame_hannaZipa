using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolDistance = 3f;

    private Vector2 startPos;
    private int direction = 1;

    void Awake() => startPos = transform.position;

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        float dist = Vector2.Distance(transform.position, startPos);
        if (dist >= patrolDistance) direction *= -1;
    }
}