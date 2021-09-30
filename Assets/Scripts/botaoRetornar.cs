using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botaoRetornar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    /* Código associado ao botão na tela de créditos para retornar ao
    menu inicial. */
    public void ReturnToStart(){
        SceneManager.LoadScene("Lab1_start");
    }
}
