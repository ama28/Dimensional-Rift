using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    Grid grid;
    public List<Building> buildings;
    public int margin;

    public GameObject buildingPrefab;
    public Building currentBuilding;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = Instantiate(buildingPrefab);
        currentBuilding = temp.GetComponent<Building>();
    }

    void Update()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = mouseWorldPoint + (Camera.main.transform.forward * 10.0f);
        Vector2Int mousePosTile = (Vector2Int)grid.WorldToCell(mousePos);
        currentBuilding.transform.position = grid.CellToWorld((Vector3Int)mousePosTile) + grid.cellSize * 0.5f;
        currentBuilding.coordinates = mousePosTile;

        if(Input.GetKeyDown(KeyCode.P)) {
            if(IsBuildingValid(currentBuilding.GetComponent<Building>(), mousePosTile)) {
                PlaceBuilding(currentBuilding.gameObject, mousePosTile);
            }
        }
    }

    bool IsBuildingValid(Building newBuilding, Vector2Int coords) {
        if(newBuilding == null) {
            Debug.LogError("new building is null!");
            return false;
        }
        return buildings.TrueForAll(building => {
            if(newBuilding.coordinates.x + newBuilding.size.x + margin < building.coordinates.x
            || building.coordinates.x + building.size.x + margin < newBuilding.coordinates.x) {
                //right side of one building is to the left of the other's left side
                return true;
            }

            if(newBuilding.coordinates.y + newBuilding.size.y + margin < building.coordinates.y
            || building.coordinates.y + building.size.y + margin < newBuilding.coordinates.y) {
                //top side of one building is above the other's bottom side
                return true;
            }

            return false;
        });
    }

    void PlaceBuilding(GameObject building, Vector2Int coords) {
        Vector3 pos = grid.CellToWorld(new Vector3Int(coords.x, coords.y, 0)) + grid.cellSize * 0.5f;
        GameObject newBuildingGameObject = Instantiate(building.gameObject, pos, Quaternion.identity);
        Building newBuilding = newBuildingGameObject.GetComponent<Building>();
        buildings.Add(newBuilding);
    }
}
