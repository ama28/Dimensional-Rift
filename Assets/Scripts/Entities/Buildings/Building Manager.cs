using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    Grid grid;
    public List<Building> buildings;

    public int margin;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    bool IsBuildingValid(Building newBuilding, Vector2Int coords) {
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

    void PlaceBuilding(Building building, Vector2Int coords) {
        Vector3 pos = grid.CellToWorld(new Vector3Int(coords.x, coords.y, 0));
        Building newBuilding = Instantiate(building, pos, Quaternion.identity);
        buildings.Add(newBuilding);
    }
}
