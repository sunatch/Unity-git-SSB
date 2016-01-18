using UnityEngine;
using System.Collections;

public class CBulletB : MonoBehaviour {

    private Vector3 target;
    private GameObject player;

    private GameObject enemy;
    private Vector3 me;

    static public float rad;

	// Use this for initialization
	void Start () {

        rad = GetAim();

        Vector2 vel;
        vel.x = -30 * Mathf.Cos(rad * Mathf.PI / 180f);
        vel.y = -30 * Mathf.Sin(rad * Mathf.PI / 180f);
        GetComponent<Rigidbody>().velocity = vel;

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


}
