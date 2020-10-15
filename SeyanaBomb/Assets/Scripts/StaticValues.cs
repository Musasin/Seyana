using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticValues : MonoBehaviour
{
    public static int scaleScore;
    public static int shakeScore;
    public static int clickScore;
    
    public static bool isClearEndingA;
    public static bool isClearEndingB;
    public static bool isClearEndingC;
    public static bool isClearEndingD;
    public static bool isClearEndingE;
    public static bool isClearEndingF;
    public static bool isClearEndingG;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void Load()
    {
        isClearEndingA = PlayerPrefs.HasKey("EndingA");
        isClearEndingB = PlayerPrefs.HasKey("EndingB");
        isClearEndingC = PlayerPrefs.HasKey("EndingC");
        isClearEndingD = PlayerPrefs.HasKey("EndingD");
        isClearEndingE = PlayerPrefs.HasKey("EndingE");
        isClearEndingF = PlayerPrefs.HasKey("EndingF");
        isClearEndingG = PlayerPrefs.HasKey("EndingG");
    }

    public static int GetSumScore()
    {
        return 130000; // テスト用仮
        return scaleScore + shakeScore + clickScore;
    }

    public static int GetNowEnding()
    {
        if (GetSumScore() < 10000)
            return 0;
        if (GetSumScore() < 40000)
            return 1; // バンド結成エンド
        else if (GetSumScore() < 60000)
            return 2; // あなたの後ろにも亡エンド
        else if (GetSumScore() < 90000)
            return 3; // 商品展開エンド
        else if (GetSumScore() < 120000)
            return 4; // セヤナー侵略エンド
        else if (GetSumScore() < 140000)
            return 5; // 人類滅亡エンド
        else
            return 6; // セヤナーの惑星エンド
    }
    public static void SaveEnding()
    {
        if (GetSumScore() < 10000)
            PlayerPrefs.SetInt("EndingA", 1);
        if (GetSumScore() < 40000)
            PlayerPrefs.SetInt("EndingB", 1);
        else if (GetSumScore() < 60000)
            PlayerPrefs.SetInt("EndingC", 1);
        else if (GetSumScore() < 90000)
            PlayerPrefs.SetInt("EndingD", 1);
        else if (GetSumScore() < 120000)
            PlayerPrefs.SetInt("EndingE", 1);
        else if (GetSumScore() < 140000)
            PlayerPrefs.SetInt("EndingF", 1);
        else
            PlayerPrefs.SetInt("EndingG", 1);
    }
}