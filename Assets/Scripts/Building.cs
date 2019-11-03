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

private class Reading {
    private DateTime readingDate;
    private double value;

    Reading(string UTCDateTime, string readingValue) {
        int year = int.Parse(UTCDateTime.Substring(0,4));
        int month = int.Parse(UTCDateTime.Substring(5,7));
        int day = int.Parse(UTCDateTime.Substring(8,10));
        this.readingDate = System.DateTime(year, month, day);
        this.value = double.Parse(readingValue);
    }

    public double getValue() {
        return this.value;
    }

    public DateTime getReadingDate() {
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
    public Reading currentReading;
    public Reading previousReading;

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
        readingValue newReading = new Reading(UTCDateTime, readingValue);
        if (this.previousReading == null) {
            this.previousReading = newReading;
        } else if (this.currentReading == null) {
            this.currentReading = newReading;
        } else if (this.currentReading.getReadingDate().CompareTo(newReading.getReadingDate()) < 0) {
            this.previousReading = this.currentReading;
            this.currentReading = newReading;
        } else if (this.previousReading.getReadingDate().CompareTo(newReading.getReadingDate()) <= 0) {
            this.previousReading = newReading;
        }
    }

    private int daysApart(DateTime dt1, DateTime dt2) {
        int result = 0;
        if (dt1.CompareTo(dt2) < 0) {
            while(dt1.CompareTo(dt2) < 0){
                result ++;
                dt1.AddDays(1);
            }
        } else if(dt2.CompareTo(dt1) < 0) {
            while (dt2.CompareTo(dt1) < 0) {
                result ++;
                dt2.AddDays(1);
            }
        }
        return result;
    }

    public double calculateLatestDailyConsumption() {
        double consumed = this.currentReading.getValue() - this.previousReading.getValue();
        int daysElapsed = this.daysApart(this.previousReading.getReadingDate(), this.currentReading.getReadingDate());
        return consumed/daysElapsed;
    }
}
