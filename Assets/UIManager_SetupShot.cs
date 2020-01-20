using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_SetupShot : MonoBehaviour
{
    public Texture clubDisplay;
    public PlayerBoxControl player;
    public GameObject courseHole;
    // Start is called before the first frame update
    private Dictionary<Clubs, Texture> clubTextures = new Dictionary<Clubs, Texture>();
    public Texture getClubTextures(Clubs club)
    {
        return clubTextures[club];
    }
    private List<Texture> tempArray;

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

        GUI.Box(new Rect(20, 910, 75, 20), (BallTest.calculateForce(club, 20f).magnitude * 5).ToString());

        float distance = Mathf.Round(Vector2.Distance(new Vector2(courseHole.transform.position.x, courseHole.transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z)));
        GUI.Box(new Rect(20, 200, 75, 20), distance.ToString() + "units");



    }
}
