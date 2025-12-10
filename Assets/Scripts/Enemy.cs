using UnityEditor.Rendering.Analytics;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int vida = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void receberDano(int dano)
    {
        vida -= dano;
        if (vida <= 0) { 
            Destroy(gameObject); 
        }
    }
}
