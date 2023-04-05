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
    private bool waiting = false;
    private float timeLeft = -1;
    private float levelTime = -1;
    private float levelCountdown = -1;
    private int shotsRemaining = -1;
    private int score = -1;
    private int scoreThreshold = -1;
    private int totalScore = 0;
    private int level = 1;

    private AudioSource music;

    void Start() {
        music = GetComponent<AudioSource>();
        playerPlatformObject = Instantiate(Player, transform.position, transform.rotation);
        playerPlatformObject.transform.GetChild(1).gameObject.GetComponent<CameraMovement>().manager = this;
        platformScript = playerPlatformObject.transform.GetChild(0).gameObject.GetComponent<PlatformMove>();
        infoLabel.text = "Click to begin";
    }

    void Update() {
        if (waiting && Input.GetKeyDown(KeyCode.Mouse0))
            waiting = false;
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
        StartCoroutine(setPlatformLevel());
        yield return new WaitForSeconds(3f);
        gameOverLabel.text = "";
        if (level == 1 && !music.isPlaying){
            music.Play();
        }
        currentLevelObject = Instantiate(LevelOne);
        currentLevelObject.transform.position = transform.position;
        currentLevelObject.GetComponent<LevelOneManager>().manager = this;
        currentLevelRing = Instantiate(LevelRing);
        currentLevelRing.transform.position = playerPlatformObject.transform.GetChild(1).transform.position;
        infoLabel.text = "";
        scoreThreshold = levelScoreThreshold;
        timeLeft = totalTime;
        score = 0;
        updateTimeLabel("Time: " + timeLeft);
        shotsRemaining = totalShots;
        updateShotsLabel(shotsRemaining.ToString());
        
        updateScoreLabel("Score: " + score);
        StartCoroutine(nextPrompt());
        
    }
    IEnumerator nextPrompt(string txt = "Click to Begin")
    {
        gameOverLabel.text = txt;
        waiting = true;
        yield return new WaitUntil(notWaiting);
        gameOverLabel.text = "3";
        yield return new WaitForSeconds(1);
        gameOverLabel.text = "2";
        yield return new WaitForSeconds(1);
        gameOverLabel.text = "1";
        yield return new WaitForSeconds(1);
        gameOverLabel.text = "";
        inLevel = true;
    }
    bool notWaiting()
    {
        return !waiting;
    }

    private IEnumerator setPlatformLevel() {
        float iSpeed = platformScript.speed;
        float iLerp = platformScript.lerpConstant;
        platformScript.speed = 0.0001f;
        platformScript.lerpConstant = 0.025f;
        platformScript.stage = transform.position + Vector3.down * 1.5f;
        yield return new WaitUntil(platformScript.AtLevel);
        platformScript.speed = iSpeed;
        platformScript.lerpConstant = iLerp;
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
            if (currentLevelObject != null)
            {
                currentLevelObject.GetComponent<LevelOneManager>().deleteLevel();
                Destroy(currentLevelObject.gameObject);
                Destroy(currentLevelRing.gameObject);
            }
            this.transform.position += transform.up*50;
            totalScore += score;
            level++;
            switch (level) {
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
                gameOverLabel.text = "Stage Complete!\n\n Score: " + totalScore.ToString();
                break;
            }
        }
        else {
            music.Stop();
            currentLevelObject.GetComponent<LevelOneManager>().deleteLevel();
            Destroy(currentLevelObject.gameObject);
            Destroy(currentLevelRing.gameObject);
            infoLabel.text = "";
            gameOverLabel.text = "Game Over! \nClick to Play Again\n\n Score: " + totalScore.ToString();
            level = 1;
            this.transform.position = Vector3.zero;
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
