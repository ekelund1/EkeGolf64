using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public enum Clubs
{
    PUTTER, SW, PW, I9, I8, I7, I6, I5, I4, I3, I2, W4, W3, W1,
}


static class ClubDictionary
{
    private static string jsonFileName = "clubData.json";
    static Dictionary<Clubs, ClubData> dict = new Dictionary<Clubs, ClubData>();

    static ClubDictionary()
    {
        string file = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        if (File.Exists(file))
        {
            string dataAsJson = File.ReadAllText(file);

            ClubDataHolder data = JsonUtility.FromJson<ClubDataHolder>(dataAsJson);

            for (int i = 0; i < data.clubdata.Count; i++)
            {
                Debug.Log(data.clubdata[i].ToString());
            }

            foreach (ClubData clubData in data.clubdata)
            {
                dict.Add(clubData.club, clubData);
            }
            Debug.Log("ClubDictionary loaded");

        }
        else
        {
            Debug.Log("Could not load file and ClubData");

        }



        //Application.persistentDataPath + "/clubData.json");


        Debug.Log("Club dict size:" + dict.Count.ToString());
    }
    public static ClubData getClubData(Clubs club)
    {
        return dict[club];
    }

}

[System.Serializable]
public class ClubDataHolder
{
    public List<ClubData> clubdata;
}

[System.Serializable]

public class ClubData
{
    public Clubs club;
    public float angle;
    public float accuracy;
    public float powerLossRatio;
    public float constantPower;


    public override string ToString()
    {
        return "(" + club + ", " + angle + ", " + accuracy + ", " + powerLossRatio + ")";
    }
}
