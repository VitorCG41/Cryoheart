using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool indoDireita;

    // --------- Vida e Defesa --------- //

    public int vidaMaxima = 3;
    public int vida = 3;

    public Slider barraDeVida;
    public GameObject telaMorte;

    // --------- Ataque --------- //

    public BoxCollider2D hitboxAtaque;
    public SpriteRenderer visualAtaque;

    public float taxaAtaque = 3f;
    public float duracaoAtaque = 0.2f;

    float nextAttackTime = 0f;

    // --------- Awake --------- //

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        barraDeVida.maxValue = vidaMaxima;
    }

    // --------- Funções para a Vida --------- //

    public void receberDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            telaMorte.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void curar(int cura)
    {
        vida += cura;
        if (vida >= vidaMaxima) { vida = vidaMaxima; }
    }

    public void SetVidaMaxima(int vidaMax) { vidaMaxima = vidaMax; }

    public void SetVida(int vida) { this.vida = vida; }

    public int GetVidaMaxima() { return vidaMaxima; }

    public int GetVida() { return vida; }

    // --------- Funções de Ataque --------- //

    public void OnAtack(InputValue value)
    {
        if (Time.time >= nextAttackTime)
        {
            StartCoroutine(Atacar());
            nextAttackTime = Time.time + 1f / taxaAtaque;
        }
    }

    IEnumerator Atacar()
    {
        hitboxAtaque.enabled = true;
        visualAtaque.enabled = true;

        yield return new WaitForSeconds(duracaoAtaque);

        hitboxAtaque.enabled = false;
        visualAtaque.enabled = false;
    }

    // --------- Update --------- //

    void Update()
    {
        barraDeVida.value = vida;
    }
}
