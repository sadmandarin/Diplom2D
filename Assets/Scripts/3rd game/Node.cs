using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    [SerializeField]
    private List<int> lines;

    [SerializeField]
    private List<Node> neighbours;

    public List<Node> Neighbours { get { return neighbours; } }

    public List<int> Lines { get { return lines; } }
}
