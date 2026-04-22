using UnityEngine;
using UnityEngine.SceneManagement;

public static class Level
{
    public static void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
}