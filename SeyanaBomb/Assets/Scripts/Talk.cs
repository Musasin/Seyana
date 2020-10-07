﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    public GameObject selifLeft;
    public GameObject selifRight;
    public GameObject orderText;
    public GameObject timerText;
    public GameObject barObject;
    GameObject nowSelifObject, nowOrderText;
    int index = 0;
    public enum State { TALK_A, CONNECT, TALK_B, GRIP, SHAKE, TALK_C, LIGHT, TALK_D };
    State state;
    int viewedTime;
    float time, talkTime;
    float maxScale = 0.5f;
    GameObject maxSeyanaObject = null;
    GameObject redBar, blueBar;
    float redPower, bluePower;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("Talk", 0.1f);
        state = State.TALK_A;
        SelifInstantiate(selifLeft, "セヤナー爆弾が\nできたよお姉ちゃん！", true, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.TALK_A)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (index)
                {
                    case 0:
                        index++;
                        SelifInstantiate(selifRight, "セヤナー爆弾？！");
                        break;
                    case 1:
                        index++;
                        SelifInstantiate(selifLeft, "セヤナーをつなげて\nよく振って\n刺激すると、");
                        break;
                    case 2:
                        index++;
                        SelifInstantiate(selifLeft, "激しい光とともに\n爆散するんだよ！");
                        break;
                    case 3:
                        index++;
                        SelifInstantiate(selifRight, "激しい光とともに爆散？！");
                        break;
                    case 4:
                        index++;
                        SelifInstantiate(selifLeft, "さっそく\nやってみよう！");
                        break;
                    case 5:
                        index++;
                        SelifInstantiate(selifRight, "なんで？！");
                        break;
                    case 6:
                        index++;
                        nowOrderText = Instantiate(orderText, transform);
                        nowOrderText.GetComponentInChildren<Text>().text = "セヤナーを\nつなげて集めろ！";

                        SelifInstantiate(selifRight, "まってまって！");
                        break;
                    case 7:
                        index = 0;
                        time = 0;
                        state = State.CONNECT;
                        AudioManager.Instance.PlayBGM("Connect", 0.1f);
                        AudioManager.Instance.PlaySE("pii");
                        break;
                }
            }
        }
        else if (state == State.CONNECT)
        {
            time += Time.deltaTime;
            talkTime += Time.deltaTime;

            if (5 - viewedTime > 5 - time)
            {
                GameObject timeObj = Instantiate(timerText, transform);
                timeObj.GetComponentInChildren<Text>().text = (5 - viewedTime).ToString();
                viewedTime++;
                
                if (viewedTime == 6)
                {
                    state = State.TALK_B;
                    index = 0;
                    AudioManager.Instance.PlaySE("pipi");
                    timeObj.GetComponentInChildren<Text>().text = "そこまで！";
                    time = 0;
                    viewedTime = 0;

                    switch (maxScale)
                    {
                        case 0.5f:
                            SelifInstantiate(selifLeft, "セヤナーを掴んで\n動かしてあげると\n大きくできるの！", false);
                            break;
                        case 1.0f:
                        case 1.5f:
                        case 2.0f:
                            SelifInstantiate(selifLeft, "小さいセヤナーも\nかわいいね！", false);
                            break;
                        case 2.5f:
                        case 3.0f:
                        case 3.5f:
                            SelifInstantiate(selifLeft, "大きくなったね！", false);
                            break;
                        case 4.0f:
                        case 4.5f:
                        case 5.0f:
                            SelifInstantiate(selifLeft, "いい調子だね！", false);
                            break;
                        case 5.5f:
                        case 6.0f:
                        case 6.5f:
                            SelifInstantiate(selifLeft, "わー、おっきい！\nさすがお姉ちゃん！", false);
                            break;
                        case 7.0f:
                            SelifInstantiate(selifLeft, "すごい！\n纏めきっちゃったね！", false);
                            break;
                    } 
                }
            }

            if (talkTime > 2)
            {
                switch (index)
                {
                    case 0:
                        index++;
                        SelifInstantiate(selifRight, "うわぁいっぱいおる", false);
                        talkTime = 0;
                        break;
                    case 1:
                        index++;
                        SelifInstantiate(selifRight, "どうすんのこれ", false);
                        talkTime = 0; 
                        break;
                }
            }

        }

        if (state == State.TALK_B)
        {
            time += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && time > 1.0f)
            {
                switch (index)
                {
                    case 0:
                        index++;
                        SelifInstantiate(selifRight, "せ、せやな...");
                        break;
                    case 1:
                        index++;
                        SelifInstantiate(selifLeft, "次は\nしっかり掴んで\n振り回すの！");
                        break;
                    case 2:
                        index++;
                        SelifInstantiate(selifLeft, "準備はいい？");
                        break;
                    case 3:
                        index++;
                        SelifInstantiate(selifRight, "掴めるんかコイツ");
                        break;
                    case 4:
                        index++;
                        Destroy(nowOrderText);
                        nowOrderText = Instantiate(orderText, transform);
                        nowOrderText.GetComponentInChildren<Text>().text = "セヤナーをつかめ！";
                        Instantiate(barObject, transform);
                        blueBar = GameObject.Find("BlueBar");
                        redBar = GameObject.Find("RedBar");

                        AudioManager.Instance.PlaySE("gan");
                        state = State.GRIP;
                        break;
                    case 5:
                        break;
                }
            }
        }


        if (state == State.SHAKE)
        {
            time += Time.deltaTime;
            talkTime += Time.deltaTime;

            if (5 - viewedTime > 5 - time)
            {
                GameObject timeObj = Instantiate(timerText, transform);
                timeObj.GetComponentInChildren<Text>().text = (5 - viewedTime).ToString();
                viewedTime++;

                if (viewedTime == 6)
                {
                    state = State.TALK_C;
                    index = 0;
                    AudioManager.Instance.PlaySE("pipi");
                    timeObj.GetComponentInChildren<Text>().text = "そこまで！";
                    time = 0;
                    viewedTime = 0;
                }
            }
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

    public State GetState()
    {
        return state;
    }
    public void SetMaxScale(float scale)
    {
        if (maxScale < scale)
            maxScale = scale;
    }
    public float GetMaxScale()
    {
        return maxScale;
    }
    public void SetMaxSeyana(GameObject obj)
    {
        maxSeyanaObject = obj;
    }
    public GameObject GetMaxSeyana()
    {
        return maxSeyanaObject;
    }
    public void StartShale()
    {
        index = 0;
        time = 0;
        AudioManager.Instance.PlaySE("pii");
        state = State.SHAKE;
        Destroy(nowOrderText);
        nowOrderText = Instantiate(orderText, transform);
        nowOrderText.GetComponentInChildren<Text>().text = "ふりまわせ！";
    }
    public void SetRedPower(float power)
    {
        redPower = power;
        redBar.transform.localScale = new Vector2(1, power / 200);
    }
    public void SetBluePower(float power)
    {
        bluePower = power;
        blueBar.transform.localScale = new Vector2(1, power / 200);
    }
}
