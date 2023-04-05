using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject LevelRing;
    public GameObject LevelOne;
    public TMPro.TMP_Text scoreLabel;
    public TMPro.TMP_Text shotsLabel;
    public TMPro.TMP_Text timeLabel;
    public TMPro.TMP_Text infoLabel;
    public TMPro.TMP_Text levelLabel;
    public TMPro.TMP_Text gameOverLabel;
    public UnityEngine.UI.RawImage reticle;

    private GameObject playerPlatformObject;
    private PlatformMove platformScript;
    private GameObject currentLevelObject;
    private GameObject currentLevelRing;
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
                StartCoroutine(endLevel());
            }
        }
        else {
            if(inGame) {

            }
            else {
                if (Input.GetKey(KeyCode.Mouse0)) {
                    level = 1;
                    platformScript.stage = new Vector3(0, 0, 0);
                    inGame = true;
                    StartCoroutine(setLevel(60f, 10, 100));
                }
            }
        }
    }

    IEnumerator setLevel(float totalTime, int totalShots, int levelScoreThreshold) {
        infoLabel.text = "Level " + level + ":\n Get " + levelScoreThreshold + " points to advance";
        yield return new WaitForSeconds(5f);

        switch(level) {
            case 1:
                currentLevelObject = Instantiate(LevelOne);
                break;
            case 2:
                currentLevelObject = Instantiate(LevelOne);
                break;
            case 3:
                currentLevelObject = Instantiate(LevelOne);
                break;
            case 4:
                currentLevelObject = Instantiate(LevelOne);
                break;
            case 5:
                currentLevelObject = Instantiate(LevelOne);
                break;
        }

        currentLevelObject.transform.position = playerPlatformObject.transform.GetChild(1).transform.position;
        currentLevelObject.GetComponent<LevelOneManager>().manager = this;
        currentLevelRing = Instantiate(LevelRing);
        currentLevelRing.transform.position = playerPlatformObject.transform.GetChild(1).transform.position;
        infoLabel.text = "";
        timeLeft = totalTime;
        updateTimeLabel("Time: " + timeLeft);
        shotsRemaining = totalShots;
        updateShotsLabel("Shots: " + shotsRemaining);
        scoreThreshold = levelScoreThreshold;
        score = 0;
        updateScoreLabel("Score: " + score);
                
            
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

    public IEnumerator endLevel() {
        inLevel = false;
        timeLeft = -1;
        updateTimeLabel("");
        shotsRemaining = -1;
        updateShotsLabel("");
        updateScoreLabel("");
        infoLabel.text = "Level Over!";
        yield return new WaitForSeconds(1f);
        
        if(score >= scoreThreshold) {
            currentLevelObject.GetComponent<LevelOneManager>().deleteLevel();
            Destroy(currentLevelObject.gameObject);
            Destroy(currentLevelRing.gameObject);
            platformScript.stage = new Vector3(platformScript.stage.x, platformScript.stage.y + 50, platformScript.stage.z);
            level++;
            switch(level) {
            case 1:
                StartCoroutine(setLevel(60f, 10, 150));
                break;
            case 2:
                StartCoroutine(setLevel(60f, 10, 150));
                break;
            case 3:
                StartCoroutine(setLevel(60f, 8, 150));
                break;
            case 4:
                StartCoroutine(setLevel(60f, 10, 200));
                break;
            case 5:
                StartCoroutine(setLevel(60f, 8, 200));
                break;
            case 6:
                inGame = false;
                infoLabel.text = "You Win!\nClick to Play Again";
                break;
            }
        }
        else {
            currentLevelObject.GetComponent<LevelOneManager>().deleteLevel();
            Destroy(currentLevelObject.gameObject);
            Destroy(currentLevelRing.gameObject);
            infoLabel.text = "Game Over! \nClick to Play Again";
            level = 1;
            platformScript.stage = new Vector3(0, 0, 0);
            inGame = false;
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
            StartCoroutine(endLevel());
        }
    }

    void updateShotsLabel(string text) {
        shotsLabel.text = text;
    }

    void updateTimeLabel(string text) {
        timeLabel.text = text;
    }
}
