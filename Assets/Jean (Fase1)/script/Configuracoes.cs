using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// ========================================
// CONTROLA AS CONFIGURAÇÕES DO JOGO
// ========================================
// Responsável por:
// - Volume geral
// - Volume da música
// - Volume dos efeitos sonoros
// - Voltar ao menu
// ========================================

public class Configuracoes : MonoBehaviour
{
    // ========================================
    // AUDIO
    // ========================================

    [Header("Audio")]

    // AudioMixer usado para controlar os volumes
    public AudioMixer audioMixer;


    // ========================================
    // VOLUME GERAL
    // ========================================

    public void AlterarVolumeGeral(float volume)
    {
        if (audioMixer == null)
        {
            Debug.LogWarning(
                "AudioMixer não foi configurado!"
            );

            return;
        }

        // Converte o valor de 0-1 para decibéis
        float volumeDB = Mathf.Log10(
            Mathf.Clamp(volume, 0.0001f, 1f)
        ) * 20f;

        audioMixer.SetFloat(
            "VolumeGeral",
            volumeDB
        );
    }


    // ========================================
    // VOLUME DA MÚSICA
    // ========================================

    public void AlterarVolumeMusica(float volume)
    {
        if (audioMixer == null)
        {
            Debug.LogWarning(
                "AudioMixer não foi configurado!"
            );

            return;
        }

        float volumeDB = Mathf.Log10(
            Mathf.Clamp(volume, 0.0001f, 1f)
        ) * 20f;

        audioMixer.SetFloat(
            "Musica",
            volumeDB
        );
    }


    // ========================================
    // VOLUME DOS EFEITOS
    // ========================================

    public void AlterarVolumeEfeitos(float volume)
    {
        if (audioMixer == null)
        {
            Debug.LogWarning(
                "AudioMixer não foi configurado!"
            );

            return;
        }

        float volumeDB = Mathf.Log10(
            Mathf.Clamp(volume, 0.0001f, 1f)
        ) * 20f;

        audioMixer.SetFloat(
            "Efeitos",
            volumeDB
        );
    }


    // ========================================
    // VOLTAR AO MENU
    // ========================================

    public void Voltar()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("menu");
    }
}