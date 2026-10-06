using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    public float limiteX = 9f;
    public float limiteY = 5f;

    void Update()
    {
        Vector3 posicion =
            transform.position;

        if (posicion.x > limiteX)
        {
            posicion.x = -limiteX;
        }
        else if (posicion.x < -limiteX)
        {
            posicion.x = limiteX;
        }

        if (posicion.y > limiteY)
        {
            posicion.y = -limiteY;
        }
        else if (posicion.y < -limiteY)
        {
            posicion.y = limiteY;
        }

        transform.position = posicion;
    }
}