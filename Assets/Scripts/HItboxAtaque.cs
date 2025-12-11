using UnityEngine;

public class HitboxAttack : MonoBehaviour
{
    public int dano = 1;                   
    public string enemyTag = "Inimigo";       

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.receberDano(dano);
            }
        }
    }
}