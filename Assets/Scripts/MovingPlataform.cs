using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3f;

    private Transform _target;

    void Start()
    {
        _target = pointB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target.position) < 0.05f)
            _target = (_target == pointA) ? pointB : pointA;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.transform.SetParent(null);
    }
}