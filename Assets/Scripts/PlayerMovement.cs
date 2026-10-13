using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public AudioClip sonidoDisparo;
    private AudioSource audioSource;
    public AudioClip sonidoMuerte;
    public AudioClip sonidoReinicio;
    public ParticleSystem propulsor;
    public GameObject explosionPrefab;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private bool invulnerable = false;
    public float tiempoInvulnerable = 3f;
    public float velocidadParpadeo = 0.15f;
    
    IEnumerator Parpadear()
    {
        float tiempo = 0f;

        while (tiempo < tiempoInvulnerable)
        {
            spriteRenderer.enabled =
                !spriteRenderer.enabled;

            yield return new WaitForSeconds(
                velocidadParpadeo
            );

            tiempo += velocidadParpadeo;
        }

        spriteRenderer.enabled = true;
    }
    
    IEnumerator Reaparecer()
    {
        invulnerable = true;

        spriteRenderer.enabled = false;
        playerCollider.enabled = false;

        propulsor.Stop();

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        yield return new WaitForSeconds(1f);

        spriteRenderer.enabled = true;

        yield return StartCoroutine(Parpadear());

        playerCollider.enabled = true;

        invulnerable = false;
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
    }

    
    void Update()
    {
        if (gameOver)
        {
            Debug.Log("GAME OVER");
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                Time.timeScale = 1f;

                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().buildIndex
                );
            }

            return;
        }

        if (Keyboard.current.spaceKey.isPressed &&
            Time.time >= siguienteDisparo)
        {
            Disparar();

            siguienteDisparo =
                Time.time + tiempoEntreDisparos;
        }
    }

    void FixedUpdate()
    {
        if (gameOver)
        {
            return;
        }

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

            if (!propulsor.isPlaying)
            {
                propulsor.Play();
            }
        }
        else
        {
            if (propulsor.isPlaying)
            {
                propulsor.Stop();
            }
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

        audioSource.PlayOneShot(sonidoDisparo);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") &&
            !invulnerable)
        {
            GameManager gameManager =
                FindFirstObjectByType<GameManager>();

            gameManager.PerderVida();

            audioSource.PlayOneShot(sonidoMuerte);

            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );

            Destroy(collision.gameObject);

            if (gameManager.vidas <= 0)
            {
                Debug.Log("GAME OVER");

                gameOver = true;

                spriteRenderer.enabled = false;
                playerCollider.enabled = false;

                propulsor.Stop();
            }
            else
            {
                StartCoroutine(Reaparecer());
            }
        }
    }

}