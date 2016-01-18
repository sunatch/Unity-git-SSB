using UnityEngine;
using System.Collections;

public class CBlock : MonoBehaviour
{
    public GameObject item1;		// プレハブ用変数.
    static int iBlockCnt = 0;


    // 壊したら入る点数(初期値は10点)
    [SerializeField]
    private int ADD_SCORE = 10;
    private int ADD_MASTERY = 5;

    // Use this for initialization
    void Start () {
        iBlockCnt++;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /** 衝突イベント*/
    void OnCollisionEnter(Collision col)
    {
        // 自分を削除する
        Destroy(gameObject);

        // スコアの加算
        CGameMan.me.addScore(ADD_SCORE);
        CGameMan.me.upMystery(ADD_MASTERY);
//        GameObject.Find("Ball(Clone)").SendMessage("controlBall"); //ボール制御


        int rnd = Random.Range(0, 100);
        if (rnd < 30 )
        {
            Instantiate(item1, transform.position, Quaternion.identity);       // プレハブ作成.
        }

        // ブロックを減らして、全滅したかを確認
        iBlockCnt--;
        if (iBlockCnt <= 0)
        {
//            CGameMan.me.changePauseMode(1);
//            Application.LoadLevel("Result");
           
            //Application.LoadLevel("Game");
        }
    }
}
