using System.Collections.Generic;
using UnityEngine;

public class FillProductBox : MonoBehaviour
{
    public GameObject productPrefab; // The product prefab to fill the box with
    public GameObject boxObject; // The GameObject representing the box
    public int productCount = 10; // Number of products to add

    private List<Vector3> productPositions = new List<Vector3>();
    private Vector3 boxSize;

    void Start()
    {
        if (boxObject == null)
        {
            boxObject = gameObject; // Default to this GameObject if none is assigned
        }
        boxSize = boxObject.transform.localScale; // Get the box size from the GameObject's scale
        GeneratePositions();
        FillBox();
    }

    void GeneratePositions()
    {
        productPositions.Clear();

        for (int i = 0; i < productCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(boxObject.transform.position.x - boxSize.x / 2, boxObject.transform.position.x + boxSize.x / 2),
                Random.Range(boxObject.transform.position.y - boxSize.y / 2, boxObject.transform.position.y + boxSize.y / 2),
                Random.Range(boxObject.transform.position.z - boxSize.z / 2, boxObject.transform.position.z + boxSize.z / 2)
            );
            productPositions.Add(randomPos);
        }
    }

    void FillBox()
    {
        if (productPrefab == null)
        {
            Debug.LogError("Product prefab is not assigned!");
            return;
        }

        foreach (Vector3 pos in productPositions)
        {
            Instantiate(productPrefab, pos, Quaternion.identity, boxObject.transform);
        }
    }
}