using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveList", menuName = "ScriptableObjects/WaveList", order = 1)]
public class WaveList : ScriptableObject
{
    public List<Wave> waves;

    public Wave GetWave(int level)
    {
        foreach (Wave wave in waves)
        {
            if (wave.waveRange.x <= level && wave.waveRange.y >= level)
            {
                return wave;
            }
        }

        return waves[waves.Count - 1];
    }
}
