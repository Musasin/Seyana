using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject endingResultObject, interviewer, aoi, companyBackground;

    string endingResultText;
    MessageText messageText;
    Animator flashAnimator;
    int index;
    float time;
    string[] messages;
    GameObject[] gameObjects;
    string[] graphDiffs;
    GraphDiffer graphDiffer;
    
    GameObject backGroundObjects;
    GameObject seyanaPlanet;
    GameObject company;

    enum State
    {
        PLAY, RESULT, END
    };
    State state = State.PLAY;

    // Start is called before the first frame update
    void Start()
    {
        flashAnimator = GameObject.Find("EndingFlash").GetComponent<Animator>();
        flashAnimator.SetBool("isEndingDisappear", true);
        messageText = GameObject.Find("MessageText").GetComponent<MessageText>();

        backGroundObjects = GameObject.Find("BackGroundObjects");
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

        if (state == State.RESULT)
        {
            state = State.END;
            // フェードアウトを挟む
            SceneManager.LoadScene("TitleScene"); // 仮。 リザルト画面を作ったらそっちに飛ばす
        }

        bool isClick = Input.GetMouseButtonDown(0);
        if (!isClick && index != 0)
            return;

        if (!messageText.IsViewed())
        {
            messageText.SetAllViewed();
            return;
        }

        if (messages.Length == index)
        {
            time = 0;
            state = State.RESULT;
            GameObject obj = Instantiate(endingResultObject, transform);
            obj.GetComponentInChildren<Text>().text = endingResultText;
            return;
        }

        messageText.SetMessage(messages[index]);
        if (gameObjects[index] != null)
        {
            if (gameObjects[index] == backGroundObjects)
                backGroundObjects.GetComponent<Animator>().SetBool("tv_off", true);
            else
            { 
                GameObject obj = Instantiate(gameObjects[index], GameObject.Find("BackGroundObjects").transform);
                if (obj.GetComponent<GraphDiffer>() != null) {
                    graphDiffer = obj.GetComponent<GraphDiffer>();
                }
            }
        }
        if (graphDiffs[index] != "")
            graphDiffer.SetSprite(graphDiffs[index]);
        
        index++;
    }

    private void SetupCompany()
    {
        company.SetActive(true);
        messages = new string[12]
        {
            "\n\n〜３年後〜",
            "- インタビュアー -\n\n今週もやってまいりました。\n世紀の大発明を振り返る、\n仰天!〜あの発明品が生まれるまで〜の\nコーナーです。",
            "- インタビュアー -\n\n今週はあの\"セヤナー爆弾\"を発明した\n琴葉葵社長をおよびしています。\nどうぞ",
            "- 葵 -\n\n琴葉コーポレーション社長の葵です。\nよろしくお願いします。",
            "- インタビュアー -\n\n今や映像作品での演出や子供たちの遊具としても親しまれているセヤナー爆弾ですが、\n当時の生産方法はとても危険なものだったと伺っております。",
            "- 葵 -\n\nはい。3年前のあの日、たまたま程よいバランスでの調合に成功したことが本製品開発のきっかけでしたが、今のような製法技術は無く、\n危険と隣合わせでした。",
            "- 葵 -\n\n当時の私たちは幼く、その危険性も理解せず調合を行っておりました。\nもう少し刺激を与えていたらどうなっていたことか...。",
            "- インタビュアー -\n\nそんな危険の伴う物を安全に製品化する\nところまで行っている点が琴葉コーポレーションの功績とも言えますね。",
            "- 葵 -\n\nこの場を借りて改めて注意喚起させて頂きますと、我が社から販売されているセヤナー爆弾は、結合,振動を規定値以下に収めることで安全性を担保しています。",
            "- 葵 -\n\n一般の方による調合は非常に危険ですので、野生のセヤナーを見つけても決して刺激しないようにしてください。",
            "- インタビュアー -\n\n...はい、ありがとうございました。\nそれでは次の質問ですがーーー",
            " ",
        };
        gameObjects = new GameObject[12]
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
            null,
            null,
            backGroundObjects,
        };
        graphDiffs = new string[12]
        {
            "",
            "",
            "",
            "aoi1",
            "",
            "aoi2",
            "aoi3",
            "",
            "aoi2",
            "",
            "",
            "",
        };
        endingResultText = "エンディング part.B\n全国展開";
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
