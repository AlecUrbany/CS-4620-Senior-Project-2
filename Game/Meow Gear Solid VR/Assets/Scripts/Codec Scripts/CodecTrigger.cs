using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodecTrigger : MonoBehaviour
{
    public AudioSource source;
    public AudioClip callSound;
    public AudioClip pickUpSound;
    public GameObject callButton;
    
    public bool isCalling;

    public bool hasCalled;
    /*[SerializeField] private Image Image;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Image.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Image.enabled = false;
        }
    }*/
    public DialogueTrigger trigger;

    private void  OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true && hasCalled == false)
        {
            hasCalled = true;
            isCalling = true;
            callButton.SetActive(true);
            StartCoroutine("Timeout");
        }

    }
    public void Update()
    {
            if(Input.GetButtonDown("Codec") && isCalling == true)
            {
                callButton.SetActive(false);
                StopCoroutine("Timeout");
                source.PlayOneShot(pickUpSound, 1f);
                trigger.StartDialogue();
                isCalling = false;
                
            }
    }
    public void Start()
    {
        source = GameObject.FindWithTag("CodecFunction").GetComponent<AudioSource>();
        callButton.SetActive(false);
        hasCalled = false;
        isCalling = false;

    }
    IEnumerator Timeout()
    {
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(1.2f);
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(1.2f);
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(3f);
        callButton.SetActive(false);
        isCalling = false;
    }
}
