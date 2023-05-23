using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
    private void Start()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Options()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }
    public void ExitOptions()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }
}
