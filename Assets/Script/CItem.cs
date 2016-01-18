using UnityEngine;
using System.Collections;

public class CItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up * 90 * Time.deltaTime);     // Y方向に１秒で90度回転させる.
        
        if (transform.position.y < -300.0f)
        {           // Y位置が-1未満になったら
            Destroy(gameObject);                    // 自分を削除.
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {       // 衝突した相手のタグ名がplayerなら
            other.transform.root.SendMessage("getItem01");
            Destroy(gameObject);                    // 自分を削除.
            CGameMan.me.addScore(1000);
        }
    }
}

