using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//info when dealing damage
public struct HitInfo {
    public Being source;
    //TODO: change to a force unit vector instead of a position
    public Vector3 sourcePos; //may be bullet or person, for angle, separate in case source is dead
    public float damage;
    public float knockbackScalar;
}
