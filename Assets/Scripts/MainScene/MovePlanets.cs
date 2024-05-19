using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class MovePlanets : MonoBehaviour
{
    private Vector3 startPos;

    [SerializeField]
    private int planetNumber;
        
    private void Start()
    {
        startPos = transform.position;

        StartCoroutine(Moves());
    }

    IEnumerator Moves()
    {
        bool up = planetNumber % 2 == 0;
        while (true)
        {
            
            if (up)
            {
                transform.position += new Vector3(0, 0.003f, 0);
                if ((transform.position.y - startPos.y) >= 0.25f)
                {
                    up = false;
                }
            }

            else
            {
                transform.position -= new Vector3(0, 0.003f, 0);

                if ((transform.position.y - startPos.y) <= -0.25f)
                {
                    up = true;
                }
            }

            yield return null;
        }

        
    }
}
