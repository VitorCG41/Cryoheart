using System;
using System.Collections;
using UnityEngine;

public class Ambiente1 : MonoBehaviour
{
    public GameObject inimigoPrefab;

    void Start()
    {
        
    }

    bool jaGerou = false;
    void Update()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
        
        if (inimigos.Length == 0 && !jaGerou)
        {
            StartCoroutine(gerarInimigo());
            jaGerou = true;
        }
    }

    IEnumerator gerarInimigo()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(inimigoPrefab, new Vector3(3.5f, 0, 0), Quaternion.identity);
        jaGerou = false;
    }
}
