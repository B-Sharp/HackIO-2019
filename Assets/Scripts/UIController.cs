using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text UIText;
    public Text UIPurpose;
    public Text UIkWh;
    public Text UIBTU;
    public Text UIGas;
    public GameObject UICompass;

    public string PPWithCommas (string s) {
        int i = 3;
        while(i < s.Length) {
            s = s.Insert(s.Length - i, ",");
            i +=4;
        }
        return s;
    }

    public void updateUI(GameObject obj) {
        BuildingScript buildingData = obj.GetComponent<BuildingScript>();
        Debug.Log(buildingData.data.name);
        UIText.text = buildingData.data.name;
        UIPurpose.text = buildingData.data.purpose;

        double comsumption = System.Math.Truncate(buildingData.data.calculateLatestDailyConsumption());
        UIkWh.text = PPWithCommas(comsumption.ToString());
        UIBTU.text = PPWithCommas((comsumption * 3412.0).ToString());
        UIGas.text = PPWithCommas((System.Math.Truncate(comsumption / 33.4)).ToString());

    }

    public void updateCompass (Transform camera) {
        UICompass.transform.rotation = Quaternion.Euler(0, 0, camera.rotation.eulerAngles.y + 180f);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIText.text = "Lorem ipsum";
        UIPurpose.text = "Lorem ipsum";
        UIkWh.text = "Lorem ipsum";
    }

}
