using UnityEngine;
using UnityEngine.SceneManagement;

// ========================================
// GERENCIA O ESTADO GERAL DO JOGO
// ========================================
// Responsável por:
// - Iniciar e encerrar a partida
// - Pausar o jogo
// - Despausar o jogo
// - Reiniciar a fase
// - Preparar o sistema para Game Over
// ========================================

public class GerenciadorJogo : MonoBehaviour
{
    // ========================================
    // ESTADO DO JOGO
    // ========================================

    public enum EstadoJogo
    {
        Jogando,
        Pausado,
        GameOver
    }


    [Header("Estado")]

    // Estado atual da partida
    public EstadoJogo estadoAtual = EstadoJogo.Jogando;


    // ========================================
    // START
    // ========================================

    void Start()
    {
        // Garante que o jogo comece rodando
        Time.timeScale = 1f;

        estadoAtual = EstadoJogo.Jogando;
    }


    // ========================================
    // UPDATE
    // ========================================

    void Update()
    {
        // ESC pausa e despausa o jogo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlternarPausa();
        }
    }


    // ========================================
    // PAUSAR / DESPAUSAR
    // ========================================

    public void AlternarPausa()
    {
        // Se estiver jogando, pausa
        if (estadoAtual == EstadoJogo.Jogando)
        {
            PausarJogo();
        }

        // Se estiver pausado, volta
        else if (estadoAtual == EstadoJogo.Pausado)
        {
            ContinuarJogo();
        }
    }


    // ========================================
    // PAUSAR
    // ========================================

    public void PausarJogo()
    {
        estadoAtual = EstadoJogo.Pausado;

        Time.timeScale = 0f;

        Debug.Log("Jogo pausado!");
    }


    // ========================================
    // CONTINUAR
    // ========================================

    public void ContinuarJogo()
    {
        estadoAtual = EstadoJogo.Jogando;

        Time.timeScale = 1f;

        Debug.Log("Jogo continuando!");
    }


    // ========================================
    // GAME OVER
    // ========================================

    public void GameOver()
    {
        estadoAtual = EstadoJogo.GameOver;

        Time.timeScale = 0f;

        Debug.Log("GAME OVER!");
    }


    // ========================================
    // REINICIAR FASE
    // ========================================

    public void ReiniciarFase()
    {
        // Volta o tempo ao normal
        Time.timeScale = 1f;

        estadoAtual = EstadoJogo.Jogando;

        // Recarrega a cena atual
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}