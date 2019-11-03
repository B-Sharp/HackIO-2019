using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject UITextObject;
    public Text UIText;
    public GameObject UIPurposeObject;
    public Text UIPurpose;
    //public Image image;

    public void updateUI(GameObject obj) {
        BuildingScript buildingData = obj.GetComponent<BuildingScript>();
        UIText.text = buildingData.data.name;
        UIPurpose.text = buildingData.data.purpose;
    }

    // Start is called before the first frame update
    void Start()
    {
        UITextObject = new GameObject("UIText");
        UIPurposeObject = new GameObject("UIPurpose");
        UIText = UITextObject.AddComponent<Text>();
        UIPurpose = UIPurposeObject.AddComponent<Text>();
        UIText.text = "Lorem ipsum";
        UIPurpose.text = "Lorem ipsum";
    }

}
