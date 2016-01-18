using UnityEngine;
using System.Collections;

public class CBlockRock : MonoBehaviour {

    public GameObject item1;		// プレハブ用変数.
    int BlockHP;


    // 壊したら入る点数(初期値は10点)
    [SerializeField]
    private int ADD_SCORE = 10;
    private int ADD_MASTERY = 5;

    // Use this for initialization
    void Start()
    {
        BlockHP = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /** 衝突イベント*/
    void OnCollisionEnter(Collision col)
    {
        if (BlockHP > 1)
        {
            BlockHP--;
        }
        else
        {
            // 自分を削除する
            Destroy(gameObject);

            // スコアの加算
            CGameMan.me.addScore(ADD_SCORE);
            CGameMan.me.upMystery(ADD_MASTERY);


            int rnd = Random.Range(0, 100);
            if (rnd < 30)
            {
                Instantiate(item1, transform.position, Quaternion.identity);       // プレハブ作成.
            }
        }
    }
}
