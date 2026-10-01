using UnityEngine;

// ========================================
// CONTROLA AS NAVES MENORES DA FASE 3
// ========================================
// Responsável por:
// - Movimento das naves
// - Vida das naves
// - Tiros das naves
// - Colisão com o tiro do jogador
// - Explosão ao morrer
// ========================================

public class InimigoFase3 : MonoBehaviour
{
    // ========================================
    // MOVIMENTO
    // ========================================

    [Header("Movimento")]

    // Velocidade da nave
    public float velocidade = 2f;

    // Limite mínimo do movimento horizontal
    public float limiteX = 0f;

    // Limite máximo do movimento horizontal
    public float limiteXDireita = 6f;

    // Limite máximo do movimento vertical
    public float limiteY = 4f;

    // Limite mínimo do movimento vertical
    public float limiteYBaixo = -4f;

    // Próximo destino da nave
    private Vector2 destino;


    // ========================================
    // VIDA
    // ========================================

    [Header("Vida")]

    // Quantidade de vida da nave
    public int vida = 1;


    // ========================================
    // TIRO
    // ========================================

    [Header("Tiro")]

    // Prefab do tiro inimigo
    public GameObject tiroPrefab;

    // Tempo entre os tiros
    public float intervaloDeTiro = 2f;

    // Velocidade do tiro
    public float velocidadeDoTiro = 8f;

    // Tempo para o próximo tiro
    private float tempoParaAtirar;


    // ========================================
    // EXPLOSÃO
    // ========================================

    [Header("Explosão")]

    // Prefab da explosão
    public GameObject explosaoPrefab;


    // ========================================
    // INÍCIO
    // ========================================

    void Start()
    {
        // Escolhe o primeiro destino
        EscolherNovoDestino();

        // Espera 1 segundo antes do primeiro tiro
        tempoParaAtirar = 1f;
    }


    // ========================================
    // ATUALIZAÇÃO
    // ========================================

    void Update()
    {
        // Movimenta a nave
        Mover();

        // Controla os tiros
        Atirar();
    }


    // ========================================
    // MOVIMENTO
    // ========================================

    void Mover()
    {
        // Move a nave até o destino
        transform.position = Vector2.MoveTowards(
            transform.position,
            destino,
            velocidade * Time.deltaTime
        );

        // Quando chegar ao destino,
        // escolhe outro
        if (Vector2.Distance(
            transform.position,
            destino
        ) < 0.1f)
        {
            EscolherNovoDestino();
        }
    }


    // ========================================
    // ESCOLHER NOVO DESTINO
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

        // Guarda o novo destino
        destino = new Vector2(
            novoX,
            novoY
        );
    }


    // ========================================
    // TIRO
    // ========================================

    void Atirar()
    {
        // Diminui o contador
        tempoParaAtirar -= Time.deltaTime;

        // Ainda não chegou a hora de atirar
        if (tempoParaAtirar > 0f)
        {
            return;
        }

        // Verifica se existe um prefab de tiro
        if (tiroPrefab == null)
        {
            Debug.LogWarning(
                "A nave inimiga está sem Tiro Prefab!"
            );

            tempoParaAtirar = intervaloDeTiro;

            return;
        }


        // ========================================
        // POSIÇÃO DO TIRO
        // ========================================

        // Começa na posição da nave
        Vector3 posicaoDoTiro =
            transform.position;

        // Coloca o tiro no lado esquerdo
        // da nave
        posicaoDoTiro.x -= 0.8f;


        // ========================================
        // CRIA O TIRO
        // ========================================

        GameObject tiro = Instantiate(
            tiroPrefab,
            posicaoDoTiro,
            Quaternion.identity
        );


        // Pega o Rigidbody2D do tiro
        Rigidbody2D rb =
            tiro.GetComponent<Rigidbody2D>();


        // ========================================
        // DIREÇÃO
        // ========================================

        if (rb != null)
        {
            // Força o tiro a ir para a esquerda
            rb.linearVelocity =
                Vector2.left * velocidadeDoTiro;
        }


        // ========================================
        // PRÓXIMO TIRO
        // ========================================

        // Reinicia o contador
        tempoParaAtirar = intervaloDeTiro;
    }


    // ========================================
    // COLISÃO
    // ========================================

    private void OnCollisionEnter2D(
        Collision2D colisao
    )
    {
        // Verifica se foi atingido pelo tiro do jogador
        if (colisao.gameObject.CompareTag("Tiro"))
        {
            // Destrói o tiro
            Destroy(colisao.gameObject);

            // Recebe dano
            ReceberDano();
        }
    }


    // ========================================
    // RECEBER DANO
    // ========================================

    void ReceberDano()
    {
        // Diminui uma vida
        vida--;

        // Mostra no Console
        Debug.Log(
            gameObject.name +
            " recebeu dano! Vida: " +
            vida
        );

        // Verifica se morreu
        if (vida <= 0)
        {
            Morrer();
        }
    }


    // ========================================
    // MORTE
    // ========================================

    void Morrer()
    {
        // Cria a explosão
        if (explosaoPrefab != null)
        {
            Instantiate(
                explosaoPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        // Destrói a nave
        Destroy(gameObject);
    }
}