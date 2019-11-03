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
    public GameObject compass;

    public void updateUI(GameObject obj) {
        BuildingScript buildingData = obj.GetComponent<BuildingScript>();
        UIText.text = buildingData.data.name;
        UIPurpose.text = buildingData.data.purpose;
    }

    public void updateCompass (Transform camera) {
        compass.transform.rotation = Quaternion.Euler(0, 0, camera.rotation.eulerAngles.y + 180f);
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
