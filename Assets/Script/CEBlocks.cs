using UnityEngine;
using System.Collections;

public class CEBlocks : MonoBehaviour {

    public GameObject prefab_block01 = null;
    public GameObject prefab_block02 = null;
    public GameObject prefab_block03 = null;
    public GameObject prefab_Exblock01 = null;
    public GameObject prefab_Exblock02 = null;
    public GameObject prefab_Exblock03 = null;

    public GameObject prefab_enemy01 = null;
    public GameObject prefab_enemy02 = null;
    public GameObject prefab_enemy03 = null;

    private int[,] blocks = new int[5,6];
    private int[,] blocksEx = new int[6, 7];

    private int storyID;

    
    // Use this for initialization
    void Start()
    {
        storyID = CGameMan.me.getStroyID();

        switch(storyID) {
            case 1:
                GameObject enemy01 = Instantiate(prefab_enemy01) as GameObject;
                enemy01.transform.position = new Vector3( 0.0f , 40.0f , 0.0f);
            break;

            case 2:
                GameObject enemy02 = Instantiate(prefab_enemy02) as GameObject;
                enemy02.transform.position = new Vector3( 0.0f , 40.0f , 0.0f);
            break;

            default:
                GameObject enemy03 = Instantiate(prefab_enemy01) as GameObject;
                enemy03.transform.position = new Vector3( 0.0f , 40.0f , 0.0f);
            break;

        }

        blocks = CGameMan.me.getStageInfo(storyID);

        if (storyID <= 5)
        {

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j += 1)
                {
                    if (blocks[i, j] == 1)
                    {
                        createBlock(i, j, 1);
                    }

                    else if (blocks[i, j] == 2)
                    {
                        createBlock(i, j, 2);
                    }
                    else if (blocks[i, j] == 0)
                    {

                    }
                }
            }
        }

        else
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j += 1)
                {
                    if (blocks[i, j] == 1)
                    {
                        createBlockEx(i, j, 1);
                    }
                    else if (blocks[i, j] == 0)
                    {

                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
    

    // ブロック生成
    void createBlock(int i, int j, int num)
    {
        if (num == 1)
        {
            GameObject go = Instantiate(prefab_block01) as GameObject;
            go.transform.position = new Vector3(-20.0f + (10.0f * i), 35.0f - j * 3.5f, 0.0f);
        }
        else if (num == 2)
        {
            GameObject go = Instantiate(prefab_block02) as GameObject;
            go.transform.position = new Vector3(-20.0f + (10.0f * i), 35.0f - j * 3.5f, 0.0f);
        }
    }

    // ブロック生成
    void createBlockEx(int i, int j, int num)
    {
        if (num == 1)
        {
            GameObject go = Instantiate(prefab_Exblock01) as GameObject;
            go.transform.position = new Vector3(-20.0f + (8.0f * i), 35.0f - j * 3.5f, 0.0f);
        }
        else if (num == 2)
        {
            GameObject go = Instantiate(prefab_Exblock02) as GameObject;
            go.transform.position = new Vector3(-20.0f + (8.0f * i), 35.0f - j * 3.5f, 0.0f);
        }
    }


    // 邪魔ブロック生成
    void createDamageBlock()
    {
            int width = Random.Range(0, 6);
            int height = Random.Range(-2, -1);

//            GameObject db = Instantiate(prefdamageblock) as GameObject;
//            db.transform.position = new Vector3(16.0f + (6 * width), 20.0f - height * 3, 0.0f);
    }


}

