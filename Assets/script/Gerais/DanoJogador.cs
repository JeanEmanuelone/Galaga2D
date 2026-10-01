using UnityEngine;

// ========================================
// CONTROLA O DANO RECEBIDO PELO JOGADOR
// ========================================
// Responsável por:
// - Detectar colisão com inimigos
// - Diminuir as vidas
// - Verificar Game Over
// ========================================

public class DanoJogador : MonoBehaviour
{
    // ========================================
    // VIDA
    // ========================================

    [Header("Vida")]

    // Quantidade de vidas do jogador
    public int vidas = 3;


    // ========================================
    // COLISÃO
    // ========================================

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        // Verifica se o jogador bateu em um inimigo
        if (colisao.gameObject.CompareTag("Inimigo"))
        {
            ReceberDano();
        }
    }


    // ========================================
    // RECEBER DANO
    // ========================================

    void ReceberDano()
    {
        // Diminui uma vida
        vidas--;

        Debug.Log(
            "O jogador recebeu dano! " +
            "Vidas restantes: " +
            vidas
        );


        // Verifica se acabaram as vidas
        if (vidas <= 0)
        {
            GameOver();
        }
    }


    // ========================================
    // GAME OVER
    // ========================================

    void GameOver()
    {
        Debug.Log("GAME OVER!");

        // Desativa a nave
        gameObject.SetActive(false);
    }
}