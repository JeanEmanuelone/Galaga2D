using UnityEngine;
using UnityEngine.SceneManagement;

// Controla os botões da tela de Game Over
public class GameOver : MonoBehaviour
{
    // Volta para a fase em que o jogador perdeu
    public void JogarNovamente()
    {
        string ultimaFase = PlayerPrefs.GetString("UltimaFase", "Fase1");

        SceneManager.LoadScene(ultimaFase);
    }

    // Fecha o jogo
    public void Sair()
    {
        Debug.Log("O jogador saiu do jogo.");

        Application.Quit();
    }
}