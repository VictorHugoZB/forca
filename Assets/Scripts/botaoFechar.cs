using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botaoFechar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()                  // Criada a função para fechar a aplicação
    {
        Debug.Log("Jogo Encerrado");    // Avisa no console que o jogo foi encerrado
        Application.Quit();
    }

}
