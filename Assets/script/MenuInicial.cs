using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void Sair()
    {
        Debug.Log("O jogador clicou em SAIR.");
        Application.Quit();
    }
}