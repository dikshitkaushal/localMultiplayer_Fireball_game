using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public Text p1score;
    public Text p2score;

    public int _p1score = 0;
    public int _p2score = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        player_logic.onplayerdeath += updateuiscore;
    }
    private void OnDisable()
    {
        player_logic.onplayerdeath -= updateuiscore;
    }
    void updateuiscore(int num)
    {
        if(num==1)
        {
            ++_p2score;
            p2score.text = "" + _p2score;
        }
        if(num==2)
        {
            ++_p1score;
            p1score.text = "" + _p1score;
        }
    }
}
