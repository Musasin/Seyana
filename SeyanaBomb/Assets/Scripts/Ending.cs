using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject endingResultObject, interviewer, akaneB, aoiB, akaneB2, aoiB2, seyanaB, akaneC, seyanaC, seyanaC2, aoi, akaneE, akaneE2, akaneE3, akaneE4, aoiE, aoiE2, aoiE3, seyanaE, seyanaE2, seyanaE3, seyanaE4, seyanaE5, companyBackground, horrorBackground, horrorBackground2, earth;

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
    GameObject band, horror, company, humanExtinction, seyanaInvasion, seyanaPlanet;

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
        band = GameObject.Find("Band");
        band.SetActive(false);
        horror = GameObject.Find("Horror");
        horror.SetActive(false);
        company = GameObject.Find("Company");
        company.SetActive(false);
        humanExtinction = GameObject.Find("HumanExtinction");
        humanExtinction.SetActive(false);
        seyanaInvasion = GameObject.Find("SeyanaInvasion");
        seyanaInvasion.SetActive(false);
        seyanaPlanet = GameObject.Find("SeyanaPlanet");
        seyanaPlanet.SetActive(false);

        int score = StaticValues.GetSumScore();
        if (score < 40000)
            SetupBand();     // バンド結成エンド
        else if (score < 60000)
            SetupHorror();         // あなたの後ろにも亡エンド
        else if (score < 90000)
            SetupCompany();         // 商品展開エンド
        else if (score < 120000)
            SetupSeyanaInvasion();   // セヤナー侵略エンド
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
            SceneManager.LoadScene("ResultScene");
        }
        
        if (messages.Length == index && state == State.PLAY)
        {
            time = 0;
            state = State.RESULT;
            GameObject obj = Instantiate(endingResultObject, transform);
            obj.GetComponentInChildren<Text>().text = endingResultText;
            return;
        }

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
            if (gameObjects[index] == backGroundObjects)
            {
                backGroundObjects.GetComponent<Animator>().SetBool("tv_off", true);
            }
            else
            {
                GameObject obj = Instantiate(gameObjects[index], GameObject.Find("BackGroundObjects").transform);
                if (obj.GetComponent<GraphDiffer>() != null)
                {
                    graphDiffer = obj.GetComponent<GraphDiffer>();
                }
            }
        }
        if (graphDiffs[index] != "")
            graphDiffer.SetSprite(graphDiffs[index]);
        
        index++;
    }
    
    private void SetupBand()
    {
        band.SetActive(true);
        messages = new string[14]
        {
            "- 茜 -\n\nみんな〜！！",
            "- 葵 -\n\n盛り上がってる〜？！",
            "- セヤナー -\n\nﾔﾃﾞ-!!!",
            "\n\nあの日。\nあの時を堺に、私達はバンドを結成した。",
            "\n\n激しい振動とともに光り輝くセヤナーを見て、お姉ちゃんが「これや！！！」って言ってね",
            "\n\n今では演出兼ボーカルのセヤナーと、\nドラムのお姉ちゃん、\nそしてギターの私で細々とバンド活動をしてる。",
            "\n\nやっぱり3人だと表現の幅に限界があるから、ベースのメンバーを募集中。",
            "\n\nあの時はまさかこんな事になるなんて\n思いもしなかったけど...",
            "- 茜 -\n\nなぁ葵！！",
            "- 葵 -\n\nなぁにお姉ちゃん！",
            "- 茜 -\n\n楽しいなぁ！！",
            "- 葵 -\n\n......うん！！",
            "\n\n今となっては、あの出会いに感謝しているんだ。",
            " ",
        };
        gameObjects = new GameObject[14]
        {
            akaneB,
            aoiB,
            seyanaB,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            akaneB2,
            aoiB2,
            null,
            null,
        };
        graphDiffs = new string[14]
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
        };
        endingResultText = "エンディング part.B\nバンド結成";
    }
    private void SetupHorror()
    {
        horror.SetActive(true);
        messages = new string[12]
        {
            "\n\n最近よく夢を見る。",
            "\n\nきっかけはあの日。\n葵に言われるまま、セヤナー爆弾とかいう\nようわからん物を作ってから。",
            "- 茜 -\n\nあおいー....\nまだ帰ってないんか？",
            "\n\n爆発こそ大きな規模でなかったものの、\nなんと巨大化したセヤナーが分裂して\n初めよりも多くのセヤナーが生まれた。",
            "- 茜 -\n\n鍵は開いとるな...",
            "\n\nその時の光景は、なんというか、\n例えるならそう、カマキリの卵、みたいな。",
            "- 茜 -\n\nただいまー...っと",
            "\n\nとにかくあのおぞましい光景が忘れられず、\n今でも大量のセヤナーが群がってくる夢を見る",
            "- 茜 -\n\nなんや、帰ってるなら電気くらい...",
            "- 茜 -\n\n......ヒッ",
            "\n\nウチが最後に目にした物は、\n頭上から降ってくるセヤナーの群れやった。",
            " ",
        };
        gameObjects = new GameObject[12]
        {
            akaneC,
            horrorBackground,
            akaneC,
            null,
            null,
            null,
            horrorBackground2,
            seyanaC,
            akaneC,
            seyanaC2,
            backGroundObjects,
            null,
        };
        graphDiffs = new string[12]
        {
            "",
            "",
            "akane_c2",
            "",
            "akane_c1",
            "",
            "",
            "",
            "akane_c3",
            "akane_c4",
            "",
            "",
        };
        endingResultText = "エンディング part.C\nあなたの後ろにも";
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
            "aoi_d1",
            "",
            "aoi_d2",
            "aoi_d3",
            "",
            "aoi_d2",
            "",
            "",
            "",
        };
        endingResultText = "エンディング part.D\n商品展開";
    }
    private void SetupSeyanaInvasion()
    {
        seyanaInvasion.SetActive(true);
        messages = new string[16]
        {
            "- 葵 -\n\nお姉ちゃん！生きてる？！",
            "- 茜 -\n\n当たり前や！\nウチを誰やと思うてんねん！",
            "\n\n私達は今、地球外生命体と\n小規模な宇宙戦争を繰り広げている。",
            "\n\nきっかけはあの日。\nあのセヤナー爆弾だ。",
            "\nこれは後から知ったんだけど、セヤナー達は\n特定間隔での振動,発光で宇宙にいる\nセヤナーに交信しており、今回の調合で\nたまたまSOSと合致してしまっていたらしい。",
            "\n\nそれを聞きつけた宇宙セヤナー達が\n地球に攻め込んできたというわけ。",
            "- 茜 -\n\n数が多すぎる！\nキリがないで！",
            "- 葵 -\n\n物資ももうだいぶ心許ないよお姉ちゃん...!",
            "\n\n私達は、\n自分の撒いた火種を回収すべく戦っている。",
            "- 葵 -\n\n......ごめんねお姉ちゃん\nこんなことになっちゃって...。",
            "- 茜 -\n\nもー！何回目やそれ！",
            "- 茜 -\n\nウチはそんなこと気にしとらんし、\nどんなことがあっても葵とずっと一緒やで！",
            "- 葵 -\n\nお姉ちゃん....",
            "\n\nたとえこの戦いに終わりがなくとも。",
            "\n\nきっと私達は最後まで戦い続けるのだろう。",
            " ",
        };
        gameObjects = new GameObject[16]
        {
            aoiE,
            akaneE,
            null,
            seyanaE,
            seyanaE2,
            seyanaE3,
            akaneE2,
            aoiE2,
            seyanaE4,
            aoiE3,
            akaneE3,
            akaneE4,
            aoiE2,
            seyanaE5,
            seyanaE3,
            null,
        };
        graphDiffs = new string[16]
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
        };
        endingResultText = "エンディング part.E\nセヤナー侵略";
    }
    private void SetupHumanExtinction()
    {
        humanExtinction.SetActive(true);
        messages = new string[5]
        {
            "\n\nセヤナー融合により放たれた大爆発は\n世界中を包み込み、",
            "\n\n一日にして世界地図を一色に塗り替えた。",
            "\n\n人類はおろか、\nあらゆる生物が死滅した世界は、",
            "\n\n再びゆっくりと時を刻み始めるのであった。",
            " ",
        };
        
        gameObjects = new GameObject[5]
        {
            null,
            earth,
            null,
            null,
            null,
        };
        graphDiffs = new string[5]
        {
            "",
            "",
            "",
            "",
            "",
        };
        endingResultText = "エンディング part.F\n人類滅亡";
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

    public void SkipButton()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
