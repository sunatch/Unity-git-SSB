using UnityEngine;
using System.Collections;

public class cBatteryBlock : MonoBehaviour {

    public GameObject item1;		// プレハブ用変数.
    public GameObject BulletB;
    
    public const float INTERVAL = 5.0f;
    public float timer = INTERVAL;


    // 壊したら入る点数(初期値は10点)
    [SerializeField]
    private int ADD_SCORE = 10;
    private int ADD_MASTERY = 10;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Instantiate(BulletB, transform.position, Quaternion.identity);

            timer = INTERVAL;
        }
    }

    /** 衝突イベント*/
    void OnCollisionEnter(Collision col)
    {
        // 自分を削除する
        Destroy(gameObject);

        // スコアの加算
        CGameMan.me.addScore(ADD_SCORE);
        CGameMan.me.upMystery(ADD_MASTERY);

        int rnd = Random.Range(0, 100);
        if (rnd < 30)
        {
//            Instantiate(item1, transform.position, Quaternion.identity);       // プレハブ作成.
        }

    }
}
