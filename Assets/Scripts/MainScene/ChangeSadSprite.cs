using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSadSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite sad;

    [SerializeField]
    private Sprite happy;

    [SerializeField]
    private GameData gameData;

    private void Update()
    {
        if (gameData.Games[2] == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = happy;

            gameObject.transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
        }

        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sad;
        }
    }
}
