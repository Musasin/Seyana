using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Talk : MonoBehaviour
{
    public GameObject selifLeft;
    public GameObject selifRight;
    public GameObject orderText;
    public GameObject timerText;
    public GameObject barObject;
    public GameObject flashObject;
    public GameObject endingObject;
    GameObject nowSelifObject, nowOrderText;
    int index = 0;
    public enum State { TALK_A, CONNECT, TALK_B, GRIP, SHAKE, TALK_C, CLICK, LIGHT, TALK_D, RESULT };
    State state;
    int viewedTime;
    float time, talkTime;
    float maxScale = 0.5f;
    GameObject maxSeyanaObject = null;
    GameObject redBar, blueBar, clickBar, flash, bar;
    float redPower, bluePower;
    int clickCount;

    // Start is called before the first frame update
    void Start()
    {
        clickBar = GameObject.Find("ClickBar");
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
                    StaticValues.scaleScore = (int)((maxScale - 0.5f) * 10000);
                    Debug.Log("scaleScore: " + StaticValues.scaleScore);

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
                        case 7.0f:
                            SelifInstantiate(selifLeft, "わー、おっきい！\nさすがお姉ちゃん！", false);
                            break;
                        case 7.5f:
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
                        nowOrderText.GetComponentInChildren<Text>().fontSize = 90;
                        bar = Instantiate(barObject, transform);
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
                    maxSeyanaObject.GetComponent<Seyana>().ResetPos();

                    float powerSum = bluePower + redPower;
                    
                    StaticValues.shakeScore = (int)Mathf.Min(50000, powerSum * 100);
                    Debug.Log("shakeScore: " + StaticValues.shakeScore);

                    if (powerSum < 10)
                        SelifInstantiate(selifLeft, "すっぽ抜けちゃったかな？", false);
                    else if (powerSum < 50)
                        SelifInstantiate(selifLeft, "まぁまぁかな！", false);
                    else if (powerSum < 100)
                        SelifInstantiate(selifLeft, "いい感じだね！", false);
                    else if (powerSum < 150)
                        SelifInstantiate(selifLeft, "すごい！\n泡立ってきたよ！", false);
                    else if (powerSum < 250)
                        SelifInstantiate(selifLeft, "すごいすごい！\nこれは期待できる!", false);
                    else if (powerSum < 400)
                        SelifInstantiate(selifLeft, "ひょっとして\nセヤナー振りの\nプロだったりする？", false);
                    else
                        SelifInstantiate(selifLeft, "うわぁ...\nこのレベルのは\n初めて見た", false);

                }
            } else if (talkTime > 2)
            {
                switch (index)
                {
                    case 0:
                        index++;
                        SelifInstantiate(selifRight, "ぬるぬるする", false);
                        talkTime = 0;
                        break;
                    case 1:
                        index++;
                        SelifInstantiate(selifRight, "ふおおおおおお", false);
                        talkTime = 0; 
                        break;
                }
            }
        }
        
        if (state == State.TALK_C)
        {
            time += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && time > 1.0f)
            {
                switch (index)
                {
                    case 0:
                        index++;
                        float powerSum = bluePower + redPower;
                        if (powerSum < 10)
                            SelifInstantiate(selifRight, "だって\nほぼ液体やんけ！");
                        else if (powerSum < 100)
                            SelifInstantiate(selifRight, "これがこのあと\nどうなって\nしまうんや...");
                        else if (powerSum < 150)
                            SelifInstantiate(selifRight, "大丈夫なんかそれ？");
                        else if (powerSum < 250)
                            SelifInstantiate(selifRight, "何が？");
                        else if (powerSum < 400)
                            SelifInstantiate(selifRight, "何やそれ");
                        else
                            SelifInstantiate(selifRight, "手ぇ疲れた");
                        break;
                    case 1:
                        index++;
                        SelifInstantiate(selifLeft, "よーし！次が\n最後の工程だよ！");
                        break;
                    case 2:
                        index++;
                        SelifInstantiate(selifLeft, "後は\nひたすらつついて\n発光させよう！");
                        break;
                    case 3:
                        index++;
                        SelifInstantiate(selifRight, "つついたら光るのが\n謎すぎる");
                        break;
                    case 4:
                        index++;
                        Destroy(nowOrderText);
                        nowOrderText = Instantiate(orderText, transform);
                        nowOrderText.GetComponentInChildren<Text>().text = "とどめだ！\nセヤナーを連打して\n発光させろ！";
                        SelifInstantiate(selifRight, "とどめって何？！");
                        state = State.CLICK;
                        break;
                    case 5:
                        break;
                }
            }
        }

        if (state == State.CLICK)
        {
            index = 0;
            time = 0;
            AudioManager.Instance.PlaySE("pii");
            state = State.LIGHT;
        }

        if (state == State.LIGHT)
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
                    state = State.TALK_D;
                    index = 0;
                    AudioManager.Instance.PlaySE("pipi");
                    timeObj.GetComponentInChildren<Text>().text = "そこまで！";
                    time = 0;
                    viewedTime = 0;
                    maxSeyanaObject.GetComponent<Seyana>().ResetPos();
                    
                    SelifInstantiate(selifRight, "うわあああああ！！", false);
                    flash = Instantiate(flashObject, transform);
                    AudioManager.Instance.PlaySE("flash");
                    AudioManager.Instance.FadeOutBGM();

                    
                    StaticValues.clickScore = (int)Mathf.Min(50000, clickCount * 1000);
                    Debug.Log("clickScore: " + StaticValues.clickScore);
                }
            } else if (talkTime > 2)
            {
                switch (index)
                {
                    case 0:
                        index++;
                        SelifInstantiate(selifRight, "くぬぬぬぬぬ", false);
                        talkTime = 0;
                        break;
                    case 1:
                        index++;
                        SelifInstantiate(selifRight, "うおおおおおお", false);
                        talkTime = 0; 
                        break;
                }
            }
        }

        if (state == State.TALK_D)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                state = State.RESULT;

                if (StaticValues.GetSumScore() < 10000)
                {
                    flash.GetComponent<Animator>().SetBool("isDisappear", true);
                    Destroy(bar);
                    Destroy(clickBar);
                    Destroy(nowOrderText);
                    time = 0;
                    index = 0;
                } else
                {
                    SceneManager.LoadScene("EndingScene");
                    return;
                }
            }
        }
        
        if (state == State.RESULT)
        {
            time += Time.deltaTime;
            if (index == 0 && time > 1.0f)
            {
                index++;
                AudioManager.Instance.PlayBGM("Talk", 0.1f);
                SelifInstantiate(selifRight, "あれ...?\nなんともないな");
            }

            if (index >= 9 && time > 3.0f) {
                SceneManager.LoadScene("TitleScene"); // 仮。 リザルト画面を作ったらそっちに飛ばす
            }

            if (Input.GetMouseButtonDown(0) && time > 1.0f)
            {
                switch (index)
                {
                    case 1:
                        index++;
                        SelifInstantiate(selifLeft, "ありゃま、\n不発だったかな？");
                        break;
                    case 2:
                        index++;
                        SelifInstantiate(selifRight, "何事も無いなら\nそれが一番やね");
                        break;
                    case 3:
                        index++;
                        SelifInstantiate(selifLeft, "うーん、仕込みが\n甘かったかなぁ");
                        break;
                    case 4:
                        index++;
                        SelifInstantiate(selifRight, "気が済んだなら\n帰るで");
                        break;
                    case 5:
                        index++;
                        SelifInstantiate(selifLeft, "あっ、待ってよ\nお姉ちゃん！");
                        break;
                    case 6:
                        index++;
                        SelifInstantiate(selifRight, "あと連れてきた\nセヤナーもちゃんと\n面倒見るんやで");
                        break;
                    case 7:
                        index++;
                        SelifInstantiate(selifLeft, "はぁーい");
                        break;
                    case 8:
                        index++;
                        AudioManager.Instance.FadeOutBGM();
                        Instantiate(endingObject, transform);
                        time = 0;
                        break;
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
        nowOrderText.GetComponentInChildren<Text>().fontSize = 120;
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
    public void AddClickCount()
    {
        clickCount++;
        clickBar.transform.localScale = new Vector3(1, (float)clickCount / 50, 1);
    }
}
