using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    private Vector3 targetPosition = new Vector3(1.4f, 0f, -16f);
    private Vector3 hidePosition = new Vector3(100f, 0f, 100f);

    private CharacterController characterController;

    private float frezeDuration = 5f;
    private float deathDuration = 2f;

    [SerializeField]private float speed = 10f;

    private int score = 0;
    private int pelletPoint = 10;
    private int powerUpPelletPoint = 100;
    private int lives = 3;

    private bool enemyFrozen = false;

    //private Vector3 originalScale;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
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

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pellet"))//player get pellet
        {
            Destroy(other.gameObject);
            score += pelletPoint;
            Debug.Log(score);
        }

        if (other.CompareTag("powerUpPellet"))//player get power up pellet
        {
            Destroy(other.gameObject);
            score += powerUpPelletPoint;
            StartCoroutine(FreezeForDuration());

            Debug.Log(score);
        }

        if (other.CompareTag("enemy") && !enemyFrozen) //player eaten by enemy
        {
            StartCoroutine(playerDeath());
            lives -= 1;
            Debug.Log("lives -1");
        }
        else if (other.CompareTag("enemy") && enemyFrozen)//enemy eaten by player
        {
            Destroy(other.gameObject);
            score += 200;
            Debug.Log("eat enemy");
        }

    }

    private IEnumerator FreezeForDuration()
    {
        enemyFrozen = true;
        yield return new WaitForSeconds(frezeDuration);
        enemyFrozen = false;


    }
    private IEnumerator playerDeath()
    {
        yield return new WaitForSeconds(0.01f);
        transform.position = hidePosition;
        
        yield return new WaitForSeconds(deathDuration);
        
        transform.position = targetPosition;
        
    }



}
