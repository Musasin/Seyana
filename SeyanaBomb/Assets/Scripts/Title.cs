using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    Animator anim1, anim2;
    float animTime;
    bool taped;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("Title", 0.1f);
        anim1 = GameObject.Find("BigSeyana").GetComponent<Animator>();
        anim2 = GameObject.Find("TitleItems").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && !taped)
        {
            anim1.SetBool("Tap", true);
            anim2.SetBool("Tap", true);
            AudioManager.Instance.FadeOutBGM();
            AudioManager.Instance.PlaySE("machdash1");
            taped = true;
            animTime = 0;
        }

        if (taped && animTime > 1.15f) { 
            SceneManager.LoadScene("MainScene");
        }
    }
}
