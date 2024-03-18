using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Cooking : MonoBehaviour
{
    private float elapsedTime;
    private float timeLimit = 10f;

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

    private int gameStage;
    private int score;

    public TMP_Text scoreDisplay;
    public TMP_Text timeDisplay;

    public AudioSource victorySound;
    public AudioSource errorSound;
    public AudioSource gameOverSound;

    void Start()
    {
        NextItem();
    }

    void Update()
    {
        HandleInput();

        result = GetComponent<SpriteRenderer>().sprite;

        elapsedTime += Time.deltaTime;

        if (elapsedTime > timeLimit)
        {
            Debug.Log("Fail!");

            // errorSound.Play();

            NextItem();
        }

        PlayItem();
        ShowTimer();
        // ScaleImage();
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

                egg.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                selectEgg = false;

                egg.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!selectTin)
            {
                selectTin = true;

                tin.GetComponent<SpriteRenderer>().color = Color.red;

            }
            else
            {
                selectTin = false;

                tin.GetComponent<SpriteRenderer>().color = Color.white;

            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!selectSpatula)
            {
                selectSpatula = true;

                spatula.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                selectSpatula = false;

                spatula.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void PlayItem()
    {
        if (result == cake)
        {
            if (selectEgg && selectTin)
            {
                score += 1;

                Debug.Log("Good job!");


                NextItem();


                pausing = true;
            }
        }
        else if (result == friedEgg)
        {
            if (selectEgg && selectSpatula)
            {
                score += 1;

                Debug.Log("Good job!");

                NextItem();

                pausing = true;
            }
        }
        else if (result == lasagna)
        {
            if (selectSpatula && selectTin)
            {
                score += 1;

                Debug.Log("Good job!");

                NextItem();

                pausing = true;

            }
        }
        else if (result == tiramisu)
        {
            if (selectEgg && selectTin && selectSpatula)
            {
                score += 1;

                Debug.Log("Good job!");


                NextItem();

                pausing = true;
            }
        }

        UpdateScore();
    }

    private void ChangeSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void NextItem()
    {
        selectEgg = false;
        selectTin = false;
        selectSpatula = false;

        egg.GetComponent<SpriteRenderer>().color = Color.white;
        tin.GetComponent<SpriteRenderer>().color = Color.white;
        spatula.GetComponent<SpriteRenderer>().color = Color.white;

        Debug.Log("Next item:");

        elapsedTime = 0;

        gameStage += 1;

        switch (gameStage)
        {
            case 1:
                Debug.Log("Make a cake.");
                ChangeSprite(cake);
                break;
            case 2:
                Debug.Log("Fry an egg.");
                ChangeSprite(friedEgg);
                break;
            case 3:
                Debug.Log("Oven a lasagna.");
                ChangeSprite(lasagna);
                break;
            case 4:
                Debug.Log("Make tiramisu!");
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

    private void ShowTimer()
    {
        timeDisplay.text = (timeLimit - elapsedTime).ToString();
    }
}
