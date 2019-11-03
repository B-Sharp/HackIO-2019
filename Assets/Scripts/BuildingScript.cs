using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public Building data;
    public string buildingName;

    public void Start() {
        buildingName = data.name;
    }
}
