using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoState<MachineController>
{
    [Header("Move")]
    [SerializeField]
    float moveSpeed = 2.0f;

    [SerializeField]
    bool isFacingRight = true;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [Header("Animation")]
    [SerializeField]
    Animator animator;

    Rigidbody2D _rb;

    Vector2 _direction;

    bool _isFlipping = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMove();
        HandleFlipX();
    }

    void HandleMove()
    {
        _direction = isFacingRight ? Vector2.right : Vector2.left;
        Vector2 velocity = _direction * moveSpeed;

        // Reduce la velocidad si no est� en el suelo
        if (!IsGrounded())
        {
            velocity *= 0.5f; // Puedes ajustar este valor seg�n tus necesidades.
        }

        // Aplica la velocidad horizontal al objeto
        _rb.velocity = new Vector2(velocity.x, _rb.velocity.y);

        // Verifica si no est� en el suelo y no se ha realizado un giro reciente
        if (!IsGrounded() && !_isFlipping)
        {
            // Cambia la direcci�n sin cambiar la posici�n
            isFacingRight = !isFacingRight;
            _isFlipping = true;
        }
    }


    void HandleFlipX()
    {
        if (_isFlipping)
        {
            // Realiza la rotaci�n para cambiar la direcci�n
            transform.Rotate(0.0f, 180.0f, 0.0f);

            // Ajusta la posici�n en el eje X para mantener al objeto en la plataforma
            Vector3 newPosition = transform.position;
            float capsuleWidth = 0.5f; // El mismo valor que se usa en IsGrounded
            newPosition.x += isFacingRight ? capsuleWidth : -capsuleWidth;
            transform.position = newPosition;

            _isFlipping = false;
        }
    }

    bool IsGrounded()
    {
        // Verifica si el objeto est� en contacto con el suelo
        Vector2 groundCheckPosition = new Vector2(groundCheck.position.x, groundCheck.position.y);
        float capsuleWidth = 0.5f; // Ancho de la c�psula de colisi�n
        float capsuleHeight = 1f;  // Alto de la c�psula de colisi�n
        return Physics2D.OverlapCapsule(groundCheckPosition, new Vector2(capsuleWidth, capsuleHeight), CapsuleDirection2D.Horizontal, 0.0f, groundMask);
    }

}
