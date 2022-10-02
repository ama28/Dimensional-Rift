using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveList", menuName = "ScriptableObjects/WaveList", order = 1)]
public class WaveList : ScriptableObject
{
    public List<Wave> waves;

}
