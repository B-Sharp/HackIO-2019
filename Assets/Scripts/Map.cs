using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Map : MonoBehaviour
{
    public string config_path = "Assets/Data/HackathonConfig.csv";

    // Start is called before the first frame update
    public Dictionary<string, Building> buildings;

    void Start()
    {
        this.buildings = new Dictionary<string, Building>();

        using (var reader = new StreamReader("Assets/Data/HackathonConfig.csv")) {
            while (!reader.EndOfStream) {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                //get data
                string name = values[5];
                name.Trim('\"');
                string lat_str = values[8];
                lat_str.Trim('\"');
                string long_str = values[9];
                long_str.Trim('\"');
                string campus = values[10];
                string purpose = values[12];

                Resource resource;

                string res_str = values[4];
                res_str.Trim('\"');
                // get Resource and Unit
                //Debug.Log(res_str);

                if (res_str.Equals("\"Heating Hot Water\"")) {
                    resource = Resource.HotWater;
                    Debug.Log("Hot Water");
                } else if (res_str.Equals("\"Chilled Water \"")) {
                    resource = Resource.ChilledWater;
                    Debug.Log("Cold water");
                } else {
                    resource = Resource.Unknown;
                    Debug.Log("Unknow source");
                    Debug.Log("unknown source: " + res_str);
                }

                // get lat and lon
                double latitude;
                double longitude;
                try {
                     latitude = double.Parse(lat_str);
                     longitude = double.Parse(long_str);
                } catch {
                    //Debug.Log(lat_str + " " + long_str);
                    latitude = 0.0;
                    longitude = 0.0;
                }
               

                
                if (!this.buildings.ContainsKey(name)) {
                    //put it indictionary
                    Building building = new Building(name, latitude, longitude, resource, purpose);
                    this.buildings.Add(name, building);
                } else {
                    // add resource info to building entry
                    buildings[name].addResource(resource);
                }
                
            }
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
