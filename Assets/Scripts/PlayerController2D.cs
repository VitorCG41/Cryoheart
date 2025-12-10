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

    // Pulo 
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer; 

    private bool estaNoSolo;

    public float forcaPulo = 15f;
    public int maxPulosExtras = 0;
    private int pulosRestantes = 0;

    // Ataque 

    public GameObject hitboxAtaque;
    // public float alcanceAtaque = 0.5f;   
    // public LayerMask enemyLayers;       
    public float taxaAtaque = 3f;
    public float duracaoAtaque = 0.2f;

    float nextAttackTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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

    public void OnAtack(InputValue value)
    {
        if (Time.time >= nextAttackTime)
        {
            StartCoroutine(Atacar());
            nextAttackTime = Time.time + 1f / taxaAtaque;
            Debug.Log("Atacou");
        }
    }

    IEnumerator Atacar()
    {

        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
        //        hitboxAtaque.position, alcanceAtaque, enemyLayers);

        hitboxAtaque.SetActive(true);
        yield return new WaitForSeconds(duracaoAtaque);
        hitboxAtaque.SetActive(false);


        // Aplica dano
        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    enemy.GetComponent<Enemy>()?.TakeDamage(1);
        //}
    }

    void Update()
    {
        Vector2 deslocamento = moveDirection * velocidade * Time.deltaTime;
        transform.Translate(deslocamento);

        float inputX = moveDirection.x;

        if (inputX < 0) indoDireita = false;
        else if (inputX > 0) indoDireita = true;

        if (indoDireita)
        {
            hitboxAtaque.transform.localPosition = new Vector3(0.7f, 0f, 0f);
            hitboxAtaque.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            sr.flipX = false;
        }

        else if (!indoDireita)
        {
            hitboxAtaque.transform.localPosition = new Vector3(-0.7f, 0f, 0f);
            hitboxAtaque.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            sr.flipX = true;
        }

        estaNoSolo = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (estaNoSolo) pulosRestantes = maxPulosExtras; 

    }

}