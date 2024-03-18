using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Cooking : MonoBehaviour
{
    private float elapsedTime;
    private float timeLimit = 10f;
    private float pauseElapsedTime;
    private float pauseTimeLimit = 2f;

    private bool pausing = true;

    public GameObject egg;
    public GameObject tin;
    public GameObject spatula;
    private Sprite result;

    public Sprite cake;
    public Sprite friedEgg;
    public Sprite lasagna;
    public Sprite tiramisu;

    private bool selectEgg;
    private bool selectTin;
    private bool selectSpatula;

    private int gameStage = 1;
    private int score;

    public TMP_Text scoreDisplay;
    public AudioSource victorySound;
    public AudioSource errorSound;
    public AudioSource gameOverSound;

    void Start()
    {
        result = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        if (!pausing)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > timeLimit)
            {
                errorSound.Play();

                NextItem();
            }
        }
        else
        {
            pauseElapsedTime += Time.deltaTime;

            if (pauseElapsedTime > pauseTimeLimit)
            {
                NextItem();
            }
        }

        HandleInput();
        ScaleImage();
    }

    private void StartRound()
    {
        pausing = false;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!selectEgg)
            {
                selectEgg = true;
                Debug.Log("Selected egg.");
            }
            else selectEgg = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!selectTin)
            {
                selectTin = true;
                Debug.Log("Selected tin.");
            }
            else selectTin = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!selectSpatula)
            {
                selectSpatula = true;
            }
            else selectSpatula = false;
        }
    }

    private void PlayItem()
    {
        if (result == cake)
        {
            if (selectEgg && selectTin)
            {
                score += 1;
                pausing = true;
            }
        }
        else if (result == friedEgg)
        {
            if (selectEgg && selectSpatula)
            {
                score += 1;
                pausing = true;
            }
        }
        else if (result == lasagna)
        {
            if (selectSpatula && selectTin)
            {
                score += 1;
                pausing = true;
            }
        }
        else if (result == tiramisu)
        {
            if (selectEgg && selectTin && selectSpatula)
            {
                score += 1;
                pausing = true;
            }
        }
    }

    private void ChangeSprite(Sprite sprite)
    {
        result = sprite;
    }

    private void NextItem()
    {
        elapsedTime = 0;
        pauseElapsedTime = 0;

        gameStage += 1;

        switch (gameStage)
        {
            case 1:
                ChangeSprite(cake);
                break;
            case 2:
                ChangeSprite(friedEgg);
                break;
            case 3:
                ChangeSprite(lasagna);
                break;
            case 4:
                ChangeSprite(tiramisu);
                break;
            default:
                break;
        }
    }
    

    private void ScaleImage()
    {
        // scale to time

        float scaledAxis = elapsedTime / 2;

        transform.localScale = new Vector3(scaledAxis, scaledAxis, scaledAxis);
    }

    private void UpdateScore()
    {
        scoreDisplay.text = score.ToString();
    }

    private void GameOverScreen()
    {

    }
}
