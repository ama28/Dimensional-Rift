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

    public int currentEnemyCount;
    public int totalEnemyCount;

    private void Start()
    {
        playerFarmer = GameManager.Instance.playerFarmer;
    }

    public void DelayForSpawning(Wave wave)
    {
        StartCoroutine(WaitForSpawning());
    }

    public IEnumerator WaitForSpawning(){
        yield return new WaitForSeconds(2f);
        healthBarImage.enabled = true;
    }

    void Update()
    {
        currentEnemyCount = GameManager.Instance.spawnManager.instanced.Count;
        totalEnemyCount = GameManager.Instance.spawnManager.waveSize;

        if (type == barType.enemies)
            healthBarImage.fillAmount = Mathf.Clamp(1f - (float)GameManager.Instance.spawnManager.instanced.Count / (float)GameManager.Instance.spawnManager.waveSize, 0, 1f);
    }

    void OnEnable() {
        GameManager.OnBuildPhaseStart += ResetBar;
        GameManager.OnActionPhaseStart += DelayForSpawning;
    }

    void OnDisable() {
        GameManager.OnBuildPhaseStart -= ResetBar;
        GameManager.OnActionPhaseStart -= DelayForSpawning;
    }

    public void ResetBar() {
        if (type == barType.enemies)
            healthBarImage.enabled = false;
    }

    public void UpdateHealthBar()
    {
        if (type == barType.health)
            healthBarImage.fillAmount = Mathf.Clamp(playerFarmer.health / playerFarmer.maxHealth, 0, 1f);
    }
}
