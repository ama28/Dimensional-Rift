using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveList", order = 1)]
public class WaveList : ScriptableObject
{
    [SerializeField] List<Wave> waves;
}
