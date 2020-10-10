using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageText : MonoBehaviour
{
    const float TEXT_INTERVAL = 0.05f;
    float viewTextTime;
    int viewWordCount;
    string message;
    Text text;
    AudioSource source;
    GameObject messageCursor;

    // Start is called before the first frame update
    void Awake()
    {
        messageCursor = GameObject.Find("MessageCursor");
        text = GetComponent<Text>();
        source = GetComponent<AudioSource>();
        source.volume = 0.1f;
        source.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        viewTextTime += Time.deltaTime;

        if (viewWordCount >= message.Length)
        {
            source.Stop();
            messageCursor.SetActive(true);
        }
        else if (viewTextTime > TEXT_INTERVAL)
        {
            viewTextTime = 0;
            viewWordCount++;
            text.text = message.Substring(0, viewWordCount);
        }
    }

    public void SetMessage(string m)
    {
        messageCursor.SetActive(false);
        viewWordCount = 0;
        viewTextTime = 0;
        message = m;
        source.Play();
    }

    public void SetAllViewed()
    {
        viewWordCount = message.Length;
        text.text = message.Substring(0, viewWordCount);
        source.Stop();
        messageCursor.SetActive(true);
    }

    public bool IsViewed()
    {
        return viewWordCount >= message.Length;
    }
}
