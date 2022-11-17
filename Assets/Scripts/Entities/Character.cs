using System;
using Commands;
using Controllers;
using EventQueue;
using Flyweight;
using Managers;
using UnityEngine;

namespace Entities
{
    public class Character : MonoBehaviour
    {
        //Binding movements
        [SerializeField] private KeyCode moveForward = KeyCode.W;
        [SerializeField] private KeyCode moveBackward = KeyCode.S;
        [SerializeField] private KeyCode moveLeft = KeyCode.A;
        [SerializeField] private KeyCode moveRight = KeyCode.D;

        [SerializeField] private KeyCode jump = KeyCode.Space;
        [SerializeField] private KeyCode sprint = KeyCode.LeftShift;

        [SerializeField] private KeyCode pickup = KeyCode.G;
        [SerializeField] private KeyCode switchFlashlight = KeyCode.F;

        // Interactions
        [SerializeField] private LayerMask pickableLayer;
        [SerializeField] private float pickupDistance = 2;
        private bool _hasFlashlight;
        private bool _contactWithFlashlight;
        private bool _contactWithNote;

        // Movement properties
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.3f;
        [SerializeField] private LayerMask groundMask;

        [SerializeField] private AudioSource interactionsAudioSource;
        [SerializeField] private AudioClip pickupNoteAudioClip;
        [SerializeField] private AudioClip pickupFlashlightAudioClip;

        [SerializeField] private AudioSource walkingAudioSource;
        [SerializeField] private AudioSource sprintingAudioSource;

        // Body parts
        [SerializeField] private Transform head;

        //Commands
        private CmdMovement _cmdMoveForward;
        private CmdMovement _cmdMoveBack;
        private CmdMovement _cmdMoveLeft;
        private CmdMovement _cmdMoveRight;
        private CmdJump _cmdJump;
        private CmdPickUpFlashlight _cmdPickUpFlashlight;
        private CmdPickUpNote _cmdPickUpNote;
        private CmdRotation _cmdRotation;
        private CmdStartSprinting _cmdStartSprinting;
        private CmdStopSprinting _cmdStopSprinting;
        private CmdSwitchFlashlight _cmdSwitchFlashlight;
        private CmdWin _cmdWin;
        private Flashlight _flashlight;
        private Inventory _inventory;
        private bool _isGrounded;
        private LifeController _lifeController;

        //Instances
        private MovementController _movementController;

        [SerializeField] private CharacterStats _characterStats;
        public CharacterStats CharacterStats => _characterStats;


        private void Start()
        {
            _movementController = GetComponent<MovementController>();
            _lifeController = GetComponent<LifeController>();
            _inventory = GetComponent<Inventory>();
            _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
            _cmdMoveBack = new CmdMovement(_movementController, Vector3.back);
            _cmdMoveLeft = new CmdMovement(_movementController, Vector3.left);
            _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
            _cmdStartSprinting = new CmdStartSprinting(_lifeController, _movementController);
            _cmdStopSprinting = new CmdStopSprinting(_movementController);
            _cmdJump = new CmdJump(_movementController, Vector3.up);
            _cmdWin = new CmdWin();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            var headRay = new Ray(head.position, head.forward);
            UpdatePickableRaycast(headRay);
        }

        private void Update()
        {
            UpdateMovement();
            UpdateMovementAudio();
            UpdateFlashlightState();
            UpdateNoteState();
        }


        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Boat"))
            {
                if (_inventory.IsFull()) EventQueueManager.instance.AddCommand(_cmdWin);
                else UpdateUIPanel("You must collect all notes\n before returning to the boat");
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Boat"))
            {
                UpdateUIPanel(null);
            }
        }

        private void UpdatePickableRaycast(Ray ray)
        {
            RaycastHit hitData;
            Debug.DrawRay(ray.origin, ray.direction * pickupDistance, color: Color.red);
            if (Physics.Raycast(ray, out hitData, pickupDistance, pickableLayer))
            {
                UpdateUIPanel("Press G to pickup");
                Collider collider = hitData.collider;
                if (collider.CompareTag("Note"))
                {
                    var note = collider.GetComponent<Note.Note>();
                    _cmdPickUpNote = new CmdPickUpNote(note, _inventory, interactionsAudioSource, pickupNoteAudioClip);
                    _contactWithNote = true;
                }
                else if (collider.CompareTag("Flashlight"))
                {
                    _flashlight = gameObject.GetComponentInChildren<Flashlight>(true);
                    var floorFlashlight = collider.GetComponent<Flashlight>();
                    _cmdPickUpFlashlight = new CmdPickUpFlashlight(floorFlashlight, _flashlight,
                        interactionsAudioSource,
                        pickupFlashlightAudioClip);
                    _contactWithFlashlight = true;
                }
            }
            else
            {
                if (_contactWithNote)
                {
                    UpdateUIPanel(null);
                    _contactWithNote = false;
                }

                if (_contactWithFlashlight)
                {
                    UpdateUIPanel(null);
                    _contactWithFlashlight = false;
                }
            }
        }

        private void UpdateNoteState()
        {
            if (_contactWithNote && Input.GetKeyDown(pickup))
            {
                EventQueueManager.instance.AddCommand(_cmdPickUpNote);
                _contactWithNote = false;
            }
        }

        private void UpdateFlashlightState()
        {
            if (!_hasFlashlight && _contactWithFlashlight && Input.GetKeyDown(pickup))
            {
                EventQueueManager.instance.AddCommand(_cmdPickUpFlashlight);
                _hasFlashlight = true;
                _cmdSwitchFlashlight = new CmdSwitchFlashlight(_flashlight);
                _contactWithFlashlight = false;
            }

            if (_hasFlashlight && Input.GetKeyDown(switchFlashlight))
                EventQueueManager.instance.AddCommand(_cmdSwitchFlashlight);
        }

        private void UpdateMovementAudio()
        {
            if (Input.GetKey(moveForward) || Input.GetKey(moveBackward) || Input.GetKey(moveLeft) ||
                Input.GetKey(moveRight))
            {
                walkingAudioSource.enabled = true;
                if (_movementController.Sprinting)
                {
                    walkingAudioSource.enabled = false;
                    sprintingAudioSource.enabled = true;
                }
                else
                {
                    sprintingAudioSource.enabled = false;
                }
            }
            else
            {
                walkingAudioSource.enabled = false;
                sprintingAudioSource.enabled = false;
            }
        }

        private void UpdateMovement()
        {
            var mouseX = Input.GetAxis("Mouse X");
            _cmdRotation = new CmdRotation(_movementController, Vector3.up * mouseX);
            EventQueueManager.instance.AddMovementCommand(_cmdRotation);

            if (Input.GetKey(moveForward)) EventQueueManager.instance.AddMovementCommand(_cmdMoveForward);
            if (Input.GetKey(moveBackward)) EventQueueManager.instance.AddMovementCommand(_cmdMoveBack);
            if (Input.GetKey(moveLeft)) EventQueueManager.instance.AddMovementCommand(_cmdMoveLeft);
            if (Input.GetKey(moveRight)) EventQueueManager.instance.AddMovementCommand(_cmdMoveRight);
            if (Input.GetKey(sprint))
            {
                EventQueueManager.instance.AddMovementCommand(_cmdStartSprinting);
            }
            else
            {
                if (_lifeController.CurrentStamina < _lifeController.MaxStamina) _lifeController.increaseStamina(0.5f);
            }

            if (Input.GetKeyUp(sprint)) EventQueueManager.instance.AddCommand(_cmdStopSprinting);

            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (Input.GetKeyDown(jump) && _isGrounded) EventQueueManager.instance.AddMovementCommand(_cmdJump);
        }

        private void UpdateUIPanel(string message)
        {
            EventManager.instance.UIPanelUpdate(message);
        }
    }
}