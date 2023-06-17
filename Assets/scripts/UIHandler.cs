using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject player;
    
    public static int score;
    [SerializeField] private TextMeshProUGUI scoreTMP;

    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverMenuUI;
    private void Start()
    {
        player.GetComponent<PlayerAimWeapon>().OnAmmoChanged += UpdateAmmoText;
    }    
    public void PauseMenu()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else { Pause(); }
    }
    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }   
    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GameIsPaused = false;
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;

        scoreTMP.text = $"¬аш счет: {score}";

        gameOverMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);        
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        EnemySpawnHandler.currentEnemies = 0;
        GameIsPaused = false;
    }
    private void UpdateAmmoText(object sender, PlayerAimWeapon.AmmoChangedEventArgs e)
    {
        text.text = $"{e.updateCurrentClip}/{e.updateMaxClipSize}|{e.updateCurrentAmmo}/{e.updateMaxAmmoSize}";
    }
}