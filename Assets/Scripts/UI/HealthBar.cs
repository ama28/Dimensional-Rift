using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    private Player playerFarmer;

    private void Start()
    {
        playerFarmer = GameManager.Instance.playerFarmer;
    }

    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(playerFarmer.health / playerFarmer.maxHealth, 0, 1f);
    }
}
