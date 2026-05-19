using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack }
    public State currentState = State.Patrol;

    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1f;

    private EnemyPatrol patrol;
    private EnemyChase chase;
    private EnemyAttack attack;
    private Transform player;

    void Awake()
    {
        patrol = GetComponent<EnemyPatrol>();
        chase  = GetComponent<EnemyChase>();
        attack = GetComponent<EnemyAttack>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        float dist = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                patrol.enabled = true;
                chase.enabled  = false;
                attack.enabled = false;
                if (dist < detectionRange) TransitionTo(State.Chase);
                break;

            case State.Chase:
                patrol.enabled = false;
                chase.enabled  = true;
                attack.enabled = false;
                if (dist > detectionRange) TransitionTo(State.Patrol);
                if (dist < attackRange)   TransitionTo(State.Attack);
                break;

            case State.Attack:
                patrol.enabled = false;
                chase.enabled  = false;
                attack.enabled = true;
                if (dist > attackRange) TransitionTo(State.Chase);
                break;
        }
    }

    void TransitionTo(State newState) => currentState = newState;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            PlayerHealth.Instance?.TakeDamage();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (currentState == State.Attack && other.CompareTag("Player"))
            PlayerHealth.Instance?.TakeDamage();
    }
}