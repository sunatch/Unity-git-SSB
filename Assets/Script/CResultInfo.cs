using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Novel;

public class CResultInfo : MonoBehaviour
{

    private Text resultScore;
    private int score;

    private int nextStoryID;

    private int WIN_LOSE;

    // Use this for initialization
    void Start()
    {

        nextStoryID = CGameMan.me.getStroyID();

        resultScore = GameObject.Find("TextScore").GetComponent<Text>();
        score = CGameMan.me.getScore();
        resultScore.text = score.ToString();
        CGameMan.me.changeStroyID();

        WIN_LOSE = CGameMan.me.getPauseMode();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {

        switch (nextStoryID)
        {
            case 1:

                NovelSingleton.StatusManager.callJoker("tall/scene2", "");
                break;

            case 2:
                NovelSingleton.StatusManager.callJoker("tall/scene3", "");
                break;

            case 3:
                NovelSingleton.StatusManager.callJoker("tall/scene4", "");
                break;

            case 4:
                NovelSingleton.StatusManager.callJoker("tall/scene5", "");
                break;

            
            default:

                break;

        }
    }
}
