using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CGameMan : MonoBehaviour {

    //ゲームモード(1:Tokoton，2:Story)
    private int GAME_MODE;
    //ポーズモード(1:勝利，2:負け，3:ポーズ)
    private int PAUSE_MODE;
    //ストーリーナンバー
    private int STORY_ID = 1;


    // 点数
    private int iScore;
    // 現在の残機。マイナスになるとゲームオーバー。
    private int iLeft;
    // 最大体力
    private int iHP;
    private int MAX_HP = 100;

    private int iMystery;

    private int ienemyHP;
    // 面が開始する時点での残機(初期設定では3回ミスでゲームオーバー)
    [SerializeField]

    // TextScoreのインスタンス
    private Text textScore;
    // SliderLeftのインスタンス
    private Slider sliderLife;
    private Slider sliderMystery;
    private Slider sliderEneLife;
    // BonbCountのインスタンス
    private Text textBomb;

    private Text textReady;
    // 自分自身のインスタンス
    public static CGameMan me = null;

    //カウントダウン
    private string countdown = "";
    private bool showcount = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (countdown == "GO!!")
        {
            GameObject.Find("Player").SendMessage("setBall");
        }
    }

    // このオブジェクトを読み込み時に破壊させない
    void Awake()
    {
        // 1つより多かったらすでに追加済みなので、このGameManは削除する
        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        // 永続化
        DontDestroyOnLoad(this);
        // インスタンスを記録
        me = this;
    }

    //ゲームモード変更受け付け
    public void changeGameMode(int gm)
    {
        GAME_MODE = gm;
    }
    //ゲームモード渡し！
    public int getGameMode()
    {
        return GAME_MODE;
    }

    //ポーズモード変更受け付け
    public void changePauseMode(int pm)
    {
        PAUSE_MODE = pm;
    }
    //ポーズモード渡し！
    public int getPauseMode()
    {
        return PAUSE_MODE;
    }

    //ストーリーナンバー受け付け
    public void changeStroyID()
    {
        STORY_ID++;
    }

    //ストーリナンバー返し
    public int getStroyID()
    {
        return STORY_ID;
    }

    //スコアの加算
    public void addScore(int add)
    {
        iScore += add;
        textScore.text = iScore.ToString();
    }

    public int getScore()
    {
        return iScore;
    }

    /** 残機を減らす処理
	 * @return bool true=ゲームオーバー / false=ゲーム継続
	 */
    public bool decLeft()
    {
        // 残機があれば減らす
        iHP -= 30;
        // 残機がなければゲームオーバー
        if (iHP <= 0)
        {
            return true;
        }
        // 結果を表示
        sliderLife.value = iHP;
        return false;
    }

    public void upMystery(int mas)
    {
        iMystery += mas;
        if (iMystery > MAX_HP)
        {
            iMystery = MAX_HP;
        }
        sliderMystery.value = iMystery;
    }

    public void useMystery()
    {
        if (iMystery >= 100)
        {
            iMystery = 0;
        }
    }

    //HP回復
    public void LifeUp()
    {
        iHP += 10;
        if(iHP > MAX_HP)
        {
            iHP = MAX_HP;
        }
        sliderLife.value = iHP;
    }

    //HPダメージ
    public bool LifeDown()
    {
        iHP -= 10;
        if (iHP <= 0)
        {
            iHP = 0;
        }
        if (iHP <= 0)
        {
            return true;
        }
        // 結果を表示
        sliderLife.value = iHP;
        return false;
        
    }

    //敵のHPダメージ
    public void downEnemyLife()
    {
        ienemyHP -= 25;
        if (ienemyHP <= 0)
        {
            ienemyHP = 0;
        }
        sliderEneLife.value = ienemyHP;
    }

    public int getEneHP()
    {
        return ienemyHP;
    }

    void setReady()
    {
        StartCoroutine("getReady");
    }

    IEnumerator getReady()
    {
        showcount = true;

        countdown = "Ready…";
        yield return new WaitForSeconds(1.5f);

        countdown = "GO!!";
        yield return new WaitForSeconds(1.0f);

        countdown = "";
        yield return new WaitForSeconds(0.5f);
        showcount = false;
             
    }

    // ゲーム開始の初期化
    public void initGame()
    {
        iScore = 0;
        iHP = 100;
        iMystery = 0;
        ienemyHP = 100;
        
        // ゲームオブジェクトを探す
        textScore = GameObject.Find("TextScore").GetComponent<Text>();
        sliderLife = GameObject.Find("HPSlider").GetComponent<Slider>();
        textReady = GameObject.Find("TextReady").GetComponent<Text>();
        sliderMystery = GameObject.Find("MysterySlider").GetComponent<Slider>();
        sliderEneLife = GameObject.Find("EnemySlider").GetComponent<Slider>();
        // 初期値を表示
        textScore.text = iScore.ToString();
        sliderLife.value = iHP;
        sliderMystery.value = iMystery;
        sliderEneLife.value = ienemyHP;
    }
    

    void OnGUI()
    {
        if (showcount)
        {
            textReady.text = countdown;
        }
    }
//ステージ ←側　上　→側　下
/****************************************************************************************/
    private int[,] stage01 = new int[5, 6] { { 0, 0, 1, 1, 0, 0},
                                             { 0, 1, 1, 0, 1, 0},
                                             { 0, 1, 0, 1, 0, 0},
                                             { 0, 1, 1, 0, 1, 0},
                                             { 0, 0, 1, 1, 0, 0} };

    private int[,] stage02 = new int[5, 6] { { 1, 1, 1, 1, 1, 0},
                                             { 1, 0, 0, 2, 0, 0},
                                             { 1, 2, 0, 2, 1, 0},
                                             { 1, 0, 0, 2, 0, 0},
                                             { 1, 1, 1, 1, 1, 0} };

    private int[,] stage03 = new int[5, 6] { { 1, 0, 1, 1, 0, 1},
                                             { 1, 0, 1, 1, 0, 1},
                                             { 1, 0, 1, 1, 0, 1},
                                             { 1, 0, 1, 1, 0, 1},
                                             { 1, 0, 1, 1, 0, 1} };

    private int[,] stage04 = new int[5, 6] { { 1, 1, 1, 1, 1, 1},
                                             { 1, 1, 1, 1, 1, 1},
                                             { 0, 0, 0, 0, 1, 1},
                                             { 1, 1, 1, 1, 1, 1},
                                             { 1, 1, 1, 1, 1, 1} };

    private int[,] stage05 = new int[5, 6] { { 0, 1, 1, 1, 1, 1},
                                             { 1, 0, 0, 0, 0, 1},
                                             { 1, 1, 1, 1, 1, 0},
                                             { 1, 0, 0, 0, 0, 1},
                                             { 0, 1, 1, 1, 1, 1} };

    private int[,] stageEx01 = new int[6, 7] { 
                                             { 0, 0, 1, 1, 1, 1, 1},
                                             { 0, 0, 2, 1, 2, 0, 1},
                                             { 0, 0, 2, 0, 2, 1, 0},
                                             { 0, 0, 2, 0, 2, 1, 0},
                                             { 0, 0, 2, 1, 2, 0, 1},
                                             { 0, 0, 1, 1, 1, 1, 1} };

    private int[,] stageEx02 = new int[6, 7] {
                                             { 0, 1, 1, 1, 1, 1, 1},
                                             { 1, 1, 1, 0, 0, 0, 1},
                                             { 0, 1, 1, 0, 2, 2, 0},
                                             { 0, 1, 1, 0, 2, 2, 0},
                                             { 1, 1, 1, 0, 0, 0, 1},
                                             { 0, 1, 1, 1, 1, 1, 1} };

    private int[,] stageEx03 = new int[6, 7] {
                                             { 2, 0, 2, 2, 0, 2, 2},
                                             { 0, 1, 0, 2, 1, 2, 0},
                                             { 2, 1, 2, 0, 1, 2, 0},
                                             { 2, 1, 2, 0, 1, 2, 0},
                                             { 0, 1, 0, 2, 1, 2, 0},
                                             { 2, 0, 2, 2, 0, 2, 2} };

    private int[,] stageEx04 = new int[6, 7] { 
                                             { 0, 0, 1, 1, 1, 1, 1},
                                             { 1, 0, 1, 1, 1, 0, 1},
                                             { 1, 0, 1, 0, 1, 1, 0},
                                             { 1, 0, 1, 0, 1, 1, 0},
                                             { 1, 0, 1, 1, 1, 0, 1},
                                             { 0, 0, 1, 1, 1, 1, 1} };

    private int[,] stageEx05 = new int[6, 7] {                                             
                                             { 0, 0, 1, 1, 1, 1, 1},
                                             { 1, 0, 1, 1, 1, 0, 1},
                                             { 1, 0, 1, 0, 1, 1, 0},
                                             { 1, 0, 1, 0, 1, 1, 0},
                                             { 1, 0, 1, 1, 1, 0, 1},
                                             { 0, 0, 1, 1, 1, 1, 1} };


    public int[,] getStageInfo(int stage)
    {

        switch (stage)
        {
            case 1:
                return stage01;
                break;

            case 2:
                return stage02;
                break;

            case 3:
                return stage03;
                break;

            case 4:
                return stage04;
                break;

            case 5:
                return stage05;
                break;

            default:
                return stageEx01;
                break;
        }

    }
/*******************************************************************************/


}
