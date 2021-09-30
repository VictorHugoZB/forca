using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Classe principal, que contém toda a lógica do jogo */
public class GameManager : MonoBehaviour
{

    private int numTentativas;              // armazena as tentativas válidas da rodada
    private int maxNumTentativas;           // numero máximo de tentativas para Forca ou Salvação
    int score = 0;                          // inicializa o score do jogador com o valor 0

    public GameObject letra;                // prefab da letra no Game
    public GameObject centro;               // objeto de texto que indica o centro da tela

    char[] letrasOcultas;                   // guarda as letras da palavra oculta em um vetor
    bool[] letrasDescobertas;               // guarda booleans que indicam se a letra no índice foi descoberta

    private string palavraOculta;           // declarando a variável que conterá a palavra oculta
    private int tamanhoPalavraOculta;       // declarando a variável que guarda o tamanho da palavra oculta

    public AudioSource mistake;             // Variável que contém a fonte de áudio associada ao gameManager (mistake) 

    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("centroDaTela");   // Busca o GameObject com nome dentroDaTela
        InitGame();                                 // Chama o método InitGame, que inicializa variáveis relacionadas a palavra oculta
        InitLetras();                               // Chama o método initLetras, que carrega elementos iniciais na tela
        numTentativas = 0;                          // Inicializa o número de tentativas realizadas pelo jogador
        maxNumTentativas = 10;                      // Define o número máximo de tentativas que o jogador tem para acertar a palavra
        UpdateNumTentativas();                      // Carrega o numero de tetativas atual (0) e o máximo (10) na tela
        UpdateScore();                              // Carrega o score atual (0) na tela
    }

    // Update is called once per frame
    void Update()
    {
        // Método que checa o que o player digita no teclado e atualiza os parâmetros na tela.
        // Acontece uma vez a cada frame.
        CheckTeclado(); 
    }

    // Carrega o numero de letras (em formato de _ ) na tela.
    void InitLetras()
    {
        int numLetras = tamanhoPalavraOculta;
        for (int i=0; i<numLetras; i++) {
            Vector3 novaPosicao;
            novaPosicao = new Vector3(centro.transform.position.x + ((i-numLetras/2.0f)*80),    // instanciando um objeto Vector3, definindo a posição que cada _ aparecerá na tela, utilizando como
                                      centro.transform.position.y,                              // base o elemento que contém as coordenadas do centro da tela
                                      centro.transform.position.z);                            
                                                                                                
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);    // instanciando um objeto do tipo "letra" na posição definida acima.
            l.name = "letra" + (i + 1);                                                         // nomeia na hierarquia a GameObject com letra (iésima+1).
            l.transform.SetParent(GameObject.Find("Canvas").transform);                         // posiciona-se como filho do GameObject Canvas.
        }
    }

    /* método que inicializa variáveis relacionadas a palavra oculta definidas anteriormente */
    void InitGame()
    {
        palavraOculta = pegaUmaPalavraDoArquivo();              // recebe uma palavra do arquivo de palavras
        tamanhoPalavraOculta = palavraOculta.Length;            // determina-se o número de letras da palavra oculta
        palavraOculta = palavraOculta.ToUpper();                // transforma-se a palavra em maiscula
        letrasOcultas = new char[tamanhoPalavraOculta];         // instanciamos o array char das letras da palavra
        letrasDescobertas = new bool[tamanhoPalavraOculta];     // instanciamos o array bool do indicador de letras certas
        letrasOcultas = palavraOculta.ToCharArray();            // copia-se a palavra no array de letras
    }

    /* método chamado uma vez a cada frame, checando o teclado e atualizando parâmetros */
    void CheckTeclado()
    {
        if (Input.anyKeyDown){
            bool letraExiste = false;   // variável auxiliar para saber se o jogador acertou a letra
            char letraTeclada = Input.inputString.ToCharArray()[0];   // "capta" a letra teclada pelo jogador
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada);   // transforma a letra teclada para int de acordo com tabela ASCII

            if(letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122){ // checa se a letra é válida
                numTentativas++;    // incrementa o número de tentativas
                UpdateNumTentativas();  // atualiza o valor na tela
                if(numTentativas > maxNumTentativas){ 
                    SceneManager.LoadScene("Lab1_forca"); // checa se o jogador perdeu, e carrega a tela de forca nesse caso.
                }
                for(int i=0; i<tamanhoPalavraOculta; i++){ // percorre as letras da palavra oculta
                    if (!letrasDescobertas[i]) { // checa se a letra nesse índice ainda não foi descoberta
                        letraTeclada = System.Char.ToUpper(letraTeclada);   // transforma a letra teclada em maiúscula
                        if(letraTeclada == letrasOcultas[i]) {  // checa se o usuáro acertou a letra desse índice
                            letraExiste = true; // guarda se o usuário acertou a letra
                            letrasDescobertas[i] = true;    // atualiza o vetor de booleans indicando que a letra foi descoberta
                            GameObject.Find("letra" + (i + 1)).GetComponent<Text>().text = letraTeclada.ToString(); // atualiza o texto na tela com a letra que o usuário digitou
                            score = PlayerPrefs.GetInt("score"); // recupera o score do player guardado em PlyerPrefs
                            score++; // incrementa o score do player
                            PlayerPrefs.SetInt("score", score); // guarda novamente o score em PlayerPrefs
                            UpdateScore();  // atualiza o score na tela
                            verificaSePalavraDescoberta(); // checa se o usuário descobriu toda a palavra
                        }
                    }
                }
                if(!letraExiste){
                    mistake.Play(); // se o usuário não acertou a letra, toca o som de erro.
                }
            }
        }
    }

    /* método para encontrar o GameObject referente as tentativas e atualizar os seus valores na tela */
    void UpdateNumTentativas(){
        GameObject.Find("numTentativas").GetComponent<Text>().text = numTentativas + " | " + maxNumTentativas;
    }

    /* método para encontrar o GameObject referente ao score e atualizar seu valor na tela */
    void UpdateScore(){
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score;
    }

    /* método que checa se o usuário descobriu a palavra oculta */
    void verificaSePalavraDescoberta(){
        bool condicao = true;                                               // inicializa como true a variável que indicará se a palavra foi descoberta
        for (int i=0; i<tamanhoPalavraOculta; i++){
            condicao = condicao && letrasDescobertas[i];                    // checa se cada letra foi descoberta, tornando false a variável "condicao" se alguma não foi
        }       
        if (condicao) {                                                     // checa se a palavra foi descoberta              
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);    // guarda a palavra descoberta em PlayerPrefs para que possa ser recuperada em outra cena
            SceneManager.LoadScene("Lab1_salvo");                           // carrega a tela de vitória
        }
    }

    /* método para retornar uma palarva aleatória do arquivo de palavras da forca */
    string pegaUmaPalavraDoArquivo(){
        TextAsset t1 = (TextAsset) Resources.Load("palavras", typeof(TextAsset)); // carreha o arquivo de palavras
        string s = t1.text; // carrega o texto contido no arquivo
        string[] palavras = s.Split(' '); // separa cada palavra do texto por espaço e carrega cada uma em um array
        int palavraAleatoria = Random.Range(0, palavras.Length + 1); // sorteia um valor aleatório entre 0 e o numero de palavras
        return palavras[palavraAleatoria]; // retorna a palavra no índice do valor aleatório sorteado
    }

    
}
