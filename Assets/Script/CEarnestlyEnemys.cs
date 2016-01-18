using UnityEngine;
using System.Collections;

public class CEarnestlyEnemys : MonoBehaviour {

    public GameObject Bullet01;
    public GameObject Bullet02;


    public const float INTERVAL1 = 2.0f;
    public const float INTERVAL2 = 4.0f;
    public const float INTERVAL3 = 6.0f;
    public const float INTERVAL4 = 8.0f;
    public const float INTERVAL5 = 10.0f;
    public float timer = INTERVAL1;


    private int storyID;

    // Use this for initialization
    void Start()
    {
        storyID = CGameMan.me.getStroyID();
    }

    // Update is called once per frame
    void Update()
    {

        switch (storyID)
        {
            case 1:

                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    StartCoroutine(shotBullet(2, 20));
                    timer = INTERVAL5;
                    
                }

                break;

            case 2:

                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    StartCoroutine(shotBullet(1, 60));
//                    StartCoroutine(shotBullet(2, 5));

                    timer = INTERVAL1;
                }

                break;

            default:
                break;
        }

    }


    void OnCollisionEnter(Collision other)
    {
        

            GameObject.Find("GameManager").SendMessage("downEnemyLife");

            switch (storyID)
            {
                case 1:
                    int rand = Random.Range(5, 10);

                    for (int i = 0; i < rand; i++)
                    {
                        Instantiate(Bullet01, transform.position, Quaternion.identity);
                    }
                    break;

                case 2:

                    int rand2 = Random.Range(5, 10);

                    for (int i = 0; i < rand2; i++)
                    {
                        Instantiate(Bullet01, transform.position, Quaternion.identity);
                    }

                    break;

                default:
                    break;

            }
            if (CGameMan.me.getEneHP() <= 0)
            {
                CGameMan.me.changePauseMode(1);
                CGameMan.me.changeStroyID();
                Application.LoadLevel("Earnestly");
                
            }
        
    }

    IEnumerator shotBullet(int pattern, int count)
    {
        switch(pattern) {

            case 1:
                for (int i = 0; i < count; i++)
                {
                    Instantiate(Bullet01, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(0.1f); // 秒間、処理を待機.
                }
                GameObject.Find("Bullet01(Clone)").SendMessage("initCount");
                break;

            case 2:
                for (int i = 0; i < count; i++)
                {
                    Instantiate(Bullet02, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(0.1f); // 秒間、処理を待機.
                    if (i % 4 == 0)
                    {
                        yield return new WaitForSeconds(1.0f); // 秒間、処理を待機.
                        GameObject.Find("Bullet04(Clone)").SendMessage("initCount");
                    }
                }
                GameObject.Find("Bullet04(Clone)").SendMessage("initCount");
                break;

        }
    }
}
