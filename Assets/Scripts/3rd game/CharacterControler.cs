using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlelr : MonoBehaviour
{
    [SerializeField]
    private Node currentNode;


    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Sprite chosedNode;

    [SerializeField]
    private Sprite defoltNode;

    public Node CurrentNode { get { return currentNode; } }
    private void Start()
    {
        if (currentNode != null)
        {
            foreach (var item in currentNode.Neighbours)
            {
                item.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ѕроверка, что нажатие произошло на узле
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Node clickedNode = hit.collider.GetComponent<Node>();
                if (clickedNode != null && clickedNode.Neighbours.Contains(currentNode) && clickedNode.gameObject.tag != "NonPlayer")
                {
                    bool closed = false;

                    if ((currentNode.Lines.Contains(5) && clickedNode.Lines.Contains(5)) || (currentNode.Lines.Contains(6) && clickedNode.Lines.Contains(6)) || (currentNode.Lines.Contains(8) && clickedNode.Lines.Contains(8)))
                    {
                        closed = true;
                    }

                    if (gameObject.GetComponent<GameController>().Hod % 2 == 0 && closed != true)
                    {
                        // ѕеремещение персонажа к выбранному узлу
                        MoveCharacter(clickedNode);
                    }
                }
            }
        }

        if (currentNode != null && gameObject.GetComponent<GameController>().Hod % 2 == 0)
        {
            foreach (var item in currentNode.Neighbours)
            {
                if (item.gameObject.tag != "NonPlayer" && gameObject.GetComponent<GameController>().Coroutine == null)
                {
                    bool closed = false;

                    if ((currentNode.Lines.Contains(5) && item.Lines.Contains(5)) || (currentNode.Lines.Contains(6) && item.Lines.Contains(6)) || (currentNode.Lines.Contains(8) && item.Lines.Contains(8)))
                    {
                        closed = true;
                    }

                    if (closed != true)
                    {
                        item.gameObject.GetComponent<BoxCollider2D>().enabled = true;

                        item.gameObject.GetComponent<SpriteRenderer>().sprite = chosedNode;
                    }
                    
                }
            }
        }
    }

    void MoveCharacter(Node targetNode)
    {
        foreach (var item in currentNode.Neighbours)
        {
            item.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            item.gameObject.GetComponent<SpriteRenderer>().sprite = defoltNode;
        }

        currentNode = targetNode;

        currentNode.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        Vector3 actualPos = targetNode.transform.position - new Vector3(0, -0.6f, 0);

        gameObject.GetComponent<GameController>().Coroutine = StartCoroutine(gameObject.GetComponent<GameController>().MovingCharacter(player, actualPos));
    }

    
}
