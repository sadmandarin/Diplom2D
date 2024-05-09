using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private GameObject puzzlePrefab;

    private bool active = false;

    private GameObject game;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!active) 
            {
                game = Instantiate(prefab);

                active = true;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (active)
            {
                Destroy(game);

                active = false;

                game = Instantiate (puzzlePrefab);

                active = true;
            }
        }

        if (Input.GetKey(KeyCode.P)) 
        {
            if(!active)
            {
                game = Instantiate(puzzlePrefab);

                active = true;
            }
        }

        if (Input.GetKey(KeyCode.Escape)) 
        {
            Destroy(game);

            active = false;
        }
    }
}
