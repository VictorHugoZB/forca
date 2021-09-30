using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostraUltimaPalavra : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // carrega a palavra salva em PlayerPrefs no script GameManager e mostra na tela colocando em um componente de texto
        GetComponent<Text>().text = "A palavra era: " + PlayerPrefs.GetString("ultimaPalavraOculta");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
