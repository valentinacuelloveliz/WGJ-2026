using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class CanvasManager : MonoBehaviour
{

    // FUNCIONES DEL MENU

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public GameObject panelCredits;

    public GameObject panelSettings;

    public void OpenCreditsPanel()
    {
        panelCredits.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        panelCredits.SetActive(false);
    }

    public void OpenSettingsPanel()
    {
        panelSettings.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        panelSettings.SetActive(false);
    }

    //FUNCIONES DEL JUEGO

    public GameObject panelPause;

    public void OpenPausePanel()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePausePanel()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
