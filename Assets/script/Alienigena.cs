using UnityEngine;

public class Alienigena : MonoBehaviour
{
    [Header("Alvo")]
    public Transform nave;

    [Header("Movimento")]
    public float velocidade = 2f;
    public float distanciaMinima = 5f;

    [Header("Ataque")]
    public GameObject fogoPrefab;
    public Transform pontoSaidaFogo;
    public float intervaloAtaque = 2f;
    public float velocidadeFogo = 8f;

    private float proximoAtaque;

    void Update()
    {
        if (nave == null)
            return;

        Mover();
        Atacar();
    }

    void Mover()
    {
        // Faz o alienígena se aproximar da nave
        float distancia = Vector2.Distance(transform.position, nave.position);

        if (distancia > distanciaMinima)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                nave.position,
                velocidade * Time.deltaTime
            );
        }
    }

    void Atacar()
    {
        if (Time.time < proximoAtaque)
            return;

        proximoAtaque = Time.time + intervaloAtaque;

        // Cria o fogo na posição da boca
        GameObject fogo = Instantiate(
            fogoPrefab,
            pontoSaidaFogo.position,
            pontoSaidaFogo.rotation
        );

        // Faz o fogo ir em direção à nave
        Rigidbody2D rb = fogo.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direcao = (nave.position - pontoSaidaFogo.position).normalized;

            rb.linearVelocity = direcao * velocidadeFogo;
        }
    }
}