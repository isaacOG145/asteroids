using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    private bool pausado = false;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (pausado)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        pausado = true;
    }

    public void Continuar()
    {
        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        pausado = false;
    }

    public void IrMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}