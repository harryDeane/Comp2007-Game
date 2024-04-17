using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PressKeyOpenDoor : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject AnimeObject;
    public GameObject ThisTrigger;
    public AudioSource DoorOpenSound;
    public bool Action = false;

    void Start()
    {
        //instruction is hidden
        Instruction.SetActive(false);

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            //Instruction is displayed
            Instruction.SetActive(true);
            Action = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                Instruction.SetActive(false);
                //Plays the door open animation
                AnimeObject.GetComponent<Animator>().Play("DoorOpen");
                ThisTrigger.SetActive(false);
                DoorOpenSound.Play();
                Action = false;
            }
        }

    }
}
