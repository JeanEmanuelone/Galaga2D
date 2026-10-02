using UnityEngine;

public sealed class StarsNear : MonoBehaviour
{
    [Header("Estrelas")]
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int starCount = 30;

    [Header("Área da tela")]
    [SerializeField] private float minX = -9f;
    [SerializeField] private float maxX = 9f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;

    [Header("Tamanho")]
    [SerializeField] private float minSize = 0.03f;
    [SerializeField] private float maxSize = 0.05f;

    [Header("Brilho")]
    [SerializeField] private float brilhoMinimo = 0.55f;
    [SerializeField] private float brilhoMaximo = 1f;
    [SerializeField] private float velocidadeMinima = 1.5f;
    [SerializeField] private float velocidadeMaxima = 4f;

    [Header("Efeito Cintilante")]
    [SerializeField] private float chanceDeBrilho = 0.7f;
    [SerializeField] private float intensidadeBrilho = 1.8f;
    [SerializeField] private float tamanhoBrilho = 1.7f;
    [SerializeField] private float velocidadeBrilho = 7f;

    [Header("Movimento")]
    [SerializeField] private float velocidadeMinimaMovimento = 0.3f;
    [SerializeField] private float velocidadeMaximaMovimento = 0.8f;

    [Header("Multiplicador de Velocidade")]
    [SerializeField] private float multiplicadorVelocidade = 1f;

    private void Start()
    {
        CriarEstrelas();
    }

    private void CriarEstrelas()
    {
        if (starPrefab == null)
        {
            Debug.LogError("StarsNear: Star Prefab não foi definido.");
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

            StarBlink piscar =
                estrela.AddComponent<StarBlink>();

            piscar.brilhoMinimo = brilhoMinimo;
            piscar.brilhoMaximo = brilhoMaximo;
            piscar.velocidade = Random.Range(
                velocidadeMinima,
                velocidadeMaxima
            );

            piscar.offset =
                Random.Range(0f, 100f);

            piscar.chanceDeBrilho =
                chanceDeBrilho;

            piscar.intensidadeBrilho =
                intensidadeBrilho;

            piscar.tamanhoBrilho =
                tamanhoBrilho;

            piscar.velocidadeBrilho =
                velocidadeBrilho;

            piscar.velocidadeMovimento =
                Random.Range(
                    velocidadeMinimaMovimento,
                    velocidadeMaximaMovimento
                );

            piscar.multiplicadorVelocidade =
                multiplicadorVelocidade;

            piscar.limiteEsquerdo = minX;
            piscar.limiteDireito = maxX;
        }
    }

    private sealed class StarBlink : MonoBehaviour
    {
        public float brilhoMinimo;
        public float brilhoMaximo;
        public float velocidade;
        public float offset;

        public float chanceDeBrilho;
        public float intensidadeBrilho;
        public float tamanhoBrilho;
        public float velocidadeBrilho;

        public float velocidadeMovimento;
        public float multiplicadorVelocidade;

        public float limiteEsquerdo;
        public float limiteDireito;

        private SpriteRenderer spriteRenderer;
        private Vector3 tamanhoOriginal;

        private float brilhoExtra;
        private float tempoBrilho;
        private bool brilhando;

        private void Awake()
        {
            spriteRenderer =
                GetComponent<SpriteRenderer>();

            tamanhoOriginal =
                transform.localScale;
        }

        private void Update()
        {
            if (spriteRenderer == null)
                return;

            // Movimento da direita para a esquerda
            transform.localPosition +=
                Vector3.left *
                velocidadeMovimento *
                multiplicadorVelocidade *
                Time.deltaTime;

            // Reaparece pela direita
            if (transform.localPosition.x < limiteEsquerdo)
            {
                Vector3 posicao =
                    transform.localPosition;

                posicao.x =
                    limiteDireito;

                transform.localPosition =
                    posicao;
            }

            // Brilho normal
            float brilhoBase =
                Mathf.Lerp(
                    brilhoMinimo,
                    brilhoMaximo,
                    (Mathf.Sin(
                        (Time.time + offset) *
                        velocidade
                    ) + 1f) / 2f
                );

            // Chance de brilho rápido
            if (!brilhando &&
                Random.value <
                chanceDeBrilho * Time.deltaTime)
            {
                brilhando = true;
                tempoBrilho = 0f;
            }

            if (brilhando)
            {
                tempoBrilho +=
                    Time.deltaTime *
                    velocidadeBrilho;

                float efeito =
                    Mathf.Sin(
                        tempoBrilho *
                        Mathf.PI
                    );

                brilhoExtra =
                    efeito *
                    intensidadeBrilho;

                float escala =
                    1f +
                    efeito *
                    (tamanhoBrilho - 1f);

                transform.localScale =
                    tamanhoOriginal *
                    escala;

                if (tempoBrilho >= 1f)
                {
                    brilhando = false;
                    brilhoExtra = 0f;

                    transform.localScale =
                        tamanhoOriginal;
                }
            }
            else
            {
                transform.localScale =
                    tamanhoOriginal;
            }

            float brilhoFinal =
                Mathf.Clamp01(
                    brilhoBase +
                    brilhoExtra
                );

            Color cor =
                spriteRenderer.color;

            cor.a = brilhoFinal;

            spriteRenderer.color =
                cor;
        }
    }
}