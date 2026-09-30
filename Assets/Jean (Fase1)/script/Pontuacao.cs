using UnityEngine;

// ========================================
// CONTROLA A PONTUAÇÃO DO JOGADOR
// ========================================
// Responsável por:
// - Armazenar os pontos
// - Adicionar pontos
// - Zerar a pontuação
// - Mostrar a pontuação no Console
// ========================================

public class Pontuacao : MonoBehaviour
{
    // ========================================
    // PONTOS
    // ========================================

    [Header("Pontuação")]

    // Pontuação atual
    public int pontos = 0;


    // Pontos ganhos ao destruir um inimigo
    public int pontosPorInimigo = 100;


    // ========================================
    // ADICIONAR PONTOS
    // ========================================

    public void AdicionarPontos(int quantidade)
    {
        pontos += quantidade;

        Debug.Log(
            "Pontuação: " + pontos
        );
    }


    // ========================================
    // PONTOS POR INIMIGO
    // ========================================

    public void PontosPorInimigo()
    {
        AdicionarPontos(
            pontosPorInimigo
        );
    }


    // ========================================
    // ZERAR PONTUAÇÃO
    // ========================================

    public void ZerarPontuacao()
    {
        pontos = 0;

        Debug.Log(
            "Pontuação zerada!"
        );
    }
}