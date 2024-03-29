﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public GameObject selifLeft;
    public GameObject selifRight;
    int index;
    float talkTime;
    GameObject nowSelifObject;

    string tweetText;
    string tweetEndingTitle;

    // Start is called before the first frame update
    void Start()
    {
        StaticValues.SaveEnding();
        StaticValues.Load();
        GameObject.Find("ScaleScore").GetComponent<Text>().text = StaticValues.scaleScore.ToString();
        GameObject.Find("ShakeScore").GetComponent<Text>().text = StaticValues.shakeScore.ToString();
        GameObject.Find("ClickScore").GetComponent<Text>().text = StaticValues.clickScore.ToString();
        GameObject.Find("SumScore").GetComponent<Text>().text = StaticValues.GetSumScore().ToString();
        
        GameObject.Find("EndingListText").GetComponent<Text>().text = "";
        GameObject.Find("EndingListText").GetComponent<Text>().text += StaticValues.isClearEndingA ? "A. 不発\n" : "A. ？？？\n";
        GameObject.Find("EndingListText").GetComponent<Text>().text += StaticValues.isClearEndingB ? "B. バンド結成\n" : "B. ？？？\n";
        GameObject.Find("EndingListText").GetComponent<Text>().text += StaticValues.isClearEndingC ? "C. あなたの後ろにも\n" : "C. ？？？\n";
        GameObject.Find("EndingListText").GetComponent<Text>().text += StaticValues.isClearEndingD ? "D. 商品展開\n" : "D. ？？？\n";
        GameObject.Find("EndingListText").GetComponent<Text>().text += StaticValues.isClearEndingE ? "E. セヤナー侵略\n" : "E. ？？？\n";
        GameObject.Find("EndingListText").GetComponent<Text>().text += StaticValues.isClearEndingF ? "F. 人類滅亡\n" : "F. ？？？\n";
        GameObject.Find("EndingListText").GetComponent<Text>().text += StaticValues.isClearEndingG ? "G. セヤナーの星\n" : "G. ？？？\n";

        AudioManager.Instance.PlayBGM("Result", 0.2f);
        
        switch (StaticValues.GetNowEnding())
        {
            case 0:
                tweetText = "セヤナー爆弾は不発に終わり、";
                tweetEndingTitle = "A. 不発";
                break;
            case 1:
                tweetText = "セヤナーの可能性を感じてバンド結成！\n";
                tweetEndingTitle = "B. バンド結成";
                break;
            case 2:
                tweetText = "「もう、こんな所で寝ないでよお姉ちゃ...ん...?」\n";
                tweetEndingTitle = "C. あなたの後ろにも";
                break;
            case 3:
                tweetText = "セヤナー爆弾は一躍大ヒット！\n";
                tweetEndingTitle = "D. 商品展開";
                break;
            case 4:
                tweetText = "セヤナー軍との戦いは続く。\n";
                tweetEndingTitle = "E. セヤナー侵略";
                break;
            case 5:
                tweetText = "";
                tweetEndingTitle = "F. 人類滅亡";
                break;
            case 6:
                int rand = Random.Range(0, 10);
                switch (rand)
                {
                    case 1:
                        tweetText = "「ｳﾁﾅｰ」\n";
                        break;
                    case 2:
                        tweetText = "「ｱｵｲｰ」\n";
                        break;
                    case 3:
                        tweetText = "「ﾔｰ」\n";
                        break;
                    case 4:
                        tweetText = "「ｴﾋﾞﾌﾗｲｰ」\n";
                        break;
                    case 5:
                        tweetText = "「ﾅﾝﾃﾞｰ?」\n";
                        break;
                    case 6:
                        tweetText = "「ｾﾔｾﾔ」\n";
                        break;
                    case 7:
                    case 8:
                        tweetText = "「ﾔﾃﾞｰ」\n";
                        break;
                    case 0:
                    case 9:
                    default:
                        tweetText = "「ｾﾔﾅｰ」\n";
                        break;

                }
                tweetEndingTitle = "G. セヤナーの星";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        talkTime += Time.deltaTime;

        if (talkTime < 3)
            return;

        switch (index)
        {
            case 0:
                index++;
                SelifInstantiate(selifLeft, "エンディングは\n全部で7種類！", false);
                talkTime = 0;
                break;
            case 1:
                index++;
                SelifInstantiate(selifLeft, "良かったら色々\n試してみてね！", false);
                talkTime = 0; 
                break;
            case 2:
                index++;
                SelifInstantiate(selifRight, "アルファベットの\n並びはスコア順に\nなってるらしいで", false);
                talkTime = 0;
                break;
            case 3:
                index++;
                SelifInstantiate(selifLeft, "最大スコアの\n理論値は\n170000だよ！", false);
                talkTime = 0;
                break;
            case 4:
                index = 0;
                SelifInstantiate(selifRight, "本当に出せるか\nどうかは知らんで", false);
                talkTime = 0;
                break;
        }
    }

    
    private void SelifInstantiate(GameObject selifObject, string text, bool playSE = true, bool isFirst = false)
    {
        if (!isFirst)
            Destroy(nowSelifObject);
        nowSelifObject = Instantiate(selifObject, transform);
        nowSelifObject.GetComponentInChildren<Text>().text = text;
        if (playSE)
            AudioManager.Instance.PlaySE("pon", 0.2f);
    }

    public void PlaySE(string seName)
    {
        AudioManager.Instance.PlaySE(seName);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Tweet()
    {
        string url = "https://twitter.com/intent/tweet?text=" +
            WWW.EscapeURL(tweetText + "トータルスコア " + StaticValues.GetSumScore() + " でエンディング\"" + tweetEndingTitle + "\"をクリアしました！\n#セヤナー爆弾\nhttps://play.google.com/store/apps/details?id=com.musasin.seyanabomb");
#if UNITY_EDITOR
        Application.OpenURL(url);
#elif UNITY_WEBGL
            Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
#else
            Application.OpenURL(url);
#endif
    }
}
