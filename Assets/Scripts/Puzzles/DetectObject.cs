using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    [SerializeField]
    private bool isFilled = false;

    private string objectTag;

    private GameObject fillObj;

    public bool IsFilled
    {
        get { return isFilled; }
    }

    private void Update()
    {
        SpriteOf();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsFilled)
        {
            fillObj = collision.gameObject;

            objectTag = collision.gameObject.tag;
        }

        isFilled = true;

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == objectTag)
        {
            isFilled = false;
        }
    }

    void SpriteOf()
    {
        if (fillObj != null)
        {
            if (fillObj.GetComponent<DragAndDropPuzzles>().IsDragging == false)
            {
                if (isFilled)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                }

                else
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        
        
    }
}
