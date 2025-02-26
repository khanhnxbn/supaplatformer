using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    [SerializeField] private GameObject InventoryUI;

    private bool isGameOver = false;
    private bool isGameWin = false;
    void Start()
    {
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
        InventoryUI.SetActive(false);
    }

  
   

    public void AddScore(int points)
    {
        if (isGameOver ==false && !isGameWin) 
        {
            score += points;
            UpdateScore();
        }
        
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void GameOver()
    { 
        isGameOver = true;
        score = 0;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }

    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }
    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        Time.timeScale = 1;
        UpdateScore();
        SceneManager.LoadScene("Game");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
