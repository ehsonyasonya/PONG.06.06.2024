using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ball : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float speed = 6f;

    public UIManager UIManager;

    public int LeftPlayerScore;
    public int RightPlayerScore;

    public static event Action BallReset;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        SendBallInrandomDirection();
    }

    private void SendBallInrandomDirection()
    {
        BallReset?.Invoke();

        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.isKinematic = true;
        transform.position = Vector3.zero;
        rigidbody2D.isKinematic = false;

        Vector2 newBallVector = new Vector2();
        newBallVector.x = Random.Range(-1f, 1f);
        newBallVector.y = Random.Range(-1f, 1f);
        rigidbody2D.velocity = newBallVector.normalized * speed;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SendBallInrandomDirection();

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<movement>() == null)
            return;

        collision.gameObject.GetComponent <movement>().speed *= 1.1f;
            rigidbody2D.velocity *= 1.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x > 0)
        {
            Debug.Log("player left +1");
            LeftPlayerScore++;
            UIManager.SetLeftPlayerScoreText(LeftPlayerScore.ToString());
        }
        else
        {
            Debug.Log("player right +1");
            RightPlayerScore++;
            UIManager.SetRightPlayerScoreText(RightPlayerScore.ToString());

        }

        SendBallInrandomDirection();
    }
}
