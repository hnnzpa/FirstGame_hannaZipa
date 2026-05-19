using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CoinManager : MonoBehaviour
{
    private static CoinManager _instance;
    public static CoinManager Instance => _instance;

    public static Action OnAddPoints;
    public static Action OnAllCoinsCollected;

    [SerializeField] private float _amount;
    public float Amount => _instance._amount;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ← NUEVO: se autocrea si no hay GameObject en la escena
    public static void EnsureExists()
    {
        if (_instance == null)
        {
            var go = new GameObject("CoinManager");
            go.AddComponent<CoinManager>();
        }
    }

    public static void AddAmount(float amount)
    {
        if (_instance == null) { Debug.LogWarning("CoinManager no existe"); return; }

        _instance._amount += amount;
        OnAddPoints?.Invoke();

        if (GameManager.Instance != null &&
            _instance._amount >= GameManager.Instance.totalCoins)
        {
            OnAllCoinsCollected?.Invoke();
        }
    }

    public static void ResetAmount()
    {
        if (_instance == null) return;
        _instance._amount = 0;
        OnAddPoints?.Invoke();
    }
}