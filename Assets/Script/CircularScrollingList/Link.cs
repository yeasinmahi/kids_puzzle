using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Futuristic+Technologies+LTD");
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.yeasin.hummingbird#details-reviews");
    }

    public void InApp()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ubisoft.assassinscreed.identity&rdid=com.ubisoft.assassinscreed.identity");
    }
}
