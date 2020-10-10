using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticValues : MonoBehaviour
{
    public static int scaleScore;
    public static int shakeScore;
    public static int clickScore;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static int GetSumScore()
    {
        return 20000; // テスト用仮
        return scaleScore + shakeScore + clickScore;
    }
}