using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmHealthBar : MonoBehaviour
{
    public Farm farm;
    public Image healthBarImage;
    
    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(farm.health / farm.stats.maxHealth, 0, 1f);
    }
}
