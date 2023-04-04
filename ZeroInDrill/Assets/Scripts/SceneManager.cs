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

    private GameObject playerObject;
    private GameObject currentLevelObject;
    private bool inGame = false;
    private float timeLeft = -1;
    private int shotsRemaining = -1;
    private int score = -1;
    private int scoreThreshold = -1;

    void Start() {
        playerObject.GetComponent<CameraMovement>().manager = this;
        currentLevelObject = Instantiate(LevelOne, transform.position, transform.rotation);
        currentLevelObject.GetComponent<LevelOneManager>().manager = this;
        setLevel(60f, 10, 100);
    }

    void Update() {
        if(inGame) {
            if(timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                updateTimeLabel("Time: " + Mathf.FloorToInt(timeLeft));
            }
            else {
                endLevel();
            }
        }
    }

    void setLevel(float totalTime, int totalShots, int levelScoreThreshold) {
        timeLeft = totalTime;
        updateTimeLabel("Time: " + timeLeft);
        shotsRemaining = totalShots;
        updateShotsLabel("Shots: " + shotsRemaining);
        scoreThreshold = levelScoreThreshold;
        score = 0;
        updateScoreLabel("Score: " + score);
        inGame = true;
    }

    public void endLevel() {
        inGame = false;
        timeLeft = -1;
        updateTimeLabel("");
        shotsRemaining = -1;
        updateShotsLabel("");
        if(score >= scoreThreshold) {
            //next level code
        }
        else {
            //game over code
        }
        score = -1;
        updateScoreLabel("");
        scoreThreshold = -1;
    }

    public bool getInGame() {
        return inGame;
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
