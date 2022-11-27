using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour, Interactable
{
    private Canvas canvas;
    private enum PlayerTarget {Farmer, Shooter};
    [SerializeField]
    private PlayerTarget type;

    void Start() {
        canvas = GetComponentInChildren<Canvas>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Slash)) {
            Debug.Log("kdjsfkld");
        }
    }

    public void OnInteract(Player player) {
        ButtonPress(player);
    }

    public void OnRelease(Player player) {
        return;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (type == PlayerTarget.Farmer && col.tag == "PlayerFarmer")
            canvas.enabled = true;
        else if (type == PlayerTarget.Shooter && col.tag == "PlayerShooter")
            canvas.enabled = true;
    }

    void OnTriggerExit2D(Collider2D col) {
        if (type == PlayerTarget.Farmer && col.tag == "PlayerFarmer")
            canvas.enabled = false;
        else if (type == PlayerTarget.Shooter && col.tag == "PlayerShooter")
            canvas.enabled = false;
    }

    void OnTriggerStay2D(Collider2D col){
        if (type == PlayerTarget.Shooter && col.tag == "PlayerShooter"){
            Debug.Log("hello");
            if (Input.GetKey(KeyCode.Slash)) {
                Debug.Log("AAAAAAAA");
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
