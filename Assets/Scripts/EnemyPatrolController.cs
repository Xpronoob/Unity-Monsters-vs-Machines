using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : MonoBehaviour
{
    public float velocidad = 5f;
    public float velocidadInversa = -5f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private bool enElSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enElSuelo = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);

        // Mover automáticamente en el eje X
        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        // Evitar caerse si no está en el suelo
        if (!enElSuelo)
        {
            rb.velocity = new Vector2(velocidadInversa, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            
            
        }
    }
}
