//Alec Kaxon-Rupp
//Daniel Zhang

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class SpawnerUI : MonoBehaviour
{

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject spawner;
    [SerializeField] private Sprite FoodIcon;
    [SerializeField] private Sprite StoneIcon;
    [SerializeField] private Sprite WoodIcon;
    [SerializeField] private Sprite GoldIcon;
    [SerializeField] private Sprite SilverIcon;

    // At the begining of the game this script sets up the player spawn menus. This Menu will display to the players the types of units they can create.
    // This will also display the cost of the units.
    // This method makes it easy to add new units to the game, simply creating a unit prefab with a name and costs will allow it to be displayed in this menu.
    // Units can have a max of 3 different resources making up their cost. If more then 3 resources are assigned, Gold or Silver might not be displayed to the player.
    void Start()
    {
        UnitSOBase[] unitTypes = spawner.GetComponent<UnitSpawner>().getSpawnableUnits();

        for (uint i = 0; i < unitTypes.Length; ++i)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content.transform,false);
            int resource = 1;

            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].name;

            if(unitTypes[i].getCost("Food")!=0)
            {
                button.transform.GetChild(resource).GetComponent<Image>().sprite = FoodIcon;
                button.transform.GetChild(resource+1).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Food");
                resource += 2;
            }

            if (unitTypes[i].getCost("Stone") != 0)
            {
                button.transform.GetChild(resource).GetComponent<Image>().sprite = StoneIcon;
                button.transform.GetChild(resource + 1).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Stone");
                resource += 2;
            }

            if (unitTypes[i].getCost("Wood") != 0)
            {
                button.transform.GetChild(resource).GetComponent<Image>().sprite = WoodIcon;
                button.transform.GetChild(resource + 1).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Wood");
                resource += 2;
            }

            if (unitTypes[i].getCost("Gold") != 0 && resource<7)
            {
                button.transform.GetChild(resource).GetComponent<Image>().sprite = GoldIcon;
                button.transform.GetChild(resource + 1).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Gold");
                resource += 2;
            }

            if (unitTypes[i].getCost("Silver") != 0 && resource < 7)
            {
                button.transform.GetChild(resource).GetComponent<Image>().sprite = SilverIcon;
                button.transform.GetChild(resource + 1).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Silver");
                resource += 2;
            }

            uint localI = i;
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                spawner.GetComponent<UnitSpawner>().spawnUnit(localI);
            });
        }
    }

}
