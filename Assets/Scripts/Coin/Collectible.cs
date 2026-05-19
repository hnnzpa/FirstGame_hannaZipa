using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float value = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.EnsureExists();
            CoinManager.AddAmount(value);
            Destroy(gameObject);
        }
    }
}