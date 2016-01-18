using UnityEngine;
using System.Collections;

public class CBullet02 : MonoBehaviour {

    private Vector3 target;
    private GameObject player;

    private GameObject enemy;
    private Vector3 me;

    static public float rad;
    static int count = 0;

	// Use this for initialization
	void Start () {

        if (count == 0)
        {
            rad = GetAim();
        }
        else
        {
//            rad += 7.3f;
        }
        Vector2 vel;
        vel.x = -20 * Mathf.Cos(rad * Mathf.PI / 180f);
        vel.y = -20 * Mathf.Sin(rad * Mathf.PI / 180f);
        GetComponent<Rigidbody>().velocity = vel;
        count++;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerB")
        {       // 衝突した相手のタグ名がplayerなら
            other.transform.root.SendMessage("hitBullet");
            Destroy(gameObject);                    // 自分を削除.
        }

        if (other.gameObject.tag == "Out" || other.gameObject.tag == "Ball")
        {
            Destroy(gameObject);
        }
    }

    float GetAim()
    {
        player = GameObject.FindWithTag("Player");
        target = player.transform.position;

        enemy = GameObject.FindWithTag("Enemy");
        me = enemy.transform.position;

        float dx = me.x - target.x;
        float dy = me.y - target.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    void initCount()
    {
        count = 0;
    }

}
