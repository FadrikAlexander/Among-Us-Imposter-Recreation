using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Classes and Components
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;
    PlayerAudioController playerAudioController;
    Rigidbody2D rigidbody2D;

    //Other Classes
    VentsSystem ventsSystem;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerAudioController = GetComponent<PlayerAudioController>();
    }

    #region Movement
    [SerializeField] float Speed = 5f;
    float currentSpeed = 0;

    bool CanMove = false;
    bool isMoving = false;

    void FixedUpdate()
    {
        if (CanMove)
        {
            isMoving = false;
            float inputHor = Input.GetAxis("Horizontal");
            float inputVer = Input.GetAxis("Vertical");

            ChangeSpeed(inputHor, inputVer);

            if (inputHor != 0)
            {
                isMoving = true;
                gameObject.GetComponent<Transform>().Translate(Vector3.right * inputHor * currentSpeed * Time.deltaTime);
            }
            if (inputVer != 0)
            {
                isMoving = true;
                gameObject.GetComponent<Transform>().Translate(Vector3.up * inputVer * currentSpeed * Time.deltaTime);
            }

            if (isMoving)
            {
                playerAnimator.SetBool("Walk", true);
                playerAudioController.PlayWalkingSound();
            }
            else
            {
                playerAnimator.SetBool("Walk", false);
                playerAudioController.StopWalking();
            }

            FlipSprite(inputHor);
        }
    }

    void FlipSprite(float inputDirection)
    {
        if (inputDirection > 0)
            playerSpriteRenderer.flipX = false;
        else if (inputDirection < 0)
            playerSpriteRenderer.flipX = true;
    }

    //Ramp Speed
    void ChangeSpeed(float inputHorDirection, float inputVerDirection)
    {
        //if the movemont started
        if (inputHorDirection != 0 || inputVerDirection != 0)
        {
            //Start Ramping Speed until reach the limit
            currentSpeed += 0.75f;
            if (currentSpeed >= Speed)
                currentSpeed = Speed;
        }
        else
        {
            currentSpeed -= 0.5f;

            //Stop moving when the speed go to 0
            if (currentSpeed <= 0)
            {
                currentSpeed = 0;
            }
        }
    }

    #endregion

    #region Vent Movement Control
    public void EnterVent(VentsSystem ventsSystem)
    {
        this.ventsSystem = ventsSystem;

        //Animation and sounds
        playerAnimator.SetTrigger("Vent");
        playerAudioController.StopWalking();
        playerAudioController.PlayVent();
    }

    public void VentEntered()
    {
        DisablePlayer();

        ventsSystem.PlayerInVent();
    }

    public bool IsInVent()
    {
        return rigidbody2D.simulated;
    }

    public void VentExited()
    {
        EnablePlayer();

        //sounds
        playerAudioController.PlayVent();
    }
    #endregion

    #region Change Player Properties
    public void MovePlayer()
    {
        CanMove = true;
    }
    public void StopPlayer()
    {
        CanMove = false;
    }
    public void KillPlayer()
    {
        CanMove = false;
        playerAnimator.SetTrigger("Dead");
    }
    void DisablePlayer()
    {
        Color c = playerSpriteRenderer.color;
        c.a = 0;
        playerSpriteRenderer.color = c;
        rigidbody2D.simulated = false;
        playerAudioController.StopWalking();
    }
    void EnablePlayer()
    {
        Color c = playerSpriteRenderer.color;
        c.a = 1;
        playerSpriteRenderer.color = c;
        rigidbody2D.simulated = true;
        MovePlayer();
    }
    #endregion
}
