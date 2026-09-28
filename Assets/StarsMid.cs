using UnityEngine;

public sealed class StarsMid : MonoBehaviour
{
    [Header("Estrelas")]
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int starCount = 45;

    [Header("Área da tela")]
    [SerializeField] private float minX = -9f;
    [SerializeField] private float maxX = 9f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;

    [Header("Tamanho")]
    [SerializeField] private float minSize = 0.02f;
    [SerializeField] private float maxSize = 0.035f;

    private void Start()
    {
        CriarEstrelas();
    }

    private void CriarEstrelas()
    {
        if (starPrefab == null)
        {
            Debug.LogError("StarsMid: Star Prefab não foi definido.");
            return;
        }

        for (int i = 0; i < starCount; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);

            GameObject estrela = Instantiate(
                starPrefab,
                transform
            );

            estrela.transform.localPosition =
                new Vector3(x, y, 0f);

            float tamanho =
                Random.Range(minSize, maxSize);

            estrela.transform.localScale =
                new Vector3(tamanho, tamanho, 1f);
        }
    }
}