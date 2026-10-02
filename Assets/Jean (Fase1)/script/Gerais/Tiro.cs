using UnityEngine;

// ========================================
// CONTROLA O TIRO
// ========================================
// Responsável por:
// - Movimentar a bala
// - Destruir a bala depois de um tempo
// - Destruir a bala quando sair da tela
// - Detectar colisão com inimigos
// ========================================

public class Tiro : MonoBehaviour
{
    // ========================================
    // MOVIMENTO
    // ========================================

    [Header("Movimento")]

    // Velocidade da bala
    public float velocidade = 15f;


    // ========================================
    // TEMPO DE VIDA
    // ========================================

    [Header("Tempo de vida")]

    // Segurança: mesmo que a bala não saia
    // da tela, ela será destruída depois desse tempo
    public float tempoDeVida = 5f;


    // ========================================
    // LIMITE DA TELA
    // ========================================

    [Header("Limite da tela")]

    // Distância extra além da tela antes
    // de destruir a bala
    public float margemForaDaTela = 1f;


    // ========================================
    // CÂMERA
    // ========================================

    private Camera cameraPrincipal;


    // ========================================
    // START
    // ========================================

    void Start()
    {
        // Pega a câmera principal
        cameraPrincipal = Camera.main;

        // Segurança contra acúmulo de objetos
        Destroy(gameObject, tempoDeVida);
    }


    // ========================================
    // UPDATE
    // ========================================

    void Update()
    {
        MoverTiro();
        VerificarSeSaiuDaTela();
    }


    // ========================================
    // MOVIMENTO
    // ========================================

    void MoverTiro()
    {
        // Move somente para a direita
        transform.position +=
            Vector3.right *
            velocidade *
            Time.deltaTime;
    }


    // ========================================
    // VERIFICAR SE SAIU DA TELA
    // ========================================

    void VerificarSeSaiuDaTela()
    {
        // Verifica se existe câmera
        if (cameraPrincipal == null)
        {
            return;
        }


        // Converte a posição do tiro
        // para coordenadas da tela
        Vector3 posicaoNaTela =
            cameraPrincipal.WorldToViewportPoint(
                transform.position
            );


        // Verifica se saiu pela direita
        if (posicaoNaTela.x > 1f + margemForaDaTela)
        {
            DestruirTiro();
            return;
        }


        // Verifica se saiu pela esquerda
        if (posicaoNaTela.x < -margemForaDaTela)
        {
            DestruirTiro();
            return;
        }


        // Verifica se saiu por cima
        if (posicaoNaTela.y > 1f + margemForaDaTela)
        {
            DestruirTiro();
            return;
        }


        // Verifica se saiu por baixo
        if (posicaoNaTela.y < -margemForaDaTela)
        {
            DestruirTiro();
        }
    }


    // ========================================
    // COLISÃO
    // ========================================

    private void OnCollisionEnter2D(
        Collision2D colisao
    )
    {
        // Se acertar um inimigo
        if (colisao.gameObject.CompareTag("Inimigo"))
        {
            DestruirTiro();
        }
    }


    // ========================================
    // DESTRUIR TIRO
    // ========================================

    void DestruirTiro()
    {
        Destroy(gameObject);
    }
}