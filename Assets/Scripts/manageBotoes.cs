using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* classe utilizada reiniciar os parâmetros do jogo ao clicar em um botão
   de start ou restart */
public class manageBotoes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // reinicia o score do jogador para 0.
        PlayerPrefs.SetInt("score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Método para carregar a cena do jogo da forca */
    public void StartMundoGame(){
        SceneManager.LoadScene("Lab1");
    }
}
