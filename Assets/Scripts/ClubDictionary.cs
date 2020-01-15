using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Clubs
{
    PUTTER, SW, PW, I9, I8, I7, I6, I5, I4, I3, I2, W4, W3, W1, 
}
public class ClubDictionary : MonoBehaviour
{
    private Dictionary<Clubs, ClubData> dict;

    public ClubData getClubData(Clubs club)
    {
        return dict[club];
    }
    ClubDictionary()
    {
        foreach (Clubs club in (Clubs[])Clubs.GetValues(typeof(Clubs)))
        {
            dict.Add(club, new ClubData(club, 1f, 1f, 1f));
        }
    }
}

public class ClubData
{
    public Clubs club { get; }
    public float angle { get; }
    public float accuracy { get; }
    public float powerLossRatio { get; }
    public float constantPower { get; }

    public ClubData(Clubs _club, float _angle, float _accuracy, float _powerLossRatio, float _constantPower = 0)
    {
        club = _club;
        angle = _angle;
        accuracy = _accuracy;
        powerLossRatio = _powerLossRatio;
        constantPower = _constantPower;
    }
}