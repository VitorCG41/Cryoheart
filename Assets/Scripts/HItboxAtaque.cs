using UnityEngine;

public class HitboxAttack : MonoBehaviour
{
    public int damage = 1;                    // dano do ataque
    public string enemyTag = "Inimigo";       // tag dos inimigos

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.receberDano(damage);
            }
        }
    }
}