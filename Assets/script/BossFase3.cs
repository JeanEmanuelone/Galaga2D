using UnityEngine;

// Controla o Boss da Fase 3
public class BossFase3 : MonoBehaviour
{
    [Header("Vida do Boss")]

    // Quantidade de vidas do Boss
    public int vida = 5;


    [Header("Explosão")]

    // Prefab da explosão do Boss
    public GameObject explosaoPrefab;


    // Detecta quando algo colide com o Boss
    private void OnCollisionEnter2D(Collision2D colisao)
    {
        // Verifica se o objeto que bateu é o tiro do jogador
        if (colisao.gameObject.CompareTag("Tiro"))
        {
            // Destrói o tiro
            Destroy(colisao.gameObject);

            // Retira uma vida do Boss
            ReceberDano();
        }
    }


    // Controla o dano recebido pelo Boss
    void ReceberDano()
    {
        // Diminui uma vida
        vida--;

        // Mostra no Console a vida restante
        Debug.Log(
            "Boss da Fase 3 recebeu dano! Vida: " +
            vida
        );

        // Verifica se o Boss foi derrotado
        if (vida <= 0)
        {
            Morrer();
        }
    }


    // Executado quando o Boss perde todas as vidas
    void Morrer()
    {
        // Cria a explosão no local do Boss
        if (explosaoPrefab != null)
        {
            Instantiate(
                explosaoPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        // Remove o Boss da cena
        Destroy(gameObject);
    }
}