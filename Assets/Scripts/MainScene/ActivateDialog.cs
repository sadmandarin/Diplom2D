using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ActivateDialog : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 dialogPos;

    [SerializeField]
    private GameObject button;

    public Vector3 DialogPos
    {
        get { return dialogPos; }
    }

    private void Update()
    {
        if (gameObject.GetComponent<DialogManager>().IsDialog == true)
        {
            button.SetActive(false);
        }

        else if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 7f)
        {
            if (button.activeSelf == false)
            {
                button.SetActive(true); ;
            }
        }
        else
        {
            button.SetActive(false);
        }
    }
}
