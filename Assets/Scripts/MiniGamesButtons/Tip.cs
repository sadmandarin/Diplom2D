using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour
{
    [SerializeField]
    private GameObject tip;

    [SerializeField]
    private AnimationClip anim;

    private int count = 0;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        tip.SetActive(true);

        StartCoroutine(Stop());

        button.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        
    }


    void OnClick()
    {
        if (count % 2 == 0)
        {
            tip.SetActive(true);

            count++;
        }
        else
        {
            tip.SetActive(false);

            count++;
        }
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(4.1f);

        tip.SetActive(false);
    }
}
