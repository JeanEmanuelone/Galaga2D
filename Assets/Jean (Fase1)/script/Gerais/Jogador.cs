using UnityEngine;
using UnityEngine.InputSystem;

// ========================================
// CONTROLA A NAVE DO JOGADOR
// ========================================

public class Jogador : MonoBehaviour
{
    // ========================================
    // MOVIMENTO
    // ========================================

    [Header("Movimento")]

    public float velocidade = 7f;

    public float limiteY = 4f;

    public float limiteYBaixo = -4f;


    // ========================================
    // TIRO
    // ========================================

    [Header("Tiro")]

    // PREFAB DA BALA
    // Arraste o Tiro azul da pasta Assets aqui
    public GameObject tiroPrefab;

    // PONTO ONDE A BALA NASCE
    // Arraste o PontoDeTiro da Hierarchy aqui
    public Transform pontoDeTiro;

    // Tempo entre os tiros
    public float intervaloEntreTiros = 0.25f;

    // Controle do intervalo
    private float tempoUltimoTiro;


    // ========================================
    // VIDA
    // ========================================

    [Header("Vida")]

    public int vidas = 3;


    // ========================================
    // START
    // ========================================

    void Start()
    {
        Application.targetFrameRate = 60;

        tempoUltimoTiro = -intervaloEntreTiros;
    }


    // ========================================
    // UPDATE
    // ========================================

    void Update()
    {
        MoverJogador();
        Atirar();
    }


    // ========================================
    // MOVIMENTO
    // ========================================

    void MoverJogador()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        float movimentoY = 0f;


        // SUBIR
        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed)
        {
            movimentoY = 1f;
        }


        // DESCER
        if (Keyboard.current.sKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed)
        {
            movimentoY = -1f;
        }


        // MOVE A NAVE
        transform.position +=
            Vector3.up *
            movimentoY *
            velocidade *
            Time.deltaTime;


        // LIMITA A NAVE
        float novaPosicaoY = Mathf.Clamp(
            transform.position.y,
            limiteYBaixo,
            limiteY
        );


        transform.position = new Vector3(
            transform.position.x,
            novaPosicaoY,
            transform.position.z
        );
    }


    // ========================================
    // ATIRAR
    // ========================================

    void Atirar()
    {
        if (Keyboard.current == null)
        {
            return;
        }


        // APERTAR ESPAÇO
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Disparar();
        }
    }


    // ========================================
    // DISPARAR
    // ========================================

    void Disparar()
    {
        // Verifica o intervalo
        if (Time.time <
            tempoUltimoTiro + intervaloEntreTiros)
        {
            return;
        }


        // Verifica o PREFAB
        if (tiroPrefab == null)
        {
            Debug.LogWarning(
                "Tiro Prefab não foi configurado na nave!"
            );

            return;
        }


        // Verifica o PONTO DE TIRO
        if (pontoDeTiro == null)
        {
            Debug.LogWarning(
                "Ponto De Tiro não foi configurado na nave!"
            );

            return;
        }


        // ========================================
        // CRIA A BALA
        // ========================================

        GameObject novoTiro = Instantiate(
            tiroPrefab,
            pontoDeTiro.position,
            pontoDeTiro.rotation
        );


        // ========================================
        // REGISTRA O MOMENTO DO TIRO
        // ========================================

        tempoUltimoTiro = Time.time;
    }
}