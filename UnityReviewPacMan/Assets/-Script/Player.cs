using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Vector3 targetPosition = new Vector3(1.4f, 0f, -16f);
    private Vector3 enemyTargetPos = new Vector3(0f, 0f, 0f);

    private CharacterController characterController;

    private float frezeDuration = 5f;

    [SerializeField]private float speed = 10f;

    private int score = 0;
    private int pelletPoint = 10;
    private int powerUpPelletPoint = 100;
    private int lives = 3;

    private bool enemyFrozen = false;
    private bool canMove = true;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI livesText;

    public AudioSource collectSound;
    public AudioSource deadedSound;
    public AudioSource powerUpSound;
    //private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (lives < 0)
        {
            SceneManager.LoadScene("loss");
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * speed * Time.deltaTime);

        //rotate while move
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0.0f, 45f, 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0.0f, -45f, 0.0f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0.0f, 0f, 0.0f);
        }

        if (enemyFrozen)
        {
            transform.localScale = new Vector3(2f, 2f, 2f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        ScoreText.text = "score: " + score.ToString();

        livesText.text = "lives: " + lives.ToString();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pellet"))//player get pellet
        {
            Destroy(other.gameObject);
            collectSound.Play();
            score += pelletPoint;
            
            //Debug.Log(score);
        }

        if (other.CompareTag("powerUpPellet"))//player get power up pellet
        {
            powerUpSound.Play();
            Destroy(other.gameObject);
            score += powerUpPelletPoint;
            //ScoreText.text = "score: " + score.ToString();
            StartCoroutine(FreezeForDuration());

            Debug.Log(score);
        }

        if (other.CompareTag("enemy") && !enemyFrozen) //player eaten by enemy
        {
            deadedSound.Play();
            StartCoroutine(DelayedPositionChange(targetPosition));
            lives -= 1;
            Debug.Log("lives -1");
        }
        else if (other.CompareTag("enemy") && enemyFrozen)//enemy eaten by player
        {
            collectSound.Play();
            other.gameObject.transform.position = enemyTargetPos;
            score += 200;
            //ScoreText.text = "score: " + score.ToString();
            Debug.Log("eat enemy");
        }

    }

    private IEnumerator FreezeForDuration()
    {
        enemyFrozen = true;
        yield return new WaitForSeconds(frezeDuration);
        enemyFrozen = false;


    }
    private IEnumerator DelayedPositionChange(Vector3 newPosition)
    {
        yield return new WaitForSeconds(0.1f); 
        gameObject.transform.position = newPosition;
    }

  
    


}
