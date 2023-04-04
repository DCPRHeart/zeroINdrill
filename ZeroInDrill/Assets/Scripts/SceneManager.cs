using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject LevelOne;
    //public GameObject LevelTwo;
    //public GameObject LevelThree;
    public TMPro.TMP_Text scoreLabel;
    public TMPro.TMP_Text shotsLabel;
    public TMPro.TMP_Text timeLabel;
    public TMPro.TMP_Text infoLabel;

    private GameObject playerPlatformObject;
    private GameObject playerObject;
    private PlatformMove platformScript;
    private GameObject currentLevelObject;
    private bool inGame = false;
    private bool inLevel = false;
    private float timeLeft = -1;
    private float levelCountdown = -1;
    private int shotsRemaining = -1;
    private int score = -1;
    private int scoreThreshold = -1;
    private int level = 1;

    void Start() {
        playerPlatformObject = Instantiate(Player, transform.position, transform.rotation);
        playerPlatformObject.transform.GetChild(1).gameObject.GetComponent<CameraMovement>().manager = this;
        platformScript = playerPlatformObject.transform.GetChild(0).gameObject.GetComponent<PlatformMove>();
        infoLabel.text = "Click to begin";
    }

    void Update() {
        if(inLevel) {
            if(timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                updateTimeLabel("Time: " + Mathf.FloorToInt(timeLeft));
            }
            else {
                endLevel();
            }
        }
        else {
            if(inGame) {

            }
            else {
                if (Input.GetKey(KeyCode.Mouse0)) {
                    inGame = true;
                    infoLabel.text = "Get Ready...";
                    StartCoroutine(setLevel(60f, 10, 100));
                }
            }
        }
    }

    IEnumerator setLevel(float totalTime, int totalShots, int levelScoreThreshold) {
        yield return new WaitForSeconds(2f);

        switch(level) {
            case 1:
                currentLevelObject = Instantiate(LevelOne, transform.position, transform.rotation);
                currentLevelObject.GetComponent<LevelOneManager>().manager = this;
                break;
            case 2:
                currentLevelObject = Instantiate(LevelOne, transform.position, transform.rotation);
                currentLevelObject.GetComponent<LevelOneManager>().manager = this;
                break;
        }
        
        infoLabel.text = "";
        timeLeft = totalTime;
        updateTimeLabel("Time: " + timeLeft);
        shotsRemaining = totalShots;
        updateShotsLabel("Shots: " + shotsRemaining);
        scoreThreshold = levelScoreThreshold;
        score = 0;
        updateScoreLabel("Score: " + score);
        inLevel = true;
    }

    public void endLevel() {
        inLevel = false;
        timeLeft = -1;
        updateTimeLabel("");
        shotsRemaining = -1;
        updateShotsLabel("");
        updateScoreLabel("");
        if(score >= scoreThreshold) {
            Debug.Log(platformScript.stage);
            platformScript.stage = new Vector3(platformScript.stage.x, platformScript.stage.y + 50, platformScript.stage.z);
            infoLabel.text = "Next Level...";
            //level++;
        }
        else {
            infoLabel.text = "Game Over!";
            level = 1;
            inGame = false;
        }
        score = -1;
        scoreThreshold = -1;
    }

    public bool getInGame() {
        return inLevel;
    }

    public void updateScore(int addScore) {
        score += addScore;
        updateScoreLabel("Score: " + score);
    }

    void updateScoreLabel(string text) {
        scoreLabel.text = text;
    }

    public void decrementShot() {
        shotsRemaining--;
        if(shotsRemaining > 0) {
            updateShotsLabel("Shots: " + shotsRemaining);
        }
        else {
            endLevel();
        }
    }

    void updateShotsLabel(string text) {
        shotsLabel.text = text;
    }

    void updateTimeLabel(string text) {
        timeLabel.text = text;
    }
}
