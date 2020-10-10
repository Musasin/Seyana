using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    MessageText messageText;


    // Start is called before the first frame update
    void Start()
    {
        messageText = GameObject.Find("MessageText").GetComponent<MessageText>();
        messageText.SetMessage("- ？？？ -\n\nその日、旧人類は絶滅し、我々が誕生した。そう。我々セヤナー人類誕生の瞬間である。");

    }

    // Update is called once per frame
    void Update()
    {
        int score = StaticValues.GetSumScore();

        if (score < 4000)
            UpdateCompany();         // 商品展開エンド
        else if (score < 6000)
            UpdateHouseBroken();     // 家破壊エンド
        else if (score < 9000)
            UpdateAoiDead();         // 葵死亡エンド
        else if (score < 12000)
            UpdateJapanCollapse();   // 日本崩壊エンド
        else if (score < 14000)
            UpdateHumanExtinction(); // 人類滅亡エンド
        else
            UpdateSeyanaPlanet();    // セヤナーの惑星エンド
    }

    private void UpdateCompany()
    {

    }
    private void UpdateHouseBroken()
    {
    
    }
    private void UpdateAoiDead()
    {

    }
    private void UpdateJapanCollapse()
    {
    }
    private void UpdateHumanExtinction()
    {
    }
    private void UpdateSeyanaPlanet()
    {

    }
}
