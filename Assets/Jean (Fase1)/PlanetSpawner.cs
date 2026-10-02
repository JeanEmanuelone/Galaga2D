using UnityEngine;

public sealed class PlanetSpawner : MonoBehaviour
{
    [Header("Planetas")]
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private int planetCount = 3;

    [Header("Área da cena")]
    [SerializeField] private float minX = -7.5f;
    [SerializeField] private float maxX = 7.5f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    [Header("Tamanhos")]
    [SerializeField] private float tamanhoGrande = 0.22f;
    [SerializeField] private float tamanhoMedio = 0.10f;
    [SerializeField] private float tamanhoPequeno = 0.06f;

    [Header("Distância mínima")]
    [SerializeField] private float minDistance = 2f;

    [Header("Renderização")]
    [SerializeField] private string sortingLayerName = "Background";
    [SerializeField] private int sortingOrder = 0;

    [Header("Rotação")]
    [SerializeField] private float velocidadeRotacao = 20f;

    private void Start()
    {
        CriarPlanetas();
    }

    private void CriarPlanetas()
    {
        if (planetPrefab == null)
        {
            Debug.LogError(
                "PlanetSpawner: Planet Prefab não foi definido."
            );

            return;
        }

        int sortingLayerID =
            SortingLayer.NameToID(sortingLayerName);

        if (sortingLayerID == 0 &&
            sortingLayerName != "Default")
        {
            Debug.LogError(
                "PlanetSpawner: A Sorting Layer '" +
                sortingLayerName +
                "' não existe. Crie essa Sorting Layer no projeto."
            );

            return;
        }

        Vector3[] posicoes =
            new Vector3[planetCount];

        for (int i = 0; i < planetCount; i++)
        {
            Vector3 novaPosicao;
            int tentativas = 0;

            do
            {
                float x =
                    Random.Range(minX, maxX);

                float y =
                    Random.Range(minY, maxY);

                novaPosicao =
                    new Vector3(
                        x,
                        y,
                        0f
                    );

                tentativas++;

            } while (
                !PosicaoValida(
                    novaPosicao,
                    posicoes,
                    i
                )
                &&
                tentativas < 100
            );

            posicoes[i] =
                novaPosicao;

            GameObject planeta =
                Instantiate(
                    planetPrefab,
                    novaPosicao,
                    Quaternion.identity
                );

            float tamanho;

            if (i == 0)
                tamanho = tamanhoGrande;
            else if (i == 1)
                tamanho = tamanhoMedio;
            else
                tamanho = tamanhoPequeno;

            planeta.transform.localScale =
                new Vector3(
                    tamanho,
                    tamanho,
                    1f
                );

            SpriteRenderer[] renderers =
                planeta.GetComponentsInChildren<SpriteRenderer>(
                    true
                );

            foreach (
                SpriteRenderer spriteRenderer
                in renderers
            )
            {
                spriteRenderer.sortingLayerID =
                    sortingLayerID;

                spriteRenderer.sortingOrder =
                    sortingOrder;
            }

            planeta
                .AddComponent<RotacaoPlaneta>()
                .velocidade =
                    velocidadeRotacao;
        }
    }

    private bool PosicaoValida(
        Vector3 novaPosicao,
        Vector3[] posicoes,
        int quantidadeCriada
    )
    {
        for (int i = 0; i < quantidadeCriada; i++)
        {
            if (
                Vector3.Distance(
                    novaPosicao,
                    posicoes[i]
                ) < minDistance
            )
            {
                return false;
            }
        }

        return true;
    }

    private sealed class RotacaoPlaneta : MonoBehaviour
    {
        public float velocidade;

        private void Update()
        {
            transform.Rotate(
                0f,
                0f,
                velocidade *
                Time.deltaTime
            );
        }
    }
}