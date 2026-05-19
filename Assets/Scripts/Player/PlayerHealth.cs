using System;
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    public static Action<int> OnLivesChanged;
    public static Action OnPlayerDied;

    [SerializeField] private int maxLives = 3;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float invincibilityDuration = 1.5f;

    private int _currentLives;
    private bool _isDead;
    private bool _isInvincible;

    public int CurrentLives => _currentLives;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _currentLives = maxLives;
        OnLivesChanged?.Invoke(_currentLives);
    }

    public void TakeDamage()
    {
        if (_isDead || _isInvincible) return;

        _currentLives--;
        OnLivesChanged?.Invoke(_currentLives);

        if (_currentLives <= 0)
            Die();
        else
            Respawn();
    }

    public void Die()
    {
        _isDead = true;
        OnPlayerDied?.Invoke();
        CoinManager.ResetAmount();
        GameManager.Instance.LoseGame();
        gameObject.SetActive(false); 
    }

    private void Respawn()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("[PlayerHealth] spawnPoint no asignado en el Inspector");
            return;
        }
        transform.position = spawnPoint.position;
        StartCoroutine(InvincibilityFrames());
    }

    private IEnumerator InvincibilityFrames()
    {
        _isInvincible = true;
        var sr = GetComponent<SpriteRenderer>();
        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            if (sr != null) sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.15f);
            elapsed += 0.15f;
        }
        if (sr != null) sr.enabled = true;
        _isInvincible = false;
    }
}