using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botaoCreditos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Código associado ao botão na tela de vitória e de derrota para
       ver os créditos */
    public void StartCreditos(){
        SceneManager.LoadScene("Lab1_creditos");
    }
}
