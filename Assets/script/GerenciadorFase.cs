using UnityEngine;
using UnityEngine.SceneManagement;

// ========================================
// GERENCIA A PROGRESSÃO DAS FASES
// ========================================
// Responsável por:
// - Controlar a fase atual
// - Avançar para a próxima fase
// - Voltar ao menu
// - Recarregar a fase atual
// ========================================

public class GerenciadorFase : MonoBehaviour
{
    // ========================================
    // CONFIGURAÇÃO
    // ========================================

    [Header("Configuração da Fase")]

    // Número da fase atual
    public int numeroDaFase = 1;

    // Nome da próxima fase
    public string proximaFase = "";


    // ========================================
    // AVANÇAR DE FASE
    // ========================================

    public void ProximaFase()
    {
        // Verifica se foi informado um nome
        // para a próxima fase
        if (string.IsNullOrEmpty(proximaFase))
        {
            Debug.LogWarning(
                "A próxima fase não foi configurada!"
            );

            return;
        }

        // Volta o tempo ao normal
        Time.timeScale = 1f;

        // Carrega a próxima fase
        SceneManager.LoadScene(proximaFase);
    }


    // ========================================
    // RECARREGAR FASE
    // ========================================

    public void RecarregarFase()
    {
        // Volta o tempo ao normal
        Time.timeScale = 1f;

        // Pega o nome da cena atual
        string cenaAtual =
            SceneManager.GetActiveScene().name;

        // Recarrega a cena
        SceneManager.LoadScene(cenaAtual);
    }


    // ========================================
    // VOLTAR AO MENU
    // ========================================

    public void VoltarAoMenu()
    {
        // Volta o tempo ao normal
        Time.timeScale = 1f;

        // Carrega o menu
        SceneManager.LoadScene("menu");
    }
}