using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("Model")]
    public GameObject ballPrefab;
    public Camera gameCamera;

    [Header("Control")]
    public float controlMoveSpeed = 0.05f;
    public float controlRotateSpeed = 0.1f;
    public Row[] rows;

    [Header("Ball")]
    public float initialSpeed = 3;
    public Vector3 initialVelocity = new Vector3(0.1f, 0, 3);
    public bool randomizeInitialVelocity = true;

    [Header("AI")]
    public float aiMoveInterval = 2;
    public float aiMoveSpeed = 2;
    public float aiRotateSpeed = 20;
    public Row[] aiRows;

    [Header("UI")]
    public Text scoreAText;
    public Text scoreBText;

    private GameObject ball = null;
    private float aiTimeleft = 0;
    private int scoreA = 0;
    private int scoreB = 0;

	// Use this for initialization
	void Start () {
        Initialize();
        aiTimeleft = aiMoveInterval;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Initialize() {
        if(ball) Destroy(ball);
        ball = Instantiate(ballPrefab);
        Rigidbody ballbody = ball.GetComponent<Rigidbody>();
        if(randomizeInitialVelocity) {
            float angle = Random.Range(0, 360);
            ballbody.velocity = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * initialSpeed;
        } else {
            ballbody.velocity = initialVelocity;
        }
    }

    public void ResetScore() {
        scoreA = scoreB = 0;
    }

    public void IncreaseScoreA() {
        scoreA++;
    }

    public void IncreaseScoreB() {
        scoreB++;
    }

    // Update is called once per frame
    void Update () {
        HandleMouseInput();
        HandleKeyboardInput();
        HandleAI();
        HandleUI();
    }

    void HandleMouseInput() {
        if(Input.mousePresent) {
            float mouseMoveX = Input.GetAxis("Mouse X");
            float mouseMoveY = Input.GetAxis("Mouse Y");
            if(!Input.GetKey("space"))
                ControlAllRows(new Vector3(mouseMoveX, mouseMoveY, 0));
        }
    }

    void HandleKeyboardInput() {
        if(Input.GetKeyDown("escape")) {
            Application.Quit();
        }
        if(Input.GetKeyDown("r")) {
            Initialize();
        }
        if(Input.GetKeyDown("s")) {
            ResetScore();
        }
    }

    void ControlAllRows(Vector3 mouseMove) {
        foreach(Row row in rows) {
            float angle = mouseMove.x / Time.deltaTime * controlRotateSpeed;
            row.Rotate(angle);
            float offset = mouseMove.y;
            row.Move(offset * controlMoveSpeed);
        }
    }

    void HandleAI() {
        aiTimeleft -= Time.deltaTime;
        if(aiTimeleft <= 0) aiTimeleft = aiMoveInterval;
        foreach(Row row in aiRows) {
            if(row.tag == "A") {
                row.Rotate(aiRotateSpeed);
            } else {
                row.Rotate(-aiRotateSpeed);
            }
            if(aiTimeleft > aiMoveInterval/2) {
                row.Move(aiMoveSpeed * Time.deltaTime);
            } else {
                row.Move(-aiMoveSpeed * Time.deltaTime);
            }
        }
    }

    void HandleUI() {
        scoreAText.text = "" + scoreA;
        scoreBText.text = "" + scoreB;
    }
}
