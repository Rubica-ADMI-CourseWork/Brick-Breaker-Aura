using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// controls paddle movement by reading touch input on
/// the screen.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    public Text debugText;
    [SerializeField]GameObject swipeTrail;
    [SerializeField] float swipeTrailZPos;
    [SerializeField] GameObject theBall;
    [SerializeField] float ballLaunchForce;
    [SerializeField] Transform[] ballSpawnPos;
    [SerializeField] bool controllerIsEnabled = true;
    Touch theTouch;
    Vector3 currentTouchStartPos;
    Vector3 currentTouchEndPos;

    Gamemanager gameManager;
    GameObject[] ballsThisRound;

    private void Awake()
    {
        //cache the gameManager ref
        gameManager = FindObjectOfType<Gamemanager>();
        ballsThisRound = new GameObject[gameManager.GetBallCountPaRound()];
    }

    private void Start()
    {
        LoadBallCannon();
    }

    private void LoadBallCannon()
    {
        for (int i = 0; i < gameManager.GetBallCountPaRound(); i++)
        {
            ballsThisRound[i] = Instantiate(theBall, ballSpawnPos[i].position, Quaternion.identity);
        }

        //deactivate them
        foreach (var ball in ballsThisRound)
        {
            ball.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.touchCount > 0 && controllerIsEnabled)
        {
            theTouch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(theTouch.position);
            //perform different action based on phase of the touch
            switch (theTouch.phase)
            {
                case TouchPhase.Began:
                    debugText.text = "Inside " + GamePhase.PREP.ToString();
                    currentTouchStartPos = touchPos;
                    break;
                case TouchPhase.Moved:
                   // debugText.text = "Inside " + GamePhase.SWIPING.ToString();
                    swipeTrail.transform.position = new Vector3(touchPos.x, touchPos.y, transform.position.z);
                    break;
                case TouchPhase.Ended:
                    debugText.text = "Inside " + GamePhase.ACTION.ToString();
                    currentTouchEndPos = touchPos;
                    LaunchBall();
                    break;
            }

        }
    }

    private void LaunchBall()
    {
        //get direction of launch.
        Vector3 launchDirection = currentTouchEndPos - currentTouchStartPos;

        //instantiate the balls based on game manager rules
        //add a force to it to launch it
        foreach(var ball in ballsThisRound)
        {
            ball.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(launchDirection.x, launchDirection.y) * ballLaunchForce);
        }
     
        //disable all touch
        controllerIsEnabled = false;
        swipeTrail.GetComponent<TrailRenderer>().enabled = false;
        StartCoroutine(ResetController());

    }

    private IEnumerator ResetController()
    {
        yield return new WaitForSeconds(3f);
        debugText.text = "Can Swipe";
        LoadBallCannon();
        controllerIsEnabled = true;
        swipeTrail.transform.position = transform.position;
        swipeTrail.GetComponent<TrailRenderer>().enabled = true;

    }
}
