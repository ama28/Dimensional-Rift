using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Player
{
    [System.Serializable]
    public class ShooterStats : PlayerStats {
        public float jumpForce = 6f;
        public float turnAroundScalar = 30f;
        public float airMovementPenalty = 0.4f;
    }

    public ShooterStats stats = new ShooterStats();

    public virtual void Start() {
        health = maxHealth;
        invulnerable = true;
    }

    public override void TakeDamage(HitInfo hit)
    {
        return;
    }
}
