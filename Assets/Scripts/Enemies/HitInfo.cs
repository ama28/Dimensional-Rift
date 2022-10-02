using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//info when dealing damage
public struct HitInfo {
    public Being source;
    public Transform sourceTransform; //may be bullet or person, for angle
    public float damage;
    public float knockbackScalar;
}
