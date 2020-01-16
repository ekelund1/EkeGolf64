using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_SetupShot : MonoBehaviour
{
    public Texture clubDisplay;
    public PlayerBoxControl player;
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
            //clubTextures.Add(club, temp);
            // tempArray.Add(Resources.Load(club.ToString()) as Texture);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(20, 800, 75, 75), clubTextures[player.selectedClub], ScaleMode.StretchToFill);

        GUI.Box(new Rect(20, 875, 75, 20), player.selectedClub.ToString());


        // tempArray.ForEach(t =>
        // {
        //     GUI.Box(new Rect(500, 10, t.height, 100), t.name);
        // });
    }
}
