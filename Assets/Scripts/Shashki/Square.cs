using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    GameObject[,] squareArray;

    private void OnEnable()
    {
        CreateField();
    }

    void CreateField()
    {
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Vector2 position = new Vector2(i - 2, j);

                Instantiate(prefab, position, Quaternion.identity, gameObject.transform);
            }
        }
    }
}
