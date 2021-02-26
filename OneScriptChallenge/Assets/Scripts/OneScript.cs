using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class OneScript : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;

    public GameObject player;
    private Rigidbody2D _rigidbody; // end of player movement variables

    public GameObject platform;
    public GameObject platformVertical;

    private Vector2 startPosition;
    private Vector2 newPosition;

    private Vector2 startPositionVer;
    private Vector2 newPositionVer;

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
    public GameObject checkpoint3; // checkpoints

    public CanvasGroup panel;
    public CanvasGroup panel2;
    public GameObject panelOuf;
    public GameObject credits;

    public GameObject menu;
    private bool isMenuOpen = false;
    public Animator animator;
    //ui 

    public GameObject shutle1;
    public GameObject shutle2;

    // corretcing bugs/gameplay


    void Start()
    {
        _rigidbody = player.GetComponent<Rigidbody2D>();
        //``````````````player initiating````````````
        startPosition = platform.transform.position;
        newPosition = platform.transform.position;

        startPositionVer = platformVertical.transform.position;
        newPositionVer = platformVertical.transform.position;
        //```````````````platform intiating`````````````
        FadeOut();
        StartCoroutine(FadeCanvasGroup(panel2, panel2.alpha, 0f, 1f));
        Invoke("CanvasSetDesactive", 1f);
        menu.SetActive(false);
        credits.SetActive(false);
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
        platform.transform.position = newPosition;


        newPositionVer.y = startPositionVer.y + (maxDistance * Mathf.Sin(Time.time * speedPlatform));
        platformVertical.transform.position = newPositionVer;

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

        }

        if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen == false)
        {
            Time.timeScale = 0.0001f;
            menu.SetActive(true);
            isMenuOpen = true;
            sound.Stop();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isMenuOpen == true)
        {
            Time.timeScale = 1f;
            menu.SetActive(false);
            isMenuOpen = false;
        }
        // showing menu
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            this.transform.parent = col.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
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

        if (other.CompareTag("Lose"))
        {
            player.transform.position = checkpointPos;
            loseSound.Play();
            FadeIn();
            Invoke("FadeOut", 1f);
        }
    }
    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(panel, 1f, panel.alpha, 1f));
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
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
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

