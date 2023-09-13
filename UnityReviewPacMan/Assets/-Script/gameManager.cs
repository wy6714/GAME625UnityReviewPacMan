using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public AudioSource clickPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playButton()
    {
        clickPlay.Play();
        Invoke("goScene02", 1.1f);
    }

    public void goScene02()
    {
        SceneManager.LoadScene("scene02");
    }

    public void playAgainButton()
    {
        clickPlay.Play();
        Invoke("goScene01", 1.1f);
    }

    public void goScene01()
    {
        SceneManager.LoadScene("scene01");
    }




}
