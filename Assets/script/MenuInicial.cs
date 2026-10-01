using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDeMenus : MonoBehaviour
{
    public enum TipoDeTela { Padrão, GameOver, Vitoria }

    [Header("Configuração desta Cena")]
    [Tooltip("Defina se este objeto está numa cena de Game Over ou Vitória")]
    public TipoDeTela tipoDeTelaAtual = TipoDeTela.Padrão;

    [Header("Configurações de Nome das Cenas")]
    [Tooltip("Nome exato da cena da primeira fase")]
    public string nomePrimeiraFase = "Fase1";

    [Tooltip("Nome exato da cena do Menu Inicial")]
    public string nomeMenuInicial = "MenuInicial";

    [Header("Painéis de UI (Opcional - Para Pause)")]
    public GameObject painelPause;

    // Variáveis estáticas persistem entre a mudança de cenas
    private static string nomeCenaAnterior = "";
    private static string ultimaFaseJogada = "";

    private void Awake()
    {
        Time.timeScale = 1f; // Restaura a velocidade normal do jogo

        string cenaAtual = SceneManager.GetActiveScene().name;

        // Se estiver jogando uma fase normal (não for menu, game over ou vitória), guarda a fase atual
        if (tipoDeTelaAtual == TipoDeTela.Padrão && cenaAtual != nomeMenuInicial)
        {
            ultimaFaseJogada = cenaAtual;
        }
    }

    // --- BOTAO JOGAR NOVAMENTE (COMPARTILHADO) ---
    public void JogarNovamente()
    {
        Time.timeScale = 1f;

        // Se for a tela de VITÓRIA -> Recarrega a primeira fase do jogo
        if (tipoDeTelaAtual == TipoDeTela.Vitoria)
        {
            CarregarCenaComHistorico(nomePrimeiraFase);
        }
        // Se for a tela de GAME OVER -> Recarrega a fase onde o jogador perdeu
        else if (tipoDeTelaAtual == TipoDeTela.GameOver)
        {
            if (!string.IsNullOrEmpty(ultimaFaseJogada))
            {
                SceneManager.LoadScene(ultimaFaseJogada);
            }
            else
            {
                // Caso de segurança se não houver registro
                CarregarCenaComHistorico(nomePrimeiraFase);
            }
        }
        // Caso padrão (reinicia a cena atual)
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // --- BOTAO JOGAR (DO MENU INICIAL) ---
    public void Jogar()
    {
        CarregarCenaComHistorico(nomePrimeiraFase);
    }

    // --- BOTAO RETORNAR À CENA ANTERIOR ---
    public void RetornarParaCenaAnterior()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(nomeCenaAnterior))
        {
            string cenaAtual = SceneManager.GetActiveScene().name;
            string destino = nomeCenaAnterior;

            nomeCenaAnterior = cenaAtual;
            SceneManager.LoadScene(destino);
        }
        else
        {
            CarregarCenaComHistorico(nomeMenuInicial);
        }
    }

    // --- BOTAO MENU INICIAL ---
    public void IrParaMenuInicial()
    {
        CarregarCenaComHistorico(nomeMenuInicial);
    }

    // --- BOTAO RETORNAR AO JOGO (PAUSE) ---
    public void RetornarAoJogo()
    {
        if (painelPause != null)
        {
            painelPause.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    // --- BOTAO PAUSAR ---
    public void PausarJogo()
    {
        if (painelPause != null)
        {
            painelPause.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    // --- BOTAO SAIR ---
    public void Sair()
    {
        Debug.Log("O jogador clicou em SAIR.");
        Application.Quit();
    }

    // Método auxiliar para registrar o histórico ao mudar de cena
    private void CarregarCenaComHistorico(string nomeDaProximaCena)
    {
        Time.timeScale = 1f;
        nomeCenaAnterior = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomeDaProximaCena);
    }
}