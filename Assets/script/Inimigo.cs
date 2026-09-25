using UnityEngine;

// ========================================
// CONTROLA O INIMIGO
// ========================================
// Responsável por:
// - Movimento do inimigo
// - Vida do inimigo
// - Receber dano dos tiros
// - Colisão com o jogador
// - Explosão ao morrer
// ========================================

public class Inimigo : MonoBehaviour
{
    // ========================================
    // MOVIMENTO
    // ========================================

    [Header("Movimento")]

    // Velocidade do inimigo
    public float velocidade = 2f;

    // Limite esquerdo
    public float limiteX = 0f;

    // Limite direito
    public float limiteXDireita = 6f;

    // Limite superior
    public float limiteY = 4f;

    // Limite inferior
    public float limiteYBaixo = -4f;

    // Destino atual do inimigo
    private Vector2 destino;


    // ========================================
    // VIDA
    // ========================================

    [Header("Vida")]

    // Quantidade de vida do inimigo
    public int vida = 3;


    // ========================================
    // EXPLOSÃO
    // ========================================

    [Header("Explosão")]

    // Prefab da explosão
    public GameObject explosaoPrefab;


    // ========================================
    // START
    // ========================================

    void Start()
    {
        EscolherNovoDestino();
    }


    // ========================================
    // UPDATE
    // ========================================

    void Update()
    {
        MoverInimigo();
    }


    // ========================================
    // MOVIMENTO
    // ========================================

    void MoverInimigo()
    {
        // Move o inimigo em direção ao destino
        transform.position = Vector2.MoveTowards(
            transform.position,
            destino,
            velocidade * Time.deltaTime
        );


        // Quando chega perto do destino,
        // escolhe outro lugar
        if (Vector2.Distance(
            transform.position,
            destino
        ) < 0.1f)
        {
            EscolherNovoDestino();
        }
    }


    // ========================================
    // ESCOLHER DESTINO
    // ========================================

    void EscolherNovoDestino()
    {
        // Escolhe uma posição X aleatória
        float novoX = Random.Range(
            limiteX,
            limiteXDireita
        );


        // Escolhe uma posição Y aleatória
        float novoY = Random.Range(
            limiteYBaixo,
            limiteY
        );


        // Define o novo destino
        destino = new Vector2(
            novoX,
            novoY
        );
    }


    // ========================================
    // COLISÃO
    // ========================================

    private void OnCollisionEnter2D(
        Collision2D colisao
    )
    {
        // ----------------------------------------
        // TIRO
        // ----------------------------------------

        if (colisao.gameObject.CompareTag("Tiro"))
        {
            // O Tiro já pode cuidar da própria
            // destruição, então aqui apenas
            // aplicamos o dano.
            ReceberDano();
        }


        // ----------------------------------------
        // JOGADOR
        // ----------------------------------------

        if (colisao.gameObject.CompareTag("Player"))
        {
            Debug.Log(
                "O inimigo bateu na nave!"
            );
        }
    }


    // ========================================
    // RECEBER DANO
    // ========================================

    void ReceberDano()
    {
        // Diminui uma vida
        vida--;


        Debug.Log(
            "Inimigo recebeu dano! Vida: " +
            vida
        );


        // Verifica se morreu
        if (vida <= 0)
        {
            Morrer();
        }
    }


    // ========================================
    // MORRER
    // ========================================

    void Morrer()
    {
        // ----------------------------------------
        // CRIAR EXPLOSÃO
        // ----------------------------------------

        if (explosaoPrefab != null)
        {
            Instantiate(
                explosaoPrefab,
                transform.position,
                Quaternion.identity
            );
        }


        // ----------------------------------------
        // DESTRUIR INIMIGO
        // ----------------------------------------

        Destroy(gameObject);
    }
}