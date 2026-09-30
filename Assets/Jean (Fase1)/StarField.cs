using UnityEngine;

public sealed class StarField : MonoBehaviour
{
    [Header("Estrelas")]
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int starCount = 120;

    [Header("Margem da tela")]
    [SerializeField] private float margemX = 0.2f;
    [SerializeField] private float margemY = 0.2f;

    [Header("Distribuição")]
    [SerializeField] private float distanciaMinima = 0.3f;

    [Header("Brilho")]
    [SerializeField] private float brilhoMinimo = 0.65f;
    [SerializeField] private float brilhoMaximo = 1f;
    [SerializeField] private float velocidadeMinima = 1.5f;
    [SerializeField] private float velocidadeMaxima = 4.5f;

    private Camera cameraPrincipal;

    private void Start()
    {
        cameraPrincipal = Camera.main;

        if (cameraPrincipal == null)
        {
            Debug.LogError(
                "StarField: Nenhuma câmera com a tag MainCamera foi encontrada."
            );

            return;
        }

        CriarEstrelas();
    }

    private void CriarEstrelas()
    {
        if (starPrefab == null)
        {
            Debug.LogError(
                "StarField: Star Prefab não foi definido."
            );

            return;
        }

        if (!cameraPrincipal.orthographic)
        {
            Debug.LogError(
                "StarField: Este sistema foi feito para câmera Orthographic."
            );

            return;
        }

        float altura =
            cameraPrincipal.orthographicSize * 2f;

        float largura =
            altura * cameraPrincipal.aspect;

        float minX =
            cameraPrincipal.transform.position.x -
            largura / 2f +
            margemX;

        float maxX =
            cameraPrincipal.transform.position.x +
            largura / 2f -
            margemX;

        float minY =
            cameraPrincipal.transform.position.y -
            altura / 2f +
            margemY;

        float maxY =
            cameraPrincipal.transform.position.y +
            altura / 2f -
            margemY;

        // Calcula uma grade proporcional ao formato da tela.
        int colunas = Mathf.CeilToInt(
            Mathf.Sqrt(
                starCount *
                (largura / altura)
            )
        );

        int linhas = Mathf.CeilToInt(
            (float)starCount / colunas
        );

        float tamanhoCelulaX =
            (maxX - minX) / colunas;

        float tamanhoCelulaY =
            (maxY - minY) / linhas;

        int estrelasCriadas = 0;

        for (int linha = 0; linha < linhas; linha++)
        {
            for (int coluna = 0; coluna < colunas; coluna++)
            {
                if (estrelasCriadas >= starCount)
                    return;

                float centroX =
                    minX +
                    (coluna + 0.5f) *
                    tamanhoCelulaX;

                float centroY =
                    minY +
                    (linha + 0.5f) *
                    tamanhoCelulaY;

                float metadeX =
                    tamanhoCelulaX * 0.45f;

                float metadeY =
                    tamanhoCelulaY * 0.45f;

                float x = Random.Range(
                    centroX - metadeX,
                    centroX + metadeX
                );

                float y = Random.Range(
                    centroY - metadeY,
                    centroY + metadeY
                );

                Vector3 posicao =
                    new Vector3(
                        x,
                        y,
                        0f
                    );

                GameObject estrela =
                    Instantiate(
                        starPrefab,
                        posicao,
                        Quaternion.identity,
                        transform
                    );

                StarBlink piscar =
                    estrela.AddComponent<StarBlink>();

                piscar.brilhoMinimo =
                    brilhoMinimo;

                piscar.brilhoMaximo =
                    brilhoMaximo;

                piscar.velocidade =
                    Random.Range(
                        velocidadeMinima,
                        velocidadeMaxima
                    );

                piscar.offset =
                    Random.Range(
                        0f,
                        100f
                    );

                estrelasCriadas++;
            }
        }
    }

    private sealed class StarBlink : MonoBehaviour
    {
        public float brilhoMinimo;
        public float brilhoMaximo;
        public float velocidade;
        public float offset;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer =
                GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (spriteRenderer == null)
                return;

            float brilho =
                Mathf.Lerp(
                    brilhoMinimo,
                    brilhoMaximo,
                    (
                        Mathf.Sin(
                            (Time.time + offset) *
                            velocidade
                        ) + 1f
                    ) / 2f
                );

            Color cor =
                spriteRenderer.color;

            cor.a = brilho;

            spriteRenderer.color = cor;
        }
    }
}