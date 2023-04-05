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
    public TMPro.TMP_Text levelLabel;
    public TMPro.TMP_Text gameOverLabel;
    public UnityEngine.UI.RawImage reticle;

    private GameObject playerPlatformObject;
    private GameObject playerObject;
    private PlatformMove platformScript;
    private GameObject currentLevelObject;
    private bool inGame = false;
    private bool inLevel = false;
    private float timeLeft = -1;
    private float levelTime = -1;
    private float levelCountdown = -1;
    private int shotsRemaining = -1;
    private int score = -1;
    private int scoreThreshold = -1;
    private int totalScore = 0;
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
        
        yield return new WaitForSeconds(2);

        infoLabel.text = "";
        score = 90;
        levelLabel.text = level.ToString();
        updateTimeLabel("Time: " + timeLeft);
        updateShotsLabel(shotsRemaining.ToString());
        updateScoreLabel(score.ToString());

        switch (level) {
            case 1:
                currentLevelObject = Instantiate(LevelOne, transform.position, transform.rotation);
                currentLevelObject.GetComponent<LevelOneManager>().manager = this;
                timeLeft = 60;
                levelTime = 60;
                shotsRemaining = 10;

                scoreThreshold = 100;

                gameOverLabel.text = "";
                StartCoroutine(setPlatformLevel());
                break;
            case 2:
                currentLevelObject = Instantiate(LevelOne, transform.position, transform.rotation);
                currentLevelObject.GetComponent<LevelOneManager>().manager = this;
                levelTime = 40;
                timeLeft = 40;
                shotsRemaining = 8;

                scoreThreshold = 120;

                gameOverLabel.text = "";
                StartCoroutine(setPlatformLevel());
                break;
            default:
                gameOverLabel.text = "STAGE COMPLETE\n\nSCORE: " + totalScore.ToString();
                break;
                
            
        }
        
    }

    private IEnumerator setPlatformLevel() {
        float iSpeed = platformScript.speed;
        float iLerp = platformScript.lerpConstant;
        platformScript.speed = 0.0001f;
        platformScript.lerpConstant = 0.025f;
        platformScript.stage = transform.position + Vector3.down * 1.25f;
        yield return new WaitUntil(platformScript.AtLevel);
        platformScript.speed = iSpeed;
        platformScript.lerpConstant = iLerp;
        inLevel = true;
    }

    public void endLevel() {
        inLevel = false;
        timeLeft = -1;
        inGame = false;
        score = -1;
        scoreThreshold = -1;
        updateTimeLabel("");
        shotsRemaining = -1;
        updateShotsLabel("");
        updateScoreLabel("");
        if(score >= scoreThreshold) {
            Debug.Log(platformScript.stage);
            infoLabel.text = "Next Level...";
            level++;
            transform.position += transform.up * 30;
            totalScore += score;
        }
        else {
            infoLabel.text = "";
            transform.position = Vector3.zero;
            gameOverLabel.text = "GAME OVER\n\nSCORE: " + totalScore.ToString();
            totalScore = 0;
            level = 1;
        }
        
    }

    public bool getInGame() {
        return inLevel;
    }

    public void updateScore(int addScore) {
        score += addScore;
        updateScoreLabel(score.ToString() + '/' + scoreThreshold.ToString());
    }

    void updateScoreLabel(string text) {
        scoreLabel.text = text;
    }

    public void decrementShot() {
        shotsRemaining--;
        if(shotsRemaining > 0) {
            updateShotsLabel(shotsRemaining.ToString());
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
