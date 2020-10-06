using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    public GameObject selifLeft;
    public GameObject selifRight;
    public GameObject orderText;
    GameObject nowSelifObject, nowOrderText;
    int index = 0;
    public enum State { TALK_A = 0,  CONNECT = 1, TALK_B = 2};
    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.TALK_A;
        nowSelifObject = Instantiate(selifLeft, transform);
        nowSelifObject.GetComponentInChildren<Text>().text = "セヤナー爆弾が\nできたよお姉ちゃん！";
    }

    // Update is called once per frame
    void Update()
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
                    break;
                case 1:
                    index++;
                    Destroy(nowSelifObject);
                    nowSelifObject = Instantiate(selifLeft, transform);
                    nowSelifObject.GetComponentInChildren<Text>().text = "セヤナーをつなげて\nよく振って\n刺激すると、";
                    break;
                case 2:
                    index++;
                    Destroy(nowSelifObject);
                    nowSelifObject = Instantiate(selifLeft, transform);
                    nowSelifObject.GetComponentInChildren<Text>().text = "激しい光とともに\n爆散するんだよ！";
                    break;
                case 3:
                    index++;
                    Destroy(nowSelifObject);
                    nowSelifObject = Instantiate(selifRight, transform);
                    nowSelifObject.GetComponentInChildren<Text>().text = "激しい光とともに爆散？！";
                    break;
                case 4:
                    index++;
                    Destroy(nowSelifObject);
                    nowSelifObject = Instantiate(selifLeft, transform);
                    nowSelifObject.GetComponentInChildren<Text>().text = "さっそく\nやってみよう！";
                    break;
                case 5:
                    index++;
                    Destroy(nowSelifObject);
                    nowSelifObject = Instantiate(selifRight, transform);
                    nowSelifObject.GetComponentInChildren<Text>().text = "なんで？！";
                    break;
                case 6:
                    index++;
                    nowOrderText = Instantiate(orderText, transform);
                    nowOrderText.GetComponentInChildren<Text>().text = "セヤナーを\nつなげて集めろ！";

                    Destroy(nowSelifObject);
                    nowSelifObject = Instantiate(selifRight, transform);
                    nowSelifObject.GetComponentInChildren<Text>().text = "まってまって！";

                    state = State.CONNECT;
                    break;
            }
        }
    }

    public State GetState()
    {
        return state;
    }
}
