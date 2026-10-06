using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int puntuacion = 0;
    public int vidas = 3;

    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos;

        Debug.Log("Puntuación: " + puntuacion);
    }

    public void PerderVida()
    {
        vidas--;

        Debug.Log("Vidas restantes: " + vidas);
    }
}