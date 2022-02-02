//Aaron Winter
//Alec Kaxon-Rupp
//Daniel Zhang


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



//Spawner will handle all unit spawning for one team.
public class Spawner : MonoBehaviour
{
    const string teamUnitContainerPrefix = "Team_";

    public GameObject unitContainer = null; //Am empty GameObject in the Hierarchy, only used for containing these spawned units.
    public GameObject unitPrefab;
    public UnitScriptableObject playerUnit;
    public UnitScriptableObject[] spawnableUnits;
    public string team;
    public uint maxSpawnDistanceFromPlayer = 150;

    private GameObject myTeamContainer;
    private GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myTeamContainer = new GameObject(teamUnitContainerPrefix + team);
        if(unitContainer != null)
        { //Put the teamContainer under this general Unit container
            myTeamContainer.transform.SetParent(unitContainer.transform, true);
        }

        myPlayer = spawnUnit(playerUnit, gameObject.transform.position, gameObject.transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Tries to spawn the unit specified by unitIndex from the Spawner's spawnableUnits[].
    //  Fails (and returns null) if the unitIndex is invalid
    public GameObject spawnUnit(uint unitIndex, Vector3 position, Quaternion rotation = new Quaternion())
    {
        if (unitIndex >= spawnableUnits.Length) return null;
        if (!myPlayer) return null;
        if (Vector3.Distance(position, myPlayer.transform.position) > maxSpawnDistanceFromPlayer) return null;
        UnitScriptableObject unitType = spawnableUnits[unitIndex];

        //Not enough resources to spawn
        if (ResourceScript.GetResourceAmount("Food") < unitType.costFood ||
            ResourceScript.GetResourceAmount("Stone") < unitType.costStone ||
            ResourceScript.GetResourceAmount("Wood") < unitType.costWood) return null;

        //Take away resources
        ResourceScript.SubtractResourceAmount("Food", unitType.costFood);
        ResourceScript.SubtractResourceAmount("Stone", unitType.costStone);
        ResourceScript.SubtractResourceAmount("Wood", unitType.costWood);

        return spawnUnit(spawnableUnits[unitIndex], position, rotation);
    }

    //Spawn on mouse when accessed through unit menu
    public GameObject spawnUnit(uint unitIndex, Transform spawnPos)
    {
        Debug.Log("Yes" + spawnPos.position);
        return spawnUnit(unitIndex, spawnPos.position, spawnPos.rotation);
    }

    private GameObject spawnUnit(UnitScriptableObject unitType, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {
        //Instantiate Unit, get UnitScript & Health
        GameObject spawnedUnit = Instantiate(unitPrefab, position, rotation, myTeamContainer.transform);
        spawnedUnit.name = unitType.unitName;
        UnitScript script = spawnedUnit.GetComponent<UnitScript>();

        //Add UnitScriptableObject (raw data) & team to the Unit
        script.unitData = unitType;
        script.team = team;

        return spawnedUnit;
    }
}
