using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seyana : MonoBehaviour
{
    bool isGrip;
    Camera mainCamera;
    float scale;
    float velocityX;
    bool isRight;
    float movement;

    Rigidbody2D rb;
    Talk talk;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        talk = GameObject.Find("Canvas").GetComponent<Talk>();
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        scale = transform.localScale.x;
        velocityX = Random.Range(0.2f, 5.0f);
        isRight = (Random.Range(0, 1) < 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(movement);
        if (talk.GetState() == Talk.State.TALK_B)
        {
            if (talk.GetMaxSeyana() == gameObject)
            {
                return;
            }
            if (talk.GetMaxScale() == scale && talk.GetMaxSeyana() == null)
            {
                rb.velocity = Vector2.zero;
                transform.position = new Vector2(0, 1.0f);
                talk.SetMaxSeyana(gameObject);
            }
            else
                Destroy(gameObject);
        }
        if (talk.GetState() == Talk.State.CONNECT || talk.GetState() == Talk.State.SHAKE)
        {
            if (isGrip)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    isGrip = false;
                }
                else
                {
                    var screenPos = Input.mousePosition;
                    screenPos.z = Mathf.Abs(mainCamera.transform.position.z);

                    if (talk.GetState() == Talk.State.SHAKE)
                        movement += Mathf.Abs(screenPos.x - transform.position.x) + Mathf.Abs(screenPos.y - transform.position.y);

                    transform.position = mainCamera.ScreenToWorldPoint(screenPos);
                }

            }
            else if (talk.GetState() == Talk.State.CONNECT)
            {
                rb.velocity = new Vector2(velocityX * (isRight ? 1 : -1), rb.velocity.y);
                transform.localScale = new Vector2(isRight ? -scale : scale, scale);
            }
        }
    }

    public void Grip()
    {
        if (talk.GetState() == Talk.State.CONNECT || talk.GetState() == Talk.State.GRIP || talk.GetState() == Talk.State.SHAKE)
        {
            if (talk.GetState() == Talk.State.GRIP)
                talk.StartShale();

            AudioManager.Instance.PlaySE("pyui");
            isGrip = true;
        }
    }

    public void AddScale(float addScale)
    {
        scale += addScale;
        talk.SetMaxScale(scale);
    }
    public float GetScale()
    {
        return scale;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isGrip)
        {
            isGrip = false;
            AddScale(collision.gameObject.GetComponent<Seyana>().GetScale());
            AudioManager.Instance.PlaySE("union");
            Destroy(collision.gameObject);
        }

        if (collision.tag == "WallLeft")
            isRight = true;
        if (collision.tag == "WallRight")
            isRight = false;
    }
}
