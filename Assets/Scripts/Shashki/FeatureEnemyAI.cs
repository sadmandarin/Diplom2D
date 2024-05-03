using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FeatureEnemyAI : MonoBehaviour
{
    public GameObject feature;

    public Square square;

    public bool loaded;

    public bool walk;

    public int x;
    public int y;

    private void Update()
    {
        if (walk)
        {
            StartCoroutine(Moving());
        }
        
    }

    IEnumerator Moving()
    {
        GameObject objectMove = gameObject;

        while (Vector3.Distance(objectMove.transform.position, gameObject.transform.position) > 0.01f)
        {
            objectMove.transform.position = Vector3.MoveTowards(objectMove.transform.position, transform.position, 0.1f);

            yield return new WaitForSeconds(0.001f);
        }
    }


}
