using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    // Movimento 

    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public float velocidade = 5f;

    private bool indoDireita;
    public BoxCollider2D hitboxAtaque;
    public SpriteRenderer visualAtaque;

    // Animação Movimento

    private Animator animator;

    // Pulo 

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer; 

    private bool estaNoSolo;

    public float forcaPulo = 15f;
    public int maxPulosExtras = 0;
    private int pulosRestantes = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
        moveDirection.Set(moveDirection.x, 0);
    }

    public void OnJump(InputValue value)
    { 

        if (estaNoSolo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
            return;
        }

        if (pulosRestantes > 0)
        {
            pulosRestantes--;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        }

    }

    private void FixedUpdate()
    {
        Vector3 deslocamento = moveDirection * velocidade * Time.deltaTime;
        transform.position = transform.position + deslocamento;

        Vector3 posicao = transform.position;
        posicao.x = Mathf.Round(posicao.x * 64f) / 64f;
        transform.position = posicao;   

        if (estaNoSolo)
        {
            pulosRestantes = maxPulosExtras;
        }
    }

    void Update()
    {
        float inputX = moveDirection.x;

        if (inputX < 0) indoDireita = false;
        else if (inputX > 0) indoDireita = true;

        if (indoDireita)
        {
            hitboxAtaque.transform.localPosition = new Vector3(0.7f, 0f, 0f);
            hitboxAtaque.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            sr.flipX = true;
        }

        else if (!indoDireita)
        {
            hitboxAtaque.transform.localPosition = new Vector3(-0.7f, 0f, 0f);
            hitboxAtaque.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            sr.flipX = false;
        }

        estaNoSolo = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Animação de Andar

        if (inputX != 0 && estaNoSolo) { animator.SetBool("estaAndando", true); }
        else { animator.SetBool("estaAndando", false); }

    }

}