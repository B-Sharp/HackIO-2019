using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Map : MonoBehaviour
{
    public string config_path = "Assets/Data/HackathonConfig.csv";
    public string daily_path = "Assets/Data/HackathonDataDaily.csv";

    // Start is called before the first frame update
    public Dictionary<string, Building> buildings;
    public BuildingScript prefab;

    public const double INVALID_COORD = 1337.0;

    void Start()
    {
        this.buildings = new Dictionary<string, Building>();
        this.getConfig();
        this.getData();
        this.populateMap();       
    }

    private void getConfig () {
        using (var reader = new StreamReader("Assets/Data/HackathonConfig.csv"))
        {   
            var num = 0;
            while (!reader.EndOfStream)
            {   
                num ++;
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                //get data
                string name = values[5];
                name = name.Trim('\"');

                string lat_str = values[8];
                lat_str = lat_str.Trim('\"');

                string long_str = values[9];
                long_str = long_str.Trim('\"');

                string purpose = values[12];
                purpose = purpose.Trim('\"');

                string meter = values[1];
                meter = meter.Trim('\"');

                Resource resource;

                string res_str = values[4];
                res_str.Trim('\"');

                // // get Resource and Unit

                // if (res_str.Equals("Heating Hot Water"))
                // {
                //     resource = Resource.HotWater;
                // }
                // else if (res_str.Equals("Chilled Water "))
                // {
                //     resource = Resource.ChilledWater;
                // }
                // else if (res_str.Equals("Steam"))
                // {
                //     resource = Resource.Steam;
                // }
                if (res_str.Equals("Electricity"))
                {
                    resource = Resource.Electricity;
                }
                else
                {   
                    continue;
                    // resource = Resource.Unknown;
                    // Debug.Log("Unknow source");
                    // Debug.Log("unknown source: " + res_str);
                }

                // get lat and lon
                double latitude;
                double longitude;
                try
                {
                    latitude = double.Parse(lat_str);
                    longitude = double.Parse(long_str);
                }
                catch
                {
                    latitude = INVALID_COORD;
                    longitude = INVALID_COORD;
                }



                if (!this.buildings.ContainsKey(name))
                {
                    //put it indictionary
                    Building building = new Building(name, latitude, longitude, resource, purpose, meter);
                    this.buildings.Add(name, building);
                }
                else
                {
                    // add resource info to building entry
                    buildings[name].addResource(resource);
                }

            }
        }
    }

    private void getData () {
        using (var reader = new StreamReader(daily_path)) {
            string currentMeterId = "";
            while (!reader.EndOfStream) {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                string status_str = values[6];
                status_str = status_str.Trim('\"');
                if (status_str.Equals("OK")) {
                    string newMeterID = values[0];
                    newMeterID = newMeterID.Trim('\"');
                    if (!newMeterID.Equals(currentMeterId)) {
                        currentMeterId = newMeterID;
                    }
                    foreach (KeyValuePair<string, Building> entry in this.buildings) {
                        if (entry.Value.meterID.Equals(currentMeterId)) {
                            string energyStr = values[1];
                            energyStr = energyStr.Trim('\"');
                            string dateString = values[3];
                            dateString = dateString.Trim('\"');
                            entry.Value.addReading(dateString, energyStr);
                        }
                    }
                }
            }
        }
    }

    private void populateMap() {
        foreach (var building in buildings) {
            BuildingScript BS = Instantiate<BuildingScript>(prefab);
            BS.data = building.Value;

            //scale the latitude and lonitude
            float x = (float) ((40.1 - building.Value.latitude) * 5000.0) - 350f;
            float y = (float) ((-83.03 - building.Value.longitude) * 5000.0) + 50f;

            Vector3 pos = new Vector3(y, 0.4f, x);
            BS.gameObject.transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
