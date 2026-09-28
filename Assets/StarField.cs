using UnityEngine;

public sealed class StarField : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int starCount = 60;

    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;

    private void Start()
    {
        for (int i = 0; i < starCount; i++)
        {
            CreateStar();
        }
    }

    private void CreateStar()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        Vector3 position = new Vector3(x, y, 0f);

        Instantiate(
            starPrefab,
            position,
            Quaternion.identity,
            transform
        );
    }
}