using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOnPlace : MonoBehaviour
{
    [SerializeField]
    private bool isCorrectPlace;

    public bool IsCorrectPlace
    {
        get { return isCorrectPlace; }
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DetectObject>() != null)
        {
            if (!collision.gameObject.GetComponent<DetectObject>().IsFilled)
            {
                if (collision.gameObject.tag == gameObject.tag)
                {
                    isCorrectPlace = true;

                    Debug.Log("Is on Place");
                }

                else
                {
                    Debug.Log("Not in place");
                }
            }
        }
    }

    
}
