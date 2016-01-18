using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class HpCntrol : MonoBehaviour
{

    Slider _slider;
    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("HPSlider").GetComponent<Slider>();
    }

//    float _hp = 0;

    void Update()
    {

    }
}