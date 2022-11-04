using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public enum barType { health, enemies }
    public barType type;
    public Image healthBarImage;
    private Player playerFarmer;

    private void Start()
    {
        playerFarmer = GameManager.Instance.playerFarmer;
    }

    public void UpdateHealthBar()
    {
        if (type == barType.health)
            healthBarImage.fillAmount = Mathf.Clamp(playerFarmer.health / playerFarmer.maxHealth, 0, 1f);
        else if (type == barType.enemies)
            healthBarImage.fillAmount = Mathf.Clamp(playerFarmer.health / playerFarmer.maxHealth, 0, 1f);
    }
}
