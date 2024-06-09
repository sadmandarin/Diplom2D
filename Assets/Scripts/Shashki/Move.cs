using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject feature;

    private GameObject featureClone;

    public Square square;

    public bool select;

    public bool walk;
    [SerializeField]
    private bool _loaded;

    public int x;
    public int y;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clip;

    public bool Loaded
    {
        get { return _loaded; }
        set { _loaded = value; }
    }

    private void Start()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (square.selectedFeature)
        {
            audioSource.PlayOneShot(square.clip);

            if (walk && !_loaded)
            {
                StartCoroutine(Moving());

                Debug.Log("Движение");
            }
        }
        else if (feature && square.step == true) 
        {
            audioSource.PlayOneShot(square.clip);

            square.selectedFeature = feature;

            Debug.Log("figura vibrana");

            

            feature = null;

            select = true;

            square.WalkController(x, y);

            select = false;
        }
    }

    

    IEnumerator Moving()
    {
        GameObject objectMove = square.selectedFeature;

        while (Vector3.Distance(objectMove.transform.position, gameObject.transform.position) > 0.01f)
        {
            objectMove.transform.position = Vector3.MoveTowards(objectMove.transform.position, transform.position, 0.1f);

            yield return new WaitForSeconds(0.001f);
        }
        feature = square.selectedFeature;

        square.selectedFeature = null;

        square.SquareArray[x, y].GetComponent<Move>().Loaded = true;

        square.step = false;

        square.Clear();

        //square.ClearField();
    }

}
