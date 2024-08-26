using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]


public class TopDownMovement : MonoBehaviour
{
    

    private float playerSpeed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float controllerDeadZone= 0.1f;
    
    GlobalGameState gameState;

    private CharacterController controller;

    private Vector2 movement;
    private Vector3 playerVelocity;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private TextMeshPro scoreText;
   
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        gameState = GameObject.FindWithTag("GlobalGameState").GetComponent<GlobalGameState>();
        scoreText = GetComponentInChildren<TextMeshPro>();
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag.Equals("ScoreVolume"))
        {
            Destroy(other.gameObject);
            gameState.score++;
            scoreText.text = "Score: " + (gameState.score-1).ToString();
        }
        //scoreText.text = "Score: " + gameState.score.ToString();

    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = gameState.game_data.player_data.speed;
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
    }
    void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
    }
    void HandleMovement()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * playerSpeed);
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
