using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Transform player;
    public Transform cameraOriginalPos;
    public Transform cameraTargetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z > -3)
        {
            transform.position = Vector3.Lerp(transform.position, cameraTargetPos.position, Time.deltaTime * 2f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, cameraOriginalPos.position, Time.deltaTime * 2f);
        }
    }
}
