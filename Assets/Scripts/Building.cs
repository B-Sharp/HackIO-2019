using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource {
    ChilledWater = 0,
    HotWater = 1,
    Steam = 2,
    Electricity = 3,
    Unknown = 4,
}

public class Reading {
    private System.DateTime readingDate;
    private double value;

    public Reading(string UTCDateTime, string readingValue) {
        if (UTCDateTime.Equals("min")){
            readingDate = System.DateTime.MinValue;
        } else{
            int year = int.Parse(UTCDateTime.Substring(0,4));
            int month = int.Parse(UTCDateTime.Substring(5,2));
            int day = int.Parse(UTCDateTime.Substring(8,2));
            this.readingDate = new System.DateTime(year, month, day);
        }
        this.value = double.Parse(readingValue);
    }

    public double getValue() {
        return this.value;
    }

    public System.DateTime getReadingDate() {
        return this.readingDate;
    }
}

public class Building
{
    public string name;
    public double latitude;
    public double longitude;
    public List<Resource> resources;
    public string purpose;
    public string meterID;
    public Reading currentReading = new Reading("min", "0.0");
    public Reading previousReading = new Reading("min", "0.0");

    public Building (string name, double lat, double lon, Resource res, string purpose, string meterID) {
        this.name = name;
        this.latitude = lat;
        this.longitude = lon;
        this.resources = new List<Resource>();
        this.resources.Add(res);
        this.purpose = purpose;
        this.meterID = meterID;
    }

    public void addResource (Resource res) {
        this.resources.Add(res);
    }

    public void  addReading(string UTCDateTime, string readingValue) {
        Reading newReading = new Reading(UTCDateTime, readingValue);
        if (this.previousReading.getReadingDate.Equals(System.DateTime.Min)) {
            this.previousReading = newReading;
        } else if (this.currentReading.getReadingDate.Equals(System.DateTime.MinValue)) {
            if(newReading.getReadingDate.CompareTo(this.previousReading) > 0) {
                this.currentReading = newReading;
            } else {
                this.currentReading = this.previousReading;
                this.previousReading = newReading;
            }
        } else if (this.currentReading.getReadingDate().CompareTo(newReading.getReadingDate()) < 0) {
            this.previousReading = this.currentReading;
            this.currentReading = newReading;
        } else if (this.previousReading.getReadingDate().CompareTo(newReading.getReadingDate()) <= 0) {
            this.previousReading = newReading;
        }
    }

    private int daysApart(System.DateTime dt1, System.DateTime dt2) {
        int result = 0;
        if (dt1.CompareTo(dt2) < 0) {
            while(dt1.Day != dt2.Day){
                result ++;
                Debug.Log("1");
                dt1 = dt1.AddDays(1);
            } while(dt1.Month != dt2.Month) {
                Debug.Log("2");
                result += System.DateTime.DaysInMonth(dt1.Year, dt1.Month);
                dt1 = dt1.AddMonths(1);
            } while(dt1.Year != dt2.Year) {
                Debug.Log("3");
                if (System.DateTime.IsLeapYear(dt1.Year)) {
                    result += 366;
                } else {
                    result += 365;
                }
                dt1 = dt1.AddYears(1);
            }
        } else if(dt2.CompareTo(dt1) < 0) {
            while(dt1.Day != dt2.Day){
                Debug.Log("4");
                result ++;
                dt2 = dt2.AddDays(1);
            } while(dt1.Month != dt2.Month) {
                Debug.Log("5");
                result += System.DateTime.DaysInMonth(dt2.Year, dt2.Month);
                dt2 = dt2.AddMonths(1);
            } while(dt1.Year != dt2.Year) {
                Debug.Log("6");
                if (System.DateTime.IsLeapYear(dt2.Year)) {
                    result += 366;
                } else {
                    result += 365;
                }
                dt2 = dt2.AddYears(1);
            }
        }
        return result;
    }

    public bool hasElectricData () {
        return (!this.previousReading.getReadingDate().Equals(System.DateTime.MinValue) && !this.currentReading.getReadingDate().Equals(System.DateTime.MinValue));
    }

    public double calculateLatestDailyConsumption() {
        double consumed = System.Math.Abs(this.currentReading.getValue() - this.previousReading.getValue());
        int daysElapsed = this.daysApart(this.previousReading.getReadingDate(), this.currentReading.getReadingDate());
        return consumed/daysElapsed;
    }
}
