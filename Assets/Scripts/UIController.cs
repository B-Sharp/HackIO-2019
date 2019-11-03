using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text UIText;
    public Text UIPurpose;
    public Image image;

    public void updateUI(GameObject obj) {
        BuildingScript buildingData = obj.GetComponent<BuildingScript>();
        UIText.text = buildingData.data.name;
        UIPurpose.text = buildingData.data.purpose;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIText.text = "Lorem ipsum";
        UIPurpose.text = "Lorem ipsum";
    }

}
