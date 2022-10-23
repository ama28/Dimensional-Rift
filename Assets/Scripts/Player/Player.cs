using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Being
{
    [System.Serializable]
    public class PlayerStats {
        public float speed = 4f;
    }
    public float maxHealth = 6f;

    public enum PlayerType {
        Farmer, Shooter
    }
    public PlayerType playerType;

    public Rigidbody2D rb;
    public virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

}
