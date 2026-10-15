using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int puntuacion = 0;
    public int vidas = 3;
    public TMP_Text textoPuntuacion;
    public TMP_Text textoVidas;
    public GameObject gameOverPanel;
    public TMP_Text textoPuntuacionFinal;
    
    void Awake()
    {
        Time.timeScale = 1f;
    }
    void Start()
    {
        ActualizarUI();
    }
    
    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos;

        ActualizarUI();
    }
    
    public void PerderVida()
    {
        vidas--;

        ActualizarUI();
    }
    
    void ActualizarUI()
    {
        textoPuntuacion.text =
            "PUNTOS: " + puntuacion;

        textoVidas.text =
            "VIDAS: " + vidas;
    }
    
    public void GameOver()
    {
        gameOverPanel.SetActive(true);

        textoPuntuacionFinal.text =
            "PUNTUACIÓN: " + puntuacion;

        Time.timeScale = 0f;
    }
    
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Game");
    }
    
    public void IrMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
    
    
}