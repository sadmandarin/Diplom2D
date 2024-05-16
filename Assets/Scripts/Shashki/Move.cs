using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject feature;

    public Square square;

    public bool select;

    public bool walk;
        
    public bool loaded;

    public int x;
    public int y;

    //private void Start()
    //{
    //    square = GameObject.Find("shashki").GetComponent<Square>();
    //}

    

    private void OnMouseDown()
    {
        if (square.selectedFeature)
        {
            if (walk && !loaded)
            {
                StartCoroutine(Moving());

                Debug.Log("Движение");
            }
            else
            {
                feature = square.selectedFeature;

                square.selectedFeature = null;
            }
            
        }
        else if (feature) 
        {
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

        feature.GetComponent<Move>().loaded = true;

        square.Clear();

        //square.ClearField();
    }

}
