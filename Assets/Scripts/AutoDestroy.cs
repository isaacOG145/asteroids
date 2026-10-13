using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float tiempo = 2f;

    void Start()
    {
        Destroy(gameObject, tiempo);
    }
}