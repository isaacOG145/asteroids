using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 12f;
    public float tiempoVida = 2f;
    

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity =
            transform.up * velocidad;

        Destroy(
            gameObject,
            tiempoVida
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Asteroid"))
    {
        GameManager gameManager =
            FindFirstObjectByType<GameManager>();

        gameManager.SumarPuntos(100);

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}

    
}