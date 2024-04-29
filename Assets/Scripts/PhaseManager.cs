using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhaseManager : MonoBehaviour
{
    [Header("Phase Objects")]
    public bool phase;
    public GameObject phaseA;
    public GameObject phaseB;

    [Header("Timing Variables")]
    private float timer = 0;
    public float cooldown; // The amount by which the timer increases
    public TMP_Text timerUI;
    public float timeLeft;

    [Header("Phase Type")]
    public bool dashType;
    public bool timeType;

    [Header("Dash Type")]
    public GameObject Player;
    Movement playerMovement;
    PlayerInput playerInput;

    [Header("Time Type")]
    private float shifTimer = 0; // If it is in timer mode then this is when it will change phase
    public float shiftCD; // The amount by which shiftimer changes

    // Start is called before the first frame update
    void Start()
    {
        // Player
        playerMovement = Player.GetComponent<Movement>();
        playerInput = Player.GetComponent<PlayerInput>();

        // UI
        timerUI.GetComponent<TextMeshProUGUI>().text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        // Phase Change basics
        if (phase)
        {
            phaseA.SetActive(true);
            phaseB.SetActive(false);
        }
        else
        {
            phaseA.SetActive(false);
            phaseB.SetActive(true);
        }

        // If dashType
        if (timer > Time.time)
        {
            timeLeft = timer - Time.time;
            timerUI.text = timeLeft.ToString();
            if (timeType)
            {
                TimedChanger();
            }
            if (dashType)
            {
                DashChanger();
            }
        }

        // Button for toggle
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            phase = !phase;
        }
    }

    public void PhaseChanger() // Called by button
    {
        timer = Time.time + cooldown;
        // phase = !phase;
        if (!timeType && !dashType)
        {
            phase = !phase;
        }
    }

    void DashChanger() // Change when dashing
    {
        if (playerMovement.isDashing && shifTimer < Time.time)
        {
            phase = !phase;
            shifTimer = Time.time + 1;
        }
    }

    void TimedChanger() // Phase changes after some seconds
    {
        if(shifTimer < Time.time)
        {
            phase = !phase;
            shifTimer = Time.time + shiftCD;
        }
    }
}