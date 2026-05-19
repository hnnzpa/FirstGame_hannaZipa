using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    private SpriteRenderer _sr;
    private Collider2D _col;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
        _sr.sprite = closedSprite;
        _col.enabled = false;
    }

    void OnEnable()  => CoinManager.OnAllCoinsCollected += OpenChest;
    void OnDisable() => CoinManager.OnAllCoinsCollected -= OpenChest;

    private void OpenChest()
    {
        _sr.sprite = openSprite;
        _col.enabled = true;  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.WinGame();
    }
}