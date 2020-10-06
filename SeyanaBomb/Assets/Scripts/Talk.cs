using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    public GameObject selifLeft;
    public GameObject selifRight;
    GameObject nowSelifObject;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("Title");
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
                    nowSelifObject = Instantiate(selifLeft, transform);
                    nowSelifObject.GetComponentInChildren<Text>().text = "セヤナー爆弾が\nできたよお姉ちゃん！";
                    break;
                case 1:
                    index++;
                    Destroy(nowSelifObject);
                    nowSelifObject = Instantiate(selifRight, transform);
                    nowSelifObject.GetComponentInChildren<Text>().text = "セヤナー爆弾？！";
                    break;
            }
        }

    }
}
