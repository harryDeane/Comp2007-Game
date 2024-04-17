using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JumpAudio : MonoBehaviour
{
    public GameObject ThisTrigger;
    public AudioSource JumpOverSound;
    public bool Action = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            
            Action = true;
        }
    }
    void OnTriggerExit(Collider collision)
    {
        
        Action = false;
    }
    // Update is called once per frame
    void Update()
    {
        
            if (Action == true)
            {
                ThisTrigger.SetActive(false);
                JumpOverSound.Play();
                Action = false;
            }
        

    }
}
