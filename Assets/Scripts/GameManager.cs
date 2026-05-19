using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool PlayerWon = false;
    [SerializeField] public int totalCoins = 5; // posa el total de monedes del nivell

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void WinGame()
    {
        PlayerWon = true;
    }

    public void LoseGame()
    {
        PlayerWon = false;
    }
}