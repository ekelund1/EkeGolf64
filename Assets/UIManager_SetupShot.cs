using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_SetupShot : MonoBehaviour
{
    public Texture clubDisplay;
    public PlayerBoxControl player;
    public HoleScript courseHole;
    // Start is called before the first frame update
    private Dictionary<Clubs, Texture> clubTextures = new Dictionary<Clubs, Texture>();
    public Texture getClubTextures(Clubs club)
    {
        return clubTextures[club];
    }
    private List<Texture> tempArray;

    public SwingManager swingManager;

    void Start()
    {
        foreach (Clubs club in (Clubs[])Clubs.GetValues(typeof(Clubs)))
        {
            Texture temp = Resources.Load("Clubs/" + club.ToString()) as Texture;
            clubTextures.Add(club, temp);
        }
    }
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(20, 800, 75, 75), clubTextures[player.selectedClub], ScaleMode.StretchToFill);
        GUI.Box(new Rect(20, 875, 75, 20), player.selectedClub.ToString());

        ClubData club = ClubDictionary.getClubData(player.selectedClub);

        GUI.Box(new Rect(20, 910, 75, 20), (BallTest.calculateForce(club, 20f, 1).magnitude * 5).ToString());


        float distance = Mathf.Round(Vector2.Distance(new Vector2(courseHole.transform.position.x, courseHole.transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z)));
        GUI.Box(new Rect(20, 200, 75, 20), distance.ToString() + "units");
        DrawSwing();
    }

    private void DrawSwing()
    {

        GUI.Box(new Rect(1100, 900, 10, 2), "");
        GUI.Box(new Rect(300, 900, 10, 2), "");

        float indicatorPosX = 1100 - 800 * swingManager.currentPower;

        float swingTimer = swingManager.swingTimer;
        if (swingManager.swingTimer > 1)
            swingTimer = 2 - swingManager.swingTimer;

        float timerPosX = 1100 - 800 * swingTimer;


        GUI.Box(new Rect(indicatorPosX, 900, 10, 2), "");
        GUI.Box(new Rect(timerPosX, 900, 10, 2), "");


        GUI.Box(new Rect(1100, 930, 150, 20), "Power: " + Mathf.Round(swingManager.currentPower * 1000).ToString() + ", accuracy:" + Mathf.Round(swingManager.currentAccuracy * 1000).ToString());

    }
}
