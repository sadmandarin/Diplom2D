using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpokeNPC", menuName = "ScriptableObjects/SpokeNPC", order = 2)]
public class SpokeNPC : ScriptableObject
{
    [SerializeField]
    private List<bool> sadNPC;

    public List<bool> SadNPC
    {
        get { return sadNPC; }
    }
}
