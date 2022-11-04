using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableUI : MonoBehaviour
{
    private Transform buildingUILayout;
    public List<GameObject> buildingUIPrefabs;

    private void Start()
    {
        buildingUILayout = GameManager.Instance.mainUI.transform.Find("Buildings");
    }

    void showBuildingUI()
    {
        if (buildingUILayout == null) Debug.Log("no buildings UI holder");
        Debug.Log("show buidlings UI");
        foreach (GameObject item in GameManager.Instance.BuildingManager.inventory)
        {
            Building building = item.GetComponent<Building>();
            switch (building.type)
            {
                case Building.buildingType.farm:
                    Instantiate(buildingUIPrefabs[0], buildingUILayout);
                    break;
                case Building.buildingType.movPlat:
                    Instantiate(buildingUIPrefabs[1], buildingUILayout);
                    break;
                case Building.buildingType.statPlat:
                    Instantiate(buildingUIPrefabs[2], buildingUILayout);
                    break;
            }
        }
    }

    private void Update()
    {
    }
}
