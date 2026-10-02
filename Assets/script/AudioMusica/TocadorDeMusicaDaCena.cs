using UnityEngine;

public class TocadorDeMusicaDaCena : MonoBehaviour
{
    public enum TipoDeMusica { Menu, FaseNormal, Boss, GameOver, Vitoria }
    public TipoDeMusica musicaDestaCena;

    private void Start()
    {
        if (GerenciadorDeAudio.Instancia == null) return;

        switch (musicaDestaCena)
        {
            case TipoDeMusica.Menu:
                GerenciadorDeAudio.Instancia.TocarMusicaTelaInicial();
                break;
            case TipoDeMusica.FaseNormal:
                GerenciadorDeAudio.Instancia.TocarMusicaFase();
                break;
            case TipoDeMusica.Boss:
                GerenciadorDeAudio.Instancia.TocarMusicaBoss();
                break;
            case TipoDeMusica.GameOver:
                GerenciadorDeAudio.Instancia.TocarMusicaGameOver();
                break;
            case TipoDeMusica.Vitoria:
                GerenciadorDeAudio.Instancia.TocarMusicaVitoria();
                break;
        }
    }
}