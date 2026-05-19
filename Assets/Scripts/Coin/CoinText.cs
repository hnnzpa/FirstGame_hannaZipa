using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    private void Start() => ChangeText(); 

    private void OnEnable()  => CoinManager.OnAddPoints += ChangeText;
    private void OnDisable() => CoinManager.OnAddPoints -= ChangeText;

    private void ChangeText()
    {
        if (CoinManager.Instance == null || GameManager.Instance == null) return;
        int current = Mathf.FloorToInt(CoinManager.Instance.Amount);
        int total   = GameManager.Instance.totalCoins;
        _text.text = $"Coins: {current} / {total}";
    }
}