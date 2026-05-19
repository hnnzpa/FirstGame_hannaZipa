using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private float horizontalRange = 2f;
    [SerializeField] private float fallGravity = 5f;

    private Rigidbody2D _rb;
    private Transform _player;
    private bool _falling = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        if (_falling) return;

        float verticalDist = transform.position.y - _player.position.y;
        float horizontalDist = Mathf.Abs(transform.position.x - _player.position.x);
        bool playerBelow = verticalDist > 0;

        if (verticalDist < detectionRange && horizontalDist < horizontalRange && playerBelow)
        {
            _falling = true;
            _rb.gravityScale = fallGravity;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!_falling) return; // si no ha caigut encara, ignora

        if (col.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage();
            Destroy(gameObject);
        }

        // Xoca amb qualsevol altra cosa (ground) → desapareix
        if (!col.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}