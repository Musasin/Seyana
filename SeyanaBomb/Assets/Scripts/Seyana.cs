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
    float movementX, movementY;
    float soundMovementSum;

    Rigidbody2D rb;
    Talk talk;
    bool isPosRight;
    float moveTime, scaleTime;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        talk = GameObject.Find("Canvas").GetComponent<Talk>();
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        sr = this.GetComponent<SpriteRenderer>();
        scale = transform.localScale.x;
        velocityX = Random.Range(0.2f, 5.0f);
        isRight = (Random.Range(0, 1) < 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (talk.GetState() == Talk.State.TALK_C || talk.GetState() == Talk.State.TALK_D)
        {
            if (talk.GetState() == Talk.State.TALK_D)
            {
                float randR = Random.Range(0.2f, 1.0f);
                float randG = Random.Range(0.2f, 1.0f);
                float randB = Random.Range(0.2f, 1.0f);
                sr.color = new Color(randR, randG, randB);
            }

            moveTime += Time.deltaTime;
            if (moveTime > 0.05f)
            {
                float movePower = (movementX + movementY) / 1000 * (isPosRight ? 1 : -1);
                transform.position = new Vector2(movePower, 1.0f);
                isPosRight = !isPosRight;
                moveTime = 0;
            }
        }
        if (talk.GetState() == Talk.State.LIGHT || talk.GetState() == Talk.State.TALK_D)
        {
            scaleTime += Time.deltaTime;
            if (scaleTime > 0.5f)
            {
                scaleTime = 0;
            }
            transform.localScale = new Vector2(scale * 1.5f - (scale * scaleTime), scale * 1.5f - (scale * scaleTime));
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, scaleTime * 1.5f);
        }
        if (talk.GetState() == Talk.State.CONNECT || talk.GetState() == Talk.State.SHAKE)
        {
            if (isGrip)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    AudioManager.Instance.PlaySE("petyo");
                    isGrip = false;
                }
                else
                {
                    var screenPos = Input.mousePosition;
                    screenPos.z = Mathf.Abs(mainCamera.transform.position.z);
                    if (screenPos.x < 0 || screenPos.x > 768 || screenPos.y < 0 || screenPos.y > 1024)
                    {
                        AudioManager.Instance.PlaySE("petyo");
                        isGrip = false;
                        return;
                    }

                    var worldPos = mainCamera.ScreenToWorldPoint(screenPos);

                    if (talk.GetState() == Talk.State.SHAKE)
                    {
                        movementX += Mathf.Abs(transform.position.x - worldPos.x) - Time.deltaTime * 5;
                        movementY += Mathf.Abs(transform.position.y - worldPos.y) - Time.deltaTime * 5;
                        movementX = Mathf.Max(movementX, 0);
                        movementY = Mathf.Max(movementY, 0);
                        talk.SetRedPower(movementX);
                        talk.SetBluePower(movementY);
                        
                        Debug.Log(movementX);
                        Debug.Log(movementY);

                        soundMovementSum += Mathf.Abs(worldPos.x - transform.position.x) + Mathf.Abs(worldPos.y - transform.position.y);
                        if (soundMovementSum > 30)
                        {
                            AudioManager.Instance.PlaySE("buon");
                            soundMovementSum = 0;
                        }
                    }

                    transform.position = worldPos;
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
        if (talk.GetState() == Talk.State.CLICK || talk.GetState() == Talk.State.LIGHT)
        {
            talk.AddClickCount();
            float randR = Random.Range(0.2f, 1.0f);
            float randG = Random.Range(0.2f, 1.0f);
            float randB = Random.Range(0.2f, 1.0f);
            sr.color = new Color(randR, randG, randB);
        }
        else if (talk.GetState() == Talk.State.CONNECT || talk.GetState() == Talk.State.GRIP || talk.GetState() == Talk.State.SHAKE)
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

    public void ResetPos()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0, 1.0f);
    }
}
