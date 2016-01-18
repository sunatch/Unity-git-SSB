using UnityEngine;
using System.Collections;

public class CPlayer : MonoBehaviour {

    public float VEL = 40f;
    public GameObject prefBall = null;
    GameObject insBall = null;

    private bool shotflag;
    private Vector2 worldPoint;

    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

    

    // Use this for initialization
    void Start () {
        GameObject.Find("GameManager").SendMessage("initGame");
        createHoldBall();
    }
	
	// Update is called once per frame
	void Update () {

/*        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
            GetComponent<Rigidbody>().velocity = worldPoint;
        }
*/

        // Vector3でマウス位置座標を取得する
        position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);

        screenToWorldPointPosition.y = -30f;
        // ワールド座標に変換されたマウス座標を代入
        gameObject.transform.position = screenToWorldPointPosition;


    }
        
    // ボールを生成して、プレイヤーバーにくっつける
    void createHoldBall()
    {
        /*
        Vector3 bpos = transform.position;
        bpos.y += (GetComponent<Collider>().bounds.size.y + prefBall.transform.localScale.y) / 2f;
        insBall = (GameObject)Instantiate(prefBall, bpos, Quaternion.identity);
        insBall.transform.parent = transform;
        */
        insBall = (GameObject)Instantiate(prefBall, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
//        insBall.transform.position = transform;
        // ボールの物理処理を無効にする
        insBall.GetComponent<Rigidbody>().isKinematic = true;

        if (insBall != null)
        {
            shotflag = true;
            GameObject.Find("GameManager").SendMessage("setReady");
        }
    }

    //Key入力判定　左右
    void InputKey(int key)
    {
        Vector3 vel = Vector3.zero;
        vel.x = VEL * key;
        GetComponent<Rigidbody>().velocity = vel;
    }


    //ボール発射準備
    void setBall()
    {
        if (shotflag == true)
        {
            insBall.GetComponent<Rigidbody>().isKinematic = false;
            insBall.SendMessage("shotBall");
            insBall = null;
        }
        shotflag = false;
    }

    //パーティクル（エフェクト）との当たり判定
    void OnParticleCollision(GameObject obj)
    {
        
    }

    //敵の弾に当たった時
    private void hitBullet()
    {        
        if (CGameMan.me.LifeDown())
        {
            CGameMan.me.changePauseMode(1);//ほんとは3？？？
            Application.LoadLevel("Result");
        }
    }

    //アイテム効果
    private void getItem01()
    {
        GameObject.Find("GameManager").SendMessage("LifeUp");
        StartCoroutine("item01");
    }
    IEnumerator item01()
    {
        transform.localScale += Vector3.right;     // Y方向にサイズを＋１する.
        yield return new WaitForSeconds(10.0f); // 10秒間、処理を待機.
        transform.localScale -= Vector3.right;     // Y方向にサイズを－１する.
    }

    private void getDamageItem()
    {
        GameObject.Find("GameManager").SendMessage("LifeDown");
    }

}
