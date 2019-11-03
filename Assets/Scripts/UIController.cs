using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text UIText;
    public Text UIPurpose;
    public Text UIkWh;
    public GameObject UICompass;

    public void updateUI(GameObject obj) {
        BuildingScript buildingData = obj.GetComponent<BuildingScript>();
        Debug.Log(buildingData.data.name);
        UIText.text = buildingData.data.name;
        UIPurpose.text = buildingData.data.purpose;
        //UIkWh.text = buildingData.data.calculateLatestDailyConsumption().ToString();
    }

    public void updateCompass (Transform camera) {
        UICompass.transform.rotation = Quaternion.Euler(0, 0, camera.rotation.eulerAngles.y + 180f);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIText.text = "Lorem ipsum";
        UIPurpose.text = "Lorem ipsum";
    }

}
