using UnityEngine;

// ========================================
// CONTROLA O NASCIMENTO DOS INIMIGOS
// ========================================
// Responsável por:
// - Criar inimigos automaticamente
// - Definir o intervalo entre inimigos
// - Definir a posição onde eles aparecem
// ========================================

public class SpawnerInimigos : MonoBehaviour
{
    // ========================================
    // PREFAB DO INIMIGO
    // ========================================

    [Header("Inimigo")]

    // Prefab que será criado
    public GameObject inimigoPrefab;


    // ========================================
    // CONFIGURAÇÃO DO SPAWN
    // ========================================

    [Header("Spawn")]

    // Tempo entre cada inimigo
    public float intervalo = 2f;

    // Posição X onde os inimigos aparecem
    public float posicaoX = 5.5f;

    // Limite superior da posição Y
    public float limiteY = 4f;

    // Limite inferior da posição Y
    public float limiteYBaixo = -4f;


    // ========================================
    // CONTROLE INTERNO
    // ========================================

    private float tempoParaProximoInimigo;


    // ========================================
    // START
    // ========================================

    void Start()
    {
        // Permite que o primeiro inimigo
        // apareça depois do intervalo definido
        tempoParaProximoInimigo = intervalo;
    }


    // ========================================
    // UPDATE
    // ========================================

    void Update()
    {
        ControlarSpawn();
    }


    // ========================================
    // CONTROLAR SPAWN
    // ========================================

    void ControlarSpawn()
    {
        // Se não existe prefab,
        // não tenta criar inimigos
        if (inimigoPrefab == null)
        {
            return;
        }


        // Diminui o tempo
        tempoParaProximoInimigo -= Time.deltaTime;


        // Verifica se chegou a hora
        if (tempoParaProximoInimigo <= 0f)
        {
            CriarInimigo();

            // Reinicia o contador
            tempoParaProximoInimigo = intervalo;
        }
    }


    // ========================================
    // CRIAR INIMIGO
    // ========================================

    void CriarInimigo()
    {
        // Escolhe uma posição Y aleatória
        float posicaoY = Random.Range(
            limiteYBaixo,
            limiteY
        );


        // Define a posição do novo inimigo
        Vector3 posicao = new Vector3(
            posicaoX,
            posicaoY,
            0f
        );


        // Cria o inimigo
        Instantiate(
            inimigoPrefab,
            posicao,
            Quaternion.identity
        );
    }
}