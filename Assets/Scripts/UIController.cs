using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text UIText;

    public void updateUI(GameObject obj) {
        BuildingScript buildingData = obj.GetComponent<BuildingScript>();
        UIText.text = buildingData.data.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIText = gameObject.GetComponent<Text>();
        UIText.text = "Lorem ipsum";
    }

}
