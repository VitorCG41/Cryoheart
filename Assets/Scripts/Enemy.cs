using UnityEditor.Rendering.Analytics;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int vidaMaxima = 1;
    public int vida = 1;

    public int danoDeContato = 1;

    public LayerMask layerPlayer;
    public float raioDeVerificacao = 0.65f;

    void Start() {  }

    void Update() 
    {
        Collider2D playerDetectado = Physics2D.OverlapCircle(transform.position, raioDeVerificacao, layerPlayer);
        if (playerDetectado != null) { 
            PlayerAttack player = playerDetectado.GetComponent<PlayerAttack>();
            //player.receberDano(danoDeContato);
            Debug.Log("acertou o jogador");
        }
    }

    public void receberDano(int dano)
    {
        vida -= dano;
        if (vida <= 0) { 
            Destroy(gameObject); 
        }
    }

    public void curar(int cura)
    {
        vida += cura;
        if (vida >=  vidaMaxima) { vida = vidaMaxima; }
    }

    public void SetVidaMaxima(int vidaMax) { vidaMaxima = vidaMax; }

    public void SetVida(int vida) { this.vida = vida; }

    public void SetRaioDeVerificacao(float raio) { raioDeVerificacao = raio; }

    public int GetVidaMaxima() { return vidaMaxima; }

    public int GetVida() { return vida; }

    public float GetRaioDeVerificacao() { return raioDeVerificacao; }
}
