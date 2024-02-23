using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    /*
    public AudioSource source;
    public AudioClip talking;
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors) {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        EventBus.Instance.LevelLoadStart();

        Debug.Log("Started conversation! Loaded messages: " + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
    }

    void DisplayMessage() { 
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
        if(actorName.text == "Paws")
        {
            StartCoroutine("TalkingCat");
        }
        else
        {
            StartCoroutine("Talking");
        }

        AnimateTextColor();
    }

    public void NextMessage() {
        activeMessage++;
        StopCoroutine("Talking");
        StopCoroutine("TalkingCat");
        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
            
        }
        else {
            Debug.Log("Conversation ended!");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;
            EventBus.Instance.LevelLoadEnd();
        }
    }

    void AnimateTextColor() {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Fire1")&& isActive == true) || (Input.GetButtonDown("Interact")&& isActive == true))
        {
            NextMessage();
        }
    }
    IEnumerator Talking()
    {
        source.PlayOneShot(talking, .75f);
        yield return new WaitForSeconds(.05f);
        source.PlayOneShot(talking, .75f);
        yield return new WaitForSeconds(.05f);
        source.PlayOneShot(talking, .75f);
        yield return new WaitForSeconds(.05f);
        source.PlayOneShot(talking, .75f);
        yield return new WaitForSeconds(.05f);
        source.PlayOneShot(talking, .75f);
        yield return new WaitForSeconds(.05f);
        source.PlayOneShot(talking, .75f);
    }
    IEnumerator TalkingCat()
    {
        source.PlayOneShot(talking, .75f);
        yield return new WaitForSeconds(.05f);
        source.PlayOneShot(talking, .75f);
    }
    */
    //Dummy OpenDialogue function cause the rest of the code had to be commented out
        public void OpenDialogue(Message[] messages, Actor[] actors) {

    }
}