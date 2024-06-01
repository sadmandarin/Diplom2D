using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackWhenDialog : MonoBehaviour
{
    [SerializeField]
    private GameObject back;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<DialogManager>().IsDialog == true)
        {
            back.transform.position = new Vector3 (0, 1.168381f, 0.3071298f);
        }
    }
}
