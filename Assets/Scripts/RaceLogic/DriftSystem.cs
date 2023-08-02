using UnityEngine;
using TMPro;

/// <summary>
/// Manages the drift system also displays text & score
/// </summary>
public class DriftSystem : MonoBehaviour
{
    #region Fields
    
    // Singleton for access to drifting system
    public static DriftSystem instance;
    // Used for the UI
    public bool driftActive;
    // Our audiosource
    public AudioSource[] driftUISounds;
    // Our drift UI
    public GameObject driftStatus;
    public TextMeshProUGUI driftScoreText;
    public TextMeshProUGUI driftStatusText;
    // Adjust this value based on how fast you want the score to increase
    public int scoringRate = 1;
    // Adjust this value to set the minimum angle for a valid drift
    public float driftAngleThreshold = 30f;
    // Adjust this value to set the minimum angular velocity for a valid drift
    public float angularVelocityThreshold = 0.1f;
    // Our current drift score
    private int driftScore = 0;
    // Our player Rigidbody
    private Rigidbody playerCar;
    private bool activateDriftSound;

    #endregion

    #region Methods
    
    private void Start()
    {
        // Find our car object
        playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        // Singleton
        instance = this;
        // For some reason, the drift audio doesn't work when I drift, but only when I don't, so I swapped logic, and for some reason now it works
        // when I'm drifting, by saying it should play when I'm not drifting, it's strange and this probably needs to be checked.
        activateDriftSound = false;
        Invoke(nameof(EnableDriftAudio), 3f);
    }

    private void Update()
    {
        // Update our score rate
        UpdateScoreRate();
        // Update our status text
        UpdateDriftStatus();
        // Update our drift score
        UpdateScore();
    }
    
    /// <summary>
    /// Updates the users drift score
    /// </summary>
    private void UpdateScore()
    {
        float carAngle = Vector3.Angle(playerCar.velocity, playerCar.transform.forward);
        float carAngularVelocity = playerCar.angularVelocity.magnitude;

        // Check to see if our car is sideways and sideways force is applied
        if (carAngle > driftAngleThreshold && carAngularVelocity > angularVelocityThreshold)
        {
            driftActive = true;
            driftScore += scoringRate;
            driftScoreText.text = driftScore.ToString();
        }
        else
        {
            if (activateDriftSound)
            {
                driftUISounds[0].Play();
            }
            driftActive = false;
            driftScore = 0;
            driftScoreText.text = "";
        }
    }
    
    /// <summary>
    /// Increments the users scoringRate based on how fast we're going
    /// </summary>
    private void UpdateScoreRate()
    {
        if (PlayerController.instance.Speed < 20)
        {
            scoringRate = 0;
        }
        if (PlayerController.instance.Speed > 20 && PlayerController.instance.Speed < 80)
        {
            scoringRate = 1;
        }
        if (PlayerController.instance.Speed > 80 && PlayerController.instance.Speed < 120)
        {
            scoringRate = 2;
        }
        if (PlayerController.instance.Speed > 120 && PlayerController.instance.Speed < 180)
        {
            scoringRate = 3;
        }
        if (PlayerController.instance.Speed > 180)
        {
            scoringRate = 4;
        }
    }
    
    /// <summary>
    /// Changes the user's drift status text based on how well they're doing
    /// </summary>
    private void UpdateDriftStatus()
    {
        // SetActive is used to trigger our animation everytime we drift
        if (driftActive && driftScore < 300)
        {
            driftUISounds[1].Play();
            driftStatusText.text = "good drift";
            driftStatus.SetActive(true);

        }
        if (driftActive && driftScore > 300 && driftScore < 500)
        {
            driftStatusText.text = "great drift";
        }
        if (driftActive && driftScore > 500 && driftScore < 750)
        {
            driftStatusText.text = "superb";
        }
        if (driftActive && driftScore > 750)
        {
            driftStatusText.text = "insane drift";
        }
        else if (!driftActive)
        {
            driftStatusText.text = "";
            driftStatus.SetActive(false);
        }
    }
    
    /// <summary>
    /// Enables drift audio
    /// </summary>
    private void EnableDriftAudio()
    {
        activateDriftSound = true;
    }
    
    #endregion
}
