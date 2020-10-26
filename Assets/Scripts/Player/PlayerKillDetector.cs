using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillDetector : MonoBehaviour
{
    //A place holder class for killing a still Crewmember and should be changed in the full game
    public bool isAlive = true;
    public Sprite killedsprite;

    //Called when the kill UI Button is pressed
    public void Killed()
    {
        isAlive = false;
        GetComponent<Animator>().SetTrigger("Dead");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().sprite = killedsprite;
    }
}
