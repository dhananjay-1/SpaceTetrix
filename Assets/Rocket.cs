using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // SerializeField makes the data member editable through inspector and non-editable through other scripts
    // The value specified here for SerializeField is just the default value which will be shown on inspector
    [SerializeField] float mainThrust = 100f;  // thrust for flying up
    [SerializeField] float rcsThrust = 100f;   // thrust for rotation, rcs : rotation control system
    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip levelCompleteSound;

    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State { Alive, Dead, Transcending };
    State state = State.Alive; 

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) // ignore collisions after dying or transcending state is reached
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //Nothing happens
                break;
            case "Finish":
                state = State.Transcending;
                StartSuccessSequence();
                break;
            default:
                state = State.Dead;
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(levelCompleteSound);
        Invoke("LoadNextLevel", 1f);
    }

    private void StartDeathSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        Invoke("LoadFirstLevel", 1f);
    }

    private void LoadFirstLevel()
    {
        //state = State.Alive; when a level is loaded the state becomes alive by default
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        //state = State.Alive; when a level is loaded the state becomes alive by default
        SceneManager.LoadScene(1);
    }

    private void ProcessInput()
    {
        if (state != State.Alive)
        {
            return;
        }

        RespondToThrustInput();
        RespondToRotationInput();
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float flyingSpeed = mainThrust;
                
            rigidbody.AddRelativeForce(Vector3.up * flyingSpeed);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrustSound);
            }

        }
        else
        {
            audioSource.Stop();
        }
    }

    private void RespondToRotationInput()
    {
        rigidbody.freezeRotation = true; // Disabling the physics of rotation while taking manual control just to provide more stability

        float rotationSpeed = rcsThrust * Time.deltaTime;  // Time.deltaTime gives the time for which previous frame remained and is a good estimator of current frame time span
                                                           // If frame time span is less, the rotationSpeed is less and is more for longer frame time span, this ensures frame rate independence

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidbody.freezeRotation = false;  // Enabling the physics of rotation again 
    }
}
