//Aaron Winter
//Alec Kaxon-Rupp
//Daniel Zhang

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



//Spawner will handle all unit spawning for one team.
public class UnitSpawner : MonoBehaviour
{
    const string teamUnitContainerPrefix = "Team_";
    readonly string[] resources = { "Food", "Stone", "Wood", "Silver", "Gold" };

    [SerializeField] private GameObject unitContainer = null; //Am empty GameObject in the Hierarchy, only used for containing these spawned units.
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private UnitSOBase playerUnit;
    [SerializeField] private UnitSOBase[] spawnableUnits;
    [SerializeField] private string team;

    private GameObject myTeamContainer;
    private GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        string teamContainerName = teamUnitContainerPrefix + team;
        if (unitContainer != null)
        { //Put the teamContainer under this general Unit container
            Transform teamContainer = unitContainer.transform.Find(teamContainerName);
            if (teamContainer == null)
            {
                myTeamContainer = new GameObject(teamContainerName);
                myTeamContainer.transform.SetParent(unitContainer.transform, true);
            }
            else
            {
                myTeamContainer = teamContainer.gameObject;
            }
        }
        else
        {
            myTeamContainer = GameObject.Find(teamContainerName);
            if(myTeamContainer == null)
            {
                myTeamContainer = new GameObject(teamContainerName);
            }
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
        
        UnitSOBase unitType = spawnableUnits[unitIndex];

        //Check have enough resources
        for(uint i = 0; i < resources.Length; ++i)
        {
            string rName = resources[i];
            //Not enough resources to spawn
            if (TeamManager.getResourceAmount(rName) < unitType.getCost(rName)) return null;
        }

        //Take away resources
        for (uint i = 0; i < resources.Length; ++i)
        {
            string rName = resources[i];
            //Not enough resources to spawn
            TeamManager.subtractResource(rName, unitType.getCost(rName));
        }

        GameObject spawnedUnit = spawnUnit(spawnableUnits[unitIndex], position, rotation);
        spawnedUnit.tag = TeamManager.getCurrentTeamTag();
        TeamManager.addNewUnit(spawnedUnit);
        return spawnedUnit;
    }

    //Spawn on mouse when accessed through unit menu
    public GameObject spawnUnit(uint unitIndex)
    {
        return spawnUnit(unitIndex, gameObject.transform.position, gameObject.transform.rotation);
    }

    private GameObject spawnUnit(UnitSOBase unitType, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {
        //Spawn the unit
        GameObject spawnedUnit = unitType.spawnUnit(unitPrefab, position, rotation, myTeamContainer.transform);
        spawnedUnit.tag = team;
        if (spawnedUnit.gameObject.GetComponent<AIAnimal>())
        {
            spawnedUnit.tag = "AIAnimal";
        }

        return spawnedUnit;
    }

    public UnitSOBase[] getSpawnableUnits()
    {
        return spawnableUnits;
    }

}
