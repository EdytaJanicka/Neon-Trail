using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


public class OneScript : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;

    public GameObject player;
    private Rigidbody2D _rigidbody; // end of player movement variables

    public List<GameObject> platform;
    public List<GameObject> platformVertical;

    public List<GameObject> platform2;

    public GameObject platformVertica2;

    private Vector2 startPosition;
    private Vector2 newPosition;

    private Vector2 startPosition_1;
    private Vector2 newPosition_1;

    private Vector2 startPosition_2;
    private Vector2 newPosition_2;

    private Vector2 startPosition_3;
    private Vector2 newPosition_3;


    private Vector2 startPosition2;
    private Vector2 newPosition2;

    private Vector2 startPosition2_1;
    private Vector2 newPosition2_1;

    private Vector2 startPositionVer;
    private Vector2 newPositionVer;

    private Vector2 startPositionVer_1;
    private Vector2 newPositionVer_1;


    private Vector2 startPositionVer2;
    private Vector2 newPositionVer2;

    [SerializeField] private int speedPlatform = 3;
    [SerializeField] private float maxDistance = 0.5f;

    [SerializeField] private Vector3 velocity;  // end of platform1 movement variables


    public AudioSource sound;
    public AudioSource checkSound;
    public AudioSource loseSound;
    public AudioMixer mixer; // sound variables

    public Vector2 checkpointPos;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    public GameObject checkpoint4; // checkpoints

    public CanvasGroup panel;
    public CanvasGroup panel2;
    public GameObject panelOuf;
    public GameObject credits;

    public GameObject menu;
    private bool isMenuOpen = false;
    public Animator animator;
    
    public TextMeshProUGUI text;
    float theTime;
    public float speed = 1;
    public bool playing = false;
    public int playing1 = 0;

    //ui 

    public GameObject shutle1;
    public GameObject shutle2;
    public GameObject[] tags;
    // corretcing bugs/gameplay
    public GameObject lose;
    public Vector2 firstOne;
    public Vector2 secondOne;


    void Start()
    {
        _rigidbody = player.GetComponent<Rigidbody2D>();
        //``````````````player initiating````````````
        
        startPosition = platform[0].transform.position;
        newPosition = platform[0].transform.position;

        startPosition_1 = platform[1].transform.position;
        newPosition_1 = platform[1].transform.position;

        startPosition_2 = platform[2].transform.position;
        newPosition_2 = platform[2].transform.position;

        startPosition_3 = platform[3].transform.position;
        newPosition_3 = platform[3].transform.position;

        startPositionVer = platformVertical[0].transform.position;
        newPositionVer = platformVertical[0].transform.position;

        startPosition2 = platform2[0].transform.position;
        newPosition2 = platform2[0].transform.position;

        startPosition2_1 = platform2[1].transform.position;
        newPosition2_1 = platform2[1].transform.position;

        startPositionVer_1 = platformVertical[1].transform.position;
        newPositionVer_1 = platformVertical[1].transform.position;



        startPositionVer2 = platformVertica2.transform.position;
        newPositionVer2 = platformVertica2.transform.position;

        //```````````````platform intiating`````````````
        FadeOut();
        StartCoroutine(FadeCanvasGroup(panel2, panel2.alpha, 0f, 1f));
        Invoke("CanvasSetDesactive", 1f);
        menu.SetActive(false);
        credits.SetActive(false);
        firstOne = new Vector2(0f, 3.6f);
        secondOne = new Vector2(0f, 7f);

    }



    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        player.transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
        //`````````````````````players movement````````````````````````````
        
            newPosition.x = startPosition.x + (maxDistance * Mathf.Sin(Time.time * speedPlatform));
            platform[0].transform.position = newPosition;

        newPosition_1.x = startPosition_1.x + (maxDistance * Mathf.Sin(Time.time * speedPlatform));
        platform[1].transform.position = newPosition_1;

        newPosition_2.x = startPosition_2.x + (maxDistance * Mathf.Sin(Time.time * speedPlatform));
        platform[2].transform.position = newPosition_2;

        newPosition_3.x = startPosition_3.x + (maxDistance * Mathf.Sin(Time.time * speedPlatform));
        platform[3].transform.position = newPosition_3;

        newPosition2.x = startPosition2.x + (-maxDistance * Mathf.Sin(Time.time * speedPlatform));
        platform2[0].transform.position = newPosition2;

        newPosition2_1.x = startPosition2_1.x + (-maxDistance * Mathf.Sin(Time.time * speedPlatform));
        platform2[2].transform.position = newPosition2_1;

        newPositionVer.y = startPositionVer.y + (maxDistance * Mathf.Sin(Time.time * speedPlatform));
        platformVertical[0].transform.position = newPositionVer;

        newPositionVer_1.y = startPositionVer_1.y + (maxDistance * Mathf.Sin(Time.time * speedPlatform));
        platformVertical[1].transform.position = newPositionVer_1;

        newPositionVer2.y = startPositionVer2.y + (-maxDistance * Mathf.Sin(Time.time * speedPlatform));
        platformVertica2.transform.position = newPositionVer2;

        //``````````````````````platform movement``````````````````````````
        if (sound.isPlaying == false && Mathf.Abs(_rigidbody.velocity.y) < 0.001f && isMenuOpen == false)
        {
            sound.Play();
        }
        else if (Mathf.Abs(_rigidbody.velocity.y) > 0.001f)
        {
            sound.Stop();
        }


        //``````````````````````sound script`````````````````````````````

        if (Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            Destroy(shutle1);
            Destroy(shutle2);
            playing = true;
        }
        if(playing == true && playing1 == 0)
        {
            theTime += Time.deltaTime * speed;
            string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
            text.text = hours + ":" + minutes + ":" + seconds;

        }


        if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen == false)
        {
            Time.timeScale = 0.0001f;
            menu.SetActive(true);
            isMenuOpen = true;
            sound.Stop();
            Cursor.visible = true;

        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isMenuOpen == true)
        {
            Time.timeScale = 1f;
            menu.SetActive(false);
            isMenuOpen = false;
            Cursor.visible = false;

        }
        // showing menu
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            this.transform.parent = col.transform;
        }

        if (col.gameObject.tag == "JumpPlatform")
        {
            JumpForce = 5;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }

            JumpForce = 2;
        

        if (col.gameObject.tag == "Platforms")
        {
            StartCoroutine(PlatformDisable());
            col.gameObject.SetActive(false);
            
        }
    }

    // ```````````````````````moving with platform`````````````````````

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint1"))
        {
            checkpointPos = checkpoint1.transform.position;
        }

        if (other.CompareTag("Checkpoint2"))
        {
            checkpointPos = checkpoint2.transform.position;
            checkSound.Play();
            checkpoint1.GetComponent<BoxCollider2D>().enabled = false;
            checkpoint2.GetComponent<BoxCollider2D>().enabled = false;

        }
        if (other.CompareTag("Checkpoint3"))
        {
            checkpointPos = checkpoint3.transform.position;
            checkSound.Play();
            checkpoint3.GetComponent<BoxCollider2D>().enabled = false;
            lose.transform.position = firstOne;

        }
        if (other.CompareTag("Checkpoint4"))
        {
            checkpointPos = checkpoint4.transform.position;
            checkSound.Play();
            lose.transform.position = secondOne;
            checkpoint4.GetComponent<BoxCollider2D>().enabled = false;

        }
        if (other.CompareTag("End"))
        {
            playing1 = 1;
            checkSound.Play();
            Invoke("FadeIn3", 2f);
            Invoke("Menu", 5f);
        }

        if (other.CompareTag("Lose"))
        {
            player.transform.position = checkpointPos;
            loseSound.Play();
            FadeIn();
            Invoke("FadeOut", 1f);
            foreach (GameObject tag in tags)
            {
                tag.SetActive(true);
            }
        }
    }
    IEnumerator PlatformDisable()
    {
        yield return new WaitForSeconds(1);

    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(panel, 1f, panel.alpha, 1f));
    }
    public void FadeIn3()
    {
        StartCoroutine(FadeCanvasGroup(panel, panel.alpha, 1f, 1f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(panel, panel.alpha, 0, .5f));
    }

    public void FadeIn2()
    {
        StartCoroutine(FadeCanvasGroup(panel, panel.alpha, 1f, 1f));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }

    }

    public void Back()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
        isMenuOpen = false;
        Cursor.visible = false;

    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
        Cursor.visible = true
            ;

    }

    public void StartGame()
    {
        animator.SetTrigger("Start");
        Time.timeScale = 1f;
        CanvasSetActive();
        Invoke("FadeIn2", 6f);
        Invoke("StartGameScene", 7f);
    }
    public void StartGameScene()
    {
        SceneManager.LoadScene("Game");
        Cursor.visible = false;

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void CanvasSetActive()
    {
        panelOuf.SetActive(true);
    }
    public void CanvasSetDesactive()
    {
        panelOuf.SetActive(false);
    }

    public void CreditsOn()
    {
        credits.SetActive(true);
    }

    public void CreditsOff()
    {
        credits.SetActive(false);
    }

}

