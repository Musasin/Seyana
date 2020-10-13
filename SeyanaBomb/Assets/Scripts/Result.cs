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

    // Start is called before the first frame update
    void Start()
    {
        StaticValues.SaveEnding();
        StaticValues.Load();
        GameObject.Find("ScaleScore").GetComponent<Text>().text = StaticValues.scaleScore.ToString();
        GameObject.Find("ShakeScore").GetComponent<Text>().text = StaticValues.clickScore.ToString();
        GameObject.Find("ClickScore").GetComponent<Text>().text = StaticValues.shakeScore.ToString();
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
                SelifInstantiate(selifRight, "ここのセリフも考え中やで", false);
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
            WWW.EscapeURL("トータルスコア" + StaticValues.GetSumScore() + "クリア！ 文言は考え中！ #ボイゲジャム #セヤナー爆弾\nhttps://musasin.itch.io/seyana-bomb");
#if UNITY_EDITOR
        Application.OpenURL(url);
#elif UNITY_WEBGL
            Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
#else
            Application.OpenURL(url);
#endif
    }
}
