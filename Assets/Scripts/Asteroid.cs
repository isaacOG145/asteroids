using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float velocidadMinima = 1f;
    public float velocidadMaxima = 3f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 direccion =
            ((Vector2)Vector2.zero -
            (Vector2)transform.position).normalized;

        direccion +=
            Random.insideUnitCircle * 0.3f;

        direccion.Normalize();

        float velocidad =
            Random.Range(
                velocidadMinima,
                velocidadMaxima
            );

        rb.linearVelocity =
            direccion * velocidad;

        rb.angularVelocity =
            Random.Range(-100f, 100f);
    }
}