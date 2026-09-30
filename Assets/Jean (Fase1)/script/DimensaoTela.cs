using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAspectRatio : MonoBehaviour
{
    // A proporção alvo para a qual a sua arte/jogo foi projetado (ex: 16/9 = 1.777)
    public float targetAspectWidth = 16f;
    public float targetAspectHeight = 9f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateCameraViewport();
    }

    void UpdateCameraViewport()
    {
        // Proporção desejada (ex: 16:9 = 1.7777...)
        float targetAspect = targetAspectWidth / targetAspectHeight;

        // Proporção atual da janela/tela do jogador
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Fator de escala comparando a tela atual com a ideal
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Adiciona barras horizontais (Letterbox - acima e abaixo)
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else
        {
            // Adiciona barras verticais (Pillarbox - lados esquerdo e direito)
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}