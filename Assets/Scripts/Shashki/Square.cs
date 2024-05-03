using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    GameObject[,] squareArray;

    [SerializeField]
    private GameObject players;

    [SerializeField]
    private GameObject enemy;

    public GameObject selectedFeature;

    private void Start()
    {
        squareArray = new GameObject[8, 8];

        CreateField();
    }

    private void Update()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    if (squareArray[i, j].GetComponent<Move>().feature != null)
                    {
                        squareArray[i, j].GetComponent<Move>().loaded = true;
                    }

                    else
                    {
                        squareArray[i, j].GetComponent<Move>().loaded = false;
                    }
                }
            }
        }
    }

    void CreateField()
    {
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Vector3 position = new Vector3(i - 3f, j -3.8f, -1);

                GameObject clone = Instantiate(prefab, position, Quaternion.identity);

                squareArray[i, j] = clone;

                if ((i + j) % 2 == 0)
                {
                    clone.AddComponent<Move>();

                    clone.GetComponent<Move>().square = gameObject.GetComponent<Square>();

                    clone.GetComponent<Move>().x = i;
                    clone.GetComponent<Move>().y = j;
                }

                if (i == 7 && j == 3)
                {
                    clone.AddComponent<FeatureEnemyAI>();

                    clone.GetComponent<FeatureEnemyAI>().square = gameObject.GetComponent<Square>();

                    clone.GetComponent<FeatureEnemyAI>().x = i;
                    clone.GetComponent <FeatureEnemyAI>().y = j;
                }
            }
                
        }
        CreateDot();
    }

    void CreateDot()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i < 1)
                {
                    if ((i+j)%2 == 0)
                    {
                        Vector3 pos = squareArray[i, j].transform.position;

                        GameObject clone = Instantiate(players, pos, Quaternion.identity, gameObject.transform);

                        clone.name = "Player";

                        squareArray[i, j].GetComponent<Move>().feature = clone;

                        squareArray[i, j].GetComponent<Move>().loaded = true;
                    }

                }



                if ((i == 7 && j == 3))
                {
                    Vector3 pos = squareArray[i, j].transform.position;

                    GameObject clone = Instantiate(enemy, pos, Quaternion.identity, gameObject.transform);

                    clone.name = "Enemy";

                    squareArray[i, j].GetComponent <FeatureEnemyAI>().feature = clone;

                    squareArray[i, j].GetComponent<FeatureEnemyAI>().loaded = true;
                }
            }
        }
    }

    public void WalkController(int I, int J)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (squareArray[i, j].GetComponent<Move>())
                {
                    if (!squareArray[i, j].GetComponent<Move>().select)
                    {
                        if ((J - j == -1 || J-j == 1))
                        {
                            if(I - i == -1)
                            {
                                if (!squareArray[i,j].GetComponent<Move>().loaded)
                                {
                                    squareArray[i, j].GetComponent<SpriteRenderer>().enabled = true;

                                    squareArray[i, j].GetComponent<Move>().walk = true;
                                }
                            }
                        }
                    }

                }

            }
        }
    }

    public void WalkEnemyController(int I, int J)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (squareArray[i, j].GetComponent<FeatureEnemyAI>())
                {
                    if (squareArray[i, j].GetComponent<Move>())
                    {
                        if (!squareArray[i, j].GetComponent<Move>().select)
                        {
                            if ((J - j == -1 || J - j == 1))
                            {
                                if (I - i == -1 && I - i == 1)
                                {
                                    if (!squareArray[i, j].GetComponent<Move>().loaded)
                                    {
                                        squareArray[i, j].GetComponent<SpriteRenderer>().enabled = true;

                                        squareArray[i, j].GetComponent<FeatureEnemyAI>().walk = true;
                                    }
                                }
                            }
                        }

                    }
                }
                

            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (squareArray[i, j].GetComponent<Move>())
                {
                    if (!squareArray[i, j].GetComponent<Move>().select)
                    {
                        squareArray[i, j].GetComponent<SpriteRenderer>().enabled = false;

                        squareArray[i, j].GetComponent<Move>().walk = false;          
                    }

                }

            }
        }
    }


    //public void ClearField()
    //{
    //    for (int i = 0; i < 8; i++)
    //    {
    //        for (int j = 0; j < 8; j++)
    //        {
    //            if (squareArray[i, j].GetComponent<Move>())
    //            {
    //                squareArray[i, j].GetComponent<Move>().select = false;

    //                squareArray[i, j].GetComponent<Move>().walk = false;
    //            }

    //        }
    //    }
    //    StepController();
    //}
}
