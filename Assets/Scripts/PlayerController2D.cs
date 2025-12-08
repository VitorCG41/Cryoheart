//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    public float speed = 5f;          // Velocidade de movimento
//    public float jumpForce = 10f;     // Força do pulo

//    private Rigidbody2D rb;
//    private bool isGrounded = false;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        // Movimento horizontal
//        float move = Input.GetAxis("Horizontal");
//        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

//        // Pulo
//        if (Input.GetButtonDown("Jump") && isGrounded)
//        {
//            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
//        }
//    }

//    // Detectar se está no ch�o
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = true;
//        }
//    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = false;
//        }
//    }
//}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;

    public float velocidade = 5f;
    public float forcaPulo = 15f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer; 

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
        moveDirection.Set(moveDirection.x, 0);
    }

    public void OnJump(InputValue value)
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        }
    }

    void Update()
    {
        Vector2 deslocamento = moveDirection * velocidade * Time.deltaTime;
        transform.Translate(deslocamento);
    }
}