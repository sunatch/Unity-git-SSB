using UnityEngine;
using System.Collections;

public class CBall : MonoBehaviour {

    public float INIT_DIRECTION = 0.0f;
    public float INIT_SPEED = 10.0f;

    public GameObject justprefab;

    private bool justflag;
    private bool justTouchflag;

    private GameObject Particle;

    // Use this for initialization
    void Start () {
    }
    


	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            justTouchflag = true;
        }
        else
            justTouchflag = false;

	}

    void OnCollisionEnter(Collision col)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 v = rb.velocity;	// 速度を取得.

        if (-1.0f < v.x && v.x <= 0.0f)
        {			// Xの速度が-3～0なら.
            v.x = -1.0f;							// Xの値を -3.0f に.
        }
        else if (0.0f < v.x && v.x < 1.0f)
        {	// Xの速度が0～3なら).
            v.x = 1.0f;							// Xの値を +3.0f に.
        }

        if (-10.0f < v.y && v.y <= 0.0f)
        {		// Yの速度が-10～0なら.
            v.y = -10.0f;							// Yの値を -10.0f に.
        }
        else if (0.0f < v.y && v.y < 10.0f)
        {	// Yの速度が0～10なら).
            v.y = 10.0f;							// Yの値を +10.0f に.
        }

        v.Normalize();		// ボールの速度を一旦１に戻す.
        v *= 30.0f;			// 速度をn倍にする.

        rb.velocity = v;	// 値を反映.

        if (col.gameObject.tag == "Block")
        {
            justflag = false;
            setJustAttack();
        }
    }

    /** ボールを発射する*/
    void shotBall()
    {
        Vector2 vel = Vector2.zero;
        vel.x = INIT_SPEED * Mathf.Cos(INIT_DIRECTION * Mathf.PI / 180f);
        vel.y = -INIT_SPEED * Mathf.Sin(INIT_DIRECTION * Mathf.PI / 180f);
        GetComponent<Rigidbody>().velocity = vel;
    }

    void controlBall()
    {
        Rigidbody rd = GetComponent<Rigidbody>();
        rd.velocity = rd.velocity.normalized * 10;

        if(Mathf.Abs(rd.velocity.y) < 3) {
            GetComponent<Rigidbody>().velocity = rd.velocity * 3;
        }

        if (Mathf.Abs(rd.velocity.x) < 3)
        {
            GetComponent<Rigidbody>().velocity = rd.velocity * 3;
        }

  }

    void setJustAttack()
    {
        if (justflag == true && GameObject.FindGameObjectsWithTag("JustParticle").Length <= 1)
        {
        
            Particle = (GameObject)Instantiate(justprefab, transform.position, Quaternion.identity);
            Particle.transform.parent = transform;
            //Destroy(Particle, 4.0f);
        }

        if (justflag == false)
        {
            Destroy(Particle);
        }
    }

    /* 判定 */
    void OnTriggerEnter(Collider other)
    {

        /* 攻撃判定 */
        if (other.CompareTag("Just") && justTouchflag == true)
        {
                justflag = true;
                setJustAttack();
        }


        /* ミス判定 */
        if (other.CompareTag("Miss"))
        {
            if (CGameMan.me.decLeft())
            {
                CGameMan.me.changePauseMode(1);//ほんとは3？？？
                Application.LoadLevel("Result");
            }
            else
            {
                Destroy(gameObject);
                GameObject.Find("Player").SendMessage("createHoldBall");
            }
        }
    }

}
