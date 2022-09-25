using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Player
{
    public class ShooterStats : PlayerStats {
        public float jumpForce = 4f;
    }

    public ShooterStats stats = new ShooterStats();

    public virtual void Start() {
        health = stats.maxHealth;
        invulnerable = true;
    }
}
