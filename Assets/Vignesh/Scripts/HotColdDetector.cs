using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotColdDetector : MonoBehaviour
{
    [Header("Game Objects")]
    public Transform target;
    public Transform player;

    [Header("Distance")]
    public float maxDistance = 42f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip beepClip;

    [Header("UI")]
    public Image detector;
    public TextMeshProUGUI textDetector;

    //time left till next beep
    private float beepTimer;

    //used for speed of the beep sound
    public float coldBeep = 1.2f;
    public float hotBeep = 0.22f;


    // Update is called once per frame
    void Update()
    {
        //makes sure that the target and player are present
        if(target == null || player == null){
            return;
        }

        //checks if the target pickup has been collected
        //If so, then make the beeping stop
        if(!target.gameObject.activeInHierarchy){
            return;
        }

        Vector2 targetPos = new Vector2(target.position.x, target.position.z);
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);

        float distance = Vector2.Distance(playerPos, targetPos);

        //checks how close the player is to the target (from 0 to 1)
        float proximity = Mathf.InverseLerp(maxDistance, 0f, distance);

        UpdateBeeping(proximity);
        UpdateUI(proximity);
    }

    void UpdateBeeping(float proximity){
        //used to determine the beep speed
        float beepInterval = Mathf.Lerp(coldBeep, hotBeep, proximity);

        //decrease the beep timer
        beepTimer -= Time.deltaTime;

        //checks if the beep countdown is done
        if(beepTimer <= 0f){
            if(audioSource != null && beepClip != null){
                //Plays the beep clip one time
                audioSource.PlayOneShot(beepClip);
            }
            // resets the beep timer
            beepTimer = beepInterval;
        }
    }

    void UpdateUI(float proximity){
        //if the detector is available, it will update the UI color
        // based on the proximity
        if(detector != null){
            //target is far from the player
            if(proximity < 0.35f){
                detector.color = Color.blue;
            }
            //target is moderately close to the player
            else if(proximity < 0.7f){
                detector.color = new Color(1f, 0.6f, 0f);
            }
            //target is very close to the player
            else{
                detector.color = Color.red;
            }
        }
        //if the textDetector is available, it will update the UI text
        // based on the proximity
        if(textDetector != null){
            //target is far from the player
            if(proximity < 0.35f){
                textDetector.text = "Cold";
            }
            //target is moderately close to the player
            else if(proximity < 0.7f){
                textDetector.text = "Warm";
            }
            //target is very close to the player
            else{
                textDetector.text = "Hot";
            }
        }
    }
}
