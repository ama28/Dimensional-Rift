using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    Grid grid;
    private List<GameObject> inventory = new List<GameObject>();
    public List<Building> currentBuildings;
    public int margin;

    public GameObject buildingPrefab;
    public Building currentBuilding;
    public List<Building> startBuildings; 

    private Vector2Int mousePosTile;

    void OnEnable() {
        GameManager.OnActionPhaseStart += OnActionPhaseStart;
        GameManager.OnBuildPhaseStart += OnBuildPhaseStart;
    }

    void OnDisable() {
        GameManager.OnActionPhaseStart -= OnActionPhaseStart;
        GameManager.OnBuildPhaseStart -= OnBuildPhaseStart;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        startBuildings.ForEach(x => AddBuildingToInventory(x.gameObject));
    }

    void Update()
    {
        if(inventory.Count > 0 && GameManager.Instance.GameState == GameManager.GameStateType.BuildPhase) {
            //move building with mouse
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePos = mouseWorldPoint + (Camera.main.transform.forward * 10.0f);
            mousePosTile = (Vector2Int)grid.WorldToCell(mousePos);
            currentBuilding.transform.position = grid.CellToWorld((Vector3Int)mousePosTile) + grid.cellSize * 0.5f;
            currentBuilding.coordinates = mousePosTile - currentBuilding.size/2;
            
            //setting color of building
            if(IsBuildingValid(currentBuilding, mousePosTile)) {
                currentBuilding.SetPlaceable(true);
            } else {
                currentBuilding.SetPlaceable(false);
            }
        }
    }

    void OnActionPhaseStart(Wave wave) {
        //TODO: Hide UI
    }

    void OnBuildPhaseStart() {
        //TODO: Show UI
        if(inventory.Count > 0) {
            currentBuilding = inventory[0].GetComponent<Building>();
        } else {
            currentBuilding = null;
        }
    }

    public void OnPlaceButton() {
        Debug.Log("Place: " +  IsBuildingValid(currentBuilding, mousePosTile));
        if(IsBuildingValid(currentBuilding, mousePosTile)) {
            PlaceBuilding(currentBuilding.gameObject, mousePosTile);
            currentBuilding.OnPlace();
            if(inventory.Count > 0) {
                currentBuilding = inventory[0].GetComponent<Building>();
                currentBuilding.OnSelect();
            }
        }
    }

    bool IsBuildingValid(Building newBuilding, Vector2Int coords) {
        if(newBuilding == null) {
            Debug.LogError("new building is null!");
            return false;
        }
        return currentBuildings.TrueForAll(building => {
            if(!building.GetComponent<Building>().collidable) { //not collidable
                return true;
            }
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
        building.transform.position = grid.CellToWorld(new Vector3Int(coords.x, coords.y, 0)) + grid.cellSize * 0.5f;
        building.GetComponent<Building>().OnPlace();
        currentBuildings.Add(building.GetComponent<Building>());
        inventory.Remove(building);
    }

    public void AddBuildingToInventory(GameObject buildingPrefab) {
        GameObject newBuildingPrefab = Instantiate(buildingPrefab);
        newBuildingPrefab.name += Random.Range(0, 100);
        inventory.Add(newBuildingPrefab);
        if(!currentBuilding) {
            currentBuilding = inventory[0].GetComponent<Building>();
            currentBuilding.OnSelect();
        } else {
            newBuildingPrefab.GetComponent<Building>().OnDeselect();
        }
    }

    public int GetNumBuildingsInInventory() {
        return inventory.Count;
    }
}
