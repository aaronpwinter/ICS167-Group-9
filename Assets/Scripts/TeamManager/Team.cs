using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public class Team
{
    private GameObject mainPlayer;
    // unitDict is a dictionary that stores all the units (game objects) that belong to
    // the mainPlayer. The reason why to use dictionary with game object's instance ID
    // as key is that it is easier to remove game objects when they die in this way.
    private Dictionary<int, GameObject> unitDict = new Dictionary<int, GameObject>();
    private string tag;

    public Team(GameObject mainPlayer, List<GameObject> units, string tag)
    {
        this.mainPlayer = mainPlayer;
        foreach (GameObject go in units)
        {
            unitDict.Add(go.GetInstanceID(), go);
        }
        this.tag = tag;
    }

    public void setMainPlayerToNull()
    {
        this.mainPlayer = null;
    }

    public void setMainPlayer(GameObject newMainPlayer)
    {
        this.mainPlayer = newMainPlayer;
    }

    public void addNewUnit(GameObject newUnit)
    {
        if (!unitDict.ContainsKey(newUnit.GetInstanceID()))
        {
            unitDict.Add(newUnit.GetInstanceID(), newUnit);
        }
    }

    public void removeUnit(GameObject unit)
    {
        if (unitDict.ContainsKey(unit.GetInstanceID()))
        {
            unitDict.Remove(unit.GetInstanceID());
        }
    }

    public GameObject getMainPlayer()
    {
        return mainPlayer;
    }

    public List<GameObject> getAllUnitsInList()
    {
        List<GameObject> newList = new List<GameObject>();
        foreach (GameObject go in unitDict.Values)
        {
            newList.Add(go);
        }
        return newList;
    }

    public string getTag()
    {
        return tag;
    }

    public bool contain(GameObject go)
    {
        return unitDict.ContainsValue(go);
    }
}