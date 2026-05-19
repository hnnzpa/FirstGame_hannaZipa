using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private Image[] hearts; 

    void OnEnable()  => PlayerHealth.OnLivesChanged += UpdateHearts;
    void OnDisable() => PlayerHealth.OnLivesChanged -= UpdateHearts;

    void Start() => UpdateHearts(PlayerHealth.Instance != null ? PlayerHealth.Instance.CurrentLives : hearts.Length);

    private void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null)
            {
                Debug.LogWarning($"[LivesUI] hearts[{i}] no asignado en el Inspector");
                continue;
            }
            // Con 3 vidas: 0,1,2 visibles. Con 2: 0,1. Con 1: solo 0. Con 0: ninguno.
            hearts[i].enabled = i < currentLives;
        }
    }
}