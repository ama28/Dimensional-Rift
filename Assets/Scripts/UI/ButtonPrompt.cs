using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour, Interactable
{
    private Canvas canvas;
    private enum PlayerTarget {Farmer, Shooter, Harvest};
    [SerializeField]
    private PlayerTarget type;
    private Farm farm;

    void Start() {
        canvas = GetComponentInChildren<Canvas>();
        if(type == PlayerTarget.Harvest) {
            farm = transform.parent.GetComponent<Farm>();
        }
    }

    public void OnInteract(Player player) {
        //if (GameManager.Instance.GameState != GameManager.GameStateType.BuildPhase) return;

        if(type == PlayerTarget.Harvest) {
            farm.Harvest();
        } else {
            ButtonPress(player);
        }
    }

    public void OnRelease(Player player) {
        return;
    }

    void OnTriggerEnter2D(Collider2D col) {
        //if (GameManager.Instance.GameState != GameManager.GameStateType.BuildPhase) return;
        
        if (type == PlayerTarget.Farmer && col.tag == "PlayerFarmer")
            canvas.enabled = true;
        else if (type == PlayerTarget.Shooter && col.tag == "PlayerShooter")
            canvas.enabled = true;
        else if (type == PlayerTarget.Harvest && col.tag == "PlayerFarmer") {
            if(farm.isHarvestable()) {
                canvas.enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if ((type == PlayerTarget.Farmer || type == PlayerTarget.Harvest) && col.tag == "PlayerFarmer")
            canvas.enabled = false;
        else if (type == PlayerTarget.Shooter && col.tag == "PlayerShooter")
            canvas.enabled = false;
    }

    void OnTriggerStay2D(Collider2D col){
        if (type == PlayerTarget.Shooter && col.tag == "PlayerShooter"){
            if (Input.GetKey(KeyCode.Slash)) {
                ButtonPress(col.transform.GetComponent<Player>());
            }
        }
    }

    void EnableCanvas() {
        canvas.enabled = true;
    }

    void DisableCanvas() {
        canvas.enabled = false;
    }

    void ButtonPress(Player player) {
        switch (player.playerType) {
            case Player.PlayerType.Farmer:
                GameManager.Instance.shopManager.OpenShop(ShopManager.ShopType.farmer);
                break;
            case Player.PlayerType.Shooter:
                GameManager.Instance.shopManager.OpenShop(ShopManager.ShopType.shooter);
                break;
        }
    }
}
