using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    public GameObject particleSys;
    private bool isAlive;
    public GameObject restartButton;
    public int score = 0;
    public Text scoreText;
    public Animator gameOverAnim;
    public Text currentScore;
    public Text bestText;
    public Image background;
    public LayerMask ground;
    private bool isPlaying = false;
    public Transform contactPoint;


    void Start()
    {
        //set player as alive when game starts
        isAlive = true;
        direction = Vector3.zero; //first start at zero position
    }

    void Update()
    {

        if (!IsGrounded() && isPlaying)
        {
            //if player fall down kill it and call the game over function to show end game menu and show play again button
            isAlive = false;
            GameOver();
            restartButton.SetActive(true);

            //stop camera to follow palyer when dead.
            if(transform.childCount > 0)
            {
                transform.GetChild(0).transform.parent = null;
            }
        }

        //change the direction of the player when a touch input occurs
        if (Input.GetMouseButtonDown(0) && isAlive)
        {
            isPlaying = true;
            score++;
            scoreText.text = score.ToString();
            if(direction == Vector3.forward)
            {
                direction = Vector3.left;
            }
            else
            {
                direction = Vector3.forward;
            }
        }

        //player move
        transform.Translate(direction * speed * Time.deltaTime);
    }

    //when player triggers a point object destroy that object, show particles and increase score by two and show that on screen
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Point")
        {
            other.gameObject.SetActive(false);
            Instantiate(particleSys, transform.position, Quaternion.identity);
            score+=2;
            scoreText.text = score.ToString();
        }
    }


    //this function is trigger our animation to show menu. also we handle best score text. if your new score when you are dead is bigger than the last best score set the new score as the new best score
    private void GameOver()
    {
        gameOverAnim.SetTrigger("GameOver");
        currentScore.text = score.ToString();
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if(score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore",score);
            background.color = new Color32(255,118,246,255);
        }
        bestText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }


    //to handle game over condition. we added a contact point for our player and when contact point is collide with gound return true else return false
    private bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(contactPoint.position, .5f, ground);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
}


