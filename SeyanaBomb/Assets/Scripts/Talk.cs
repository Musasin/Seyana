using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    public GameObject selifLeft;
    public GameObject selifRight;
    public GameObject orderText;
    public GameObject timerText;
    GameObject nowSelifObject, nowOrderText;
    int index = 0;
    public enum State { TALK_A = 0,  CONNECT = 1, TALK_B = 2};
    State state;
    int viewedTime;
    float time, talkTime;
    float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("Talk", 0.1f);
        state = State.TALK_A;
        nowSelifObject = Instantiate(selifLeft, transform);
        nowSelifObject.GetComponentInChildren<Text>().text = "セヤナー爆弾が\nできたよお姉ちゃん！";
        AudioManager.Instance.PlaySE("pon", 0.2f);
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
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifRight, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "セヤナー爆弾？！";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
                        break;
                    case 1:
                        index++;
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifLeft, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "セヤナーをつなげて\nよく振って\n刺激すると、";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
                        break;
                    case 2:
                        index++;
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifLeft, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "激しい光とともに\n爆散するんだよ！";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
                        break;
                    case 3:
                        index++;
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifRight, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "激しい光とともに爆散？！";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
                        break;
                    case 4:
                        index++;
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifLeft, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "さっそく\nやってみよう！";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
                        break;
                    case 5:
                        index++;
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifRight, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "なんで？！";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
                        break;
                    case 6:
                        index++;
                        nowOrderText = Instantiate(orderText, transform);
                        nowOrderText.GetComponentInChildren<Text>().text = "セヤナーを\nつなげて集めろ！";

                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifRight, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "まってまって！";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
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

                    Destroy(nowSelifObject);
                    nowSelifObject = Instantiate(selifLeft, transform);
                    nowSelifObject.GetComponentInChildren<Text>().text = "大きくなったね！";
                }
            }

            if (talkTime > 2)
            {
                switch (index)
                {
                    case 0:
                        index++;
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifRight, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "うわぁいっぱいおる";
                        talkTime = 0;
                        break;
                    case 1:
                        index++;
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifRight, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "どうすんのこれ";
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
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifRight, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "せ、せやな...";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
                        break;
                    case 1:
                        index++;
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifLeft, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "次は\nしっかり掴んで\n振り回すの！";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
                        break;
                    case 2:
                        index++;
                        Destroy(nowSelifObject);
                        nowSelifObject = Instantiate(selifLeft, transform);
                        nowSelifObject.GetComponentInChildren<Text>().text = "準備はいい？";
                        AudioManager.Instance.PlaySE("pon", 0.2f);
                        break;
                }
            }
        }
    }

    public State GetState()
    {
        return state;
    }
    public void SetMaxScale(float scale)
    {
        Debug.Log(maxScale);
        if (maxScale < scale)
            maxScale = scale;
    }
    public float GetMaxScale()
    {
        return maxScale;
    }
}
