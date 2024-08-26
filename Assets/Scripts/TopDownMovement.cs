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
    private float gravityValue = -9.81f;
    private float controllerDeadZone = 0.1f;
    private GlobalGameState gameState;
    private CharacterController controller;
    private PlayerControls playerControls;
    private PlayerInput playerInput;
    private TextMeshPro scoreText;
    private Vector2 movement;
    private Vector3 playerVelocity;
    public GameOverScreen gameOverScreen;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        gameState = GameObject.FindWithTag("GlobalGameState").GetComponent<GlobalGameState>();
        scoreText = GetComponentInChildren<TextMeshPro>();
        //gameOverScreen = new GameOverScreen();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("ScoreVolume"))
        {
            Destroy(other.gameObject);
            gameState.score++;
            scoreText.text = "Score: " + (gameState.score - 1).ToString();
        }

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
        float y = gameObject.transform.position.y;
        if(y < -10)
        {
            gameOverScreen.Setup(gameState.score);
        }
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
