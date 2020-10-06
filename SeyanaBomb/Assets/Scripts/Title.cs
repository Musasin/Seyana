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
        AudioManager.Instance.PlayBGM("Title");
        anim1 = GameObject.Find("BigSeyana").GetComponent<Animator>();
        anim2 = GameObject.Find("TitleItems").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            anim1.SetBool("Tap", true);
            anim2.SetBool("Tap", true);
            AudioManager.Instance.FadeOutBGM();
            taped = true;
            animTime = 0;
        }

        if (taped && animTime > 1.2f) { 
            SceneManager.LoadScene("MainScene");
        }
    }
}
