using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float velocidadRotacion = 180f;
    public float fuerzaImpulso = 5f;
    public float velocidadMaxima = 8f;
    private Rigidbody2D rb;
    private bool gameOver = false;
    public float tiempoEntreDisparos = 0.6f;

private float siguienteDisparo = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (!gameOver &&
    Keyboard.current.spaceKey.isPressed &&
    Time.time >= siguienteDisparo)
{
    Disparar();

    siguienteDisparo =
        Time.time + tiempoEntreDisparos;
}

        if (gameOver &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }
    
    void FixedUpdate()
    {
        float rotacion = 0;

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            rotacion = 1;
        }

        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            rotacion = -1;
        }

        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed)
        {
            rb.AddForce(transform.up * fuerzaImpulso);
        }
        
        rb.MoveRotation(
            rb.rotation +
            rotacion *
            velocidadRotacion *
            Time.fixedDeltaTime
        );
        
        if (rb.linearVelocity.magnitude > velocidadMaxima)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * velocidadMaxima;
        }
    }
    
    void Disparar()
    {
        Instantiate(
            balaPrefab,
            puntoDisparo.position,
            puntoDisparo.rotation
        );
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Asteroid"))
    {
        GameManager gameManager =
            FindFirstObjectByType<GameManager>();

        gameManager.PerderVida();

        Destroy(collision.gameObject);

        if (gameManager.vidas <= 0)
        {
            Debug.Log("GAME OVER");

            gameOver = true;

            Time.timeScale = 0f;
        }
        else
        {
            ReiniciarJugador();
        }
    }
}

void ReiniciarJugador()
{
    transform.position = Vector3.zero;

    rb.linearVelocity = Vector2.zero;

    rb.angularVelocity = 0f;
}
}