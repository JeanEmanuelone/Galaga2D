// ============================================================
// SCRIPT: MenuInicial
// JOGO: Galaga 2D
// FUNÇÃO: Controlar os botões da tela inicial
// ============================================================

// Importa as funções básicas do Unity
using UnityEngine;

// Importa as funções necessárias para trocar de cena
using UnityEngine.SceneManagement;


// ============================================================
// CLASSE DO MENU INICIAL
// ============================================================

public class MenuInicial : MonoBehaviour
{
    // ========================================================
    // FUNÇÃO: JOGAR
    // ========================================================
    // Essa função será chamada quando o jogador clicar
    // no botão "JOGAR".
    //
    // Ela vai carregar a cena onde o jogo começa.
    // ========================================================

    public void Jogar()
    {
        // Aqui estamos mandando o Unity abrir uma cena
        // chamada "Jogo".
        //
        // IMPORTANTE:
        // O nome "Jogo" precisa ser exatamente o nome
        // da sua cena do jogo.

        SceneManager.LoadScene("Jogo");
    }


    // ========================================================
    // FUNÇÃO: SAIR
    // ========================================================
    // Essa função será chamada quando o jogador clicar
    // no botão "SAIR".
    //
    // Ela fecha o jogo quando ele estiver executando
    // como um programa (.exe).
    // ========================================================

    public void Sair()
    {
        // Mostra uma mensagem no Console do Unity.
        // Isso é útil para saber se o botão foi pressionado.

        Debug.Log("O jogador clicou em SAIR.");


        // Fecha o jogo.
        //
        // IMPORTANTE:
        // Application.Quit() funciona quando o jogo está
        // executando como um programa.
        //
        // No Unity Editor, apertar SAIR não vai fechar
        // o Unity.

        Application.Quit();
    }
}