using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject interviewer, aoi, companyBackground;

    MessageText messageText;
    Animator flashAnimator;
    int index;
    float time;
    string[] messages;
    GameObject[] gameObjects;
    string[] graphDiffs;
    GraphDiffer graphDiffer;
    
    GameObject seyanaPlanet;
    GameObject company;

    // Start is called before the first frame update
    void Start()
    {
        flashAnimator = GameObject.Find("EndingFlash").GetComponent<Animator>();
        flashAnimator.SetBool("isEndingDisappear", true);
        messageText = GameObject.Find("MessageText").GetComponent<MessageText>();

        company = GameObject.Find("Company");
        company.SetActive(false);
        seyanaPlanet = GameObject.Find("SeyanaPlanet");
        seyanaPlanet.SetActive(false);

        int score = StaticValues.GetSumScore();
        if (score < 40000)
            SetupCompany();         // 商品展開エンド
        else if (score < 60000)
            SetupHouseBroken();     // 家破壊エンド
        else if (score < 90000)
            SetupAoiDead();         // 葵死亡エンド
        else if (score < 120000)
            SetupJapanCollapse();   // 日本崩壊エンド
        else if (score < 140000)
            SetupHumanExtinction(); // 人類滅亡エンド
        else
            SetupSeyanaPlanet();    // セヤナーの惑星エンド
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < 3)
            return;

        bool isClick = Input.GetMouseButtonDown(0);
        if (!isClick && index != 0)
            return;

        if (!messageText.IsViewed())
        {
            messageText.SetAllViewed();
            return;
        }

        messageText.SetMessage(messages[index]);
        if (gameObjects[index] != null)
        {
            GameObject obj = Instantiate(gameObjects[index], GameObject.Find("BackGroundObjects").transform);
            if (obj.GetComponent<GraphDiffer>() != null) {
                graphDiffer = obj.GetComponent<GraphDiffer>();
            }
        }
        if (graphDiffs[index] != "")
            graphDiffer.SetSprite(graphDiffs[index]);
        
        index++;
    }

    private void SetupCompany()
    {
        company.SetActive(true);
        messages = new string[10]
        {
            "\n\n〜３年後〜",
            "- インタビュアー -\n\n今週もやってまいりました。\n世紀の大発明を振り返る、\n仰天!〜あの発明品が生まれるまで〜の\nコーナーです。",
            "- インタビュアー -\n\n今週はあの\"セヤナー爆弾\"を発明した\n琴葉葵社長をおよびしています。\nどうぞ",
            "- 葵 -\n\n琴葉コーポレーション社長の葵です。\nよろしくお願いします。",
            "- インタビュアー -\n\n今や映像作品での演出や子供たちの遊具としても親しまれているセヤナー爆弾ですが、\n当時の生産方法はとても危険なものだったと伺っております。",
            "- 葵 -\n\nはい。3年前のあの日、たまたま程よいバランスでの調合に成功したことが本製品開発のきっかけでしたが、今のような製法技術は無く、\n危険と隣合わせでした。",
            "- 葵 -\n\n当時の私たちは幼く、その危険性も理解せず調合を行っておりました。もう少し刺激を与えていたらどうなっていたことか...。",
            "- インタビュアー -\n\nそんな危険の伴う物を安全に製品化するところまで行っている点が琴葉コーポレーションの功績とも言えますね。",
            "",
            "",
        };
        gameObjects = new GameObject[10]
        {
            companyBackground,
            interviewer,
            null,
            aoi,
            null,
            null,
            null,
            null,
            null,
            null
        };
        graphDiffs = new string[10]
        {
            "",
            "",
            "",
            "aoi1",
            "",
            "aoi2",
            "aoi3",
            "",
            "",
            "",
        };

    }
    private void SetupHouseBroken()
    {
    
    }
    private void SetupAoiDead()
    {

    }
    private void SetupJapanCollapse()
    {
    }
    private void SetupHumanExtinction()
    {
        messages = new string[3]
        {
            "\n\nセヤナー融合により放たれた大爆発は世界中を包み込み、一日にして世界地図を一色に塗り替えた。",
            "\n\n。aaa",
            ""
        };
    }
    private void SetupSeyanaPlanet()
    {
        seyanaPlanet.SetActive(true);
        messages = new string[3]
        {
            "- ？？？ -\n\nその日、旧人類は絶滅し、我々が誕生した。そう。我々セヤナー人類誕生の瞬間である。",
            "\n\n。aaa",
            ""
        };

    }
}
