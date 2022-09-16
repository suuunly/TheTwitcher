using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Health))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float MovementSpeed = 10.0f;
    public float TurnSpeed = 10.0f;

    [Header("Actions")]
    public Gun Gun;
    public Punching Punching;

    [Header("Boost")]
    public GameEventPure Boosted;
    public GameEventPure Unboosted;

    public float SpeedModifier;


    [Header("Visuals")]
    public Animator Animation;

    private Rigidbody mBody;

    private Vector2 mRotation;

    private float _speedModifier = 1.0f;
    private System.Action _calculateRotation;


    private void Awake()
    {
        this._calculateRotation = this.calculateLookRotation;
        InputSettings.OnTypeSet += OnControlsSet;
    }

    // Start is called before the first frame update
    void Start()
    {
        mBody = GetComponent<Rigidbody>();
        Boosted.OnRaised += this.BoostPlayer;
        Unboosted.OnRaised += this.UnboostPlayer;

        Health health = GetComponent<Health>();
        health.OnDamageTaken += this.OnTakeDamage;
        health.OnDied += this.OnDied;

        MenuHandler.OnPauseState += OnPaused;
    }

    void OnPaused(bool state)
    {
        enabled = !state;
    }

    void OnControlsSet(InputType type)
    {
        if (type == InputType.Controller)
            _calculateRotation = this.calculateLookRotation;
        else
            _calculateRotation = this.calculateLookRotationMouse;
    }

    void OnDied() {
        enabled = false;
    }

    void OnTakeDamage()
    {
        this.Animation.SetTrigger("damaged");
    }

    void UnboostPlayer(object data)
    {
        this._speedModifier = 1.0f;
    }


    void BoostPlayer(object data)
    {
        this._speedModifier = SpeedModifier;
    }

    private void OnDestroy()
    {
        Boosted.OnRaised -= this.BoostPlayer;
        Unboosted.OnRaised -= this.UnboostPlayer;
        MenuHandler.OnPauseState -= OnPaused;
        InputSettings.OnTypeSet -= OnControlsSet;
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        this._calculateRotation();

        processPunching();
        processShooting();
    }

    void processPunching()
    {
        if (Input.GetAxisRaw("Punch") > 0.0f && this.Punching.Punch())
            this.Animation.SetTrigger("punch");
    }

    void processShooting()
    {
        if (Input.GetAxisRaw("Fire") > 0.0f && this.Gun.Shoot())
            this.Animation.SetTrigger("shoot");
    }

    void calculateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = transform.TransformDirection(new Vector3(-x, 0.0f, z));
        Animation.SetFloat("forward", dir.z);
        Animation.SetFloat("sideways", dir.x);

        float dSpeed = MovementSpeed * Time.deltaTime * this._speedModifier;
        float dGRavity = Physics.gravity.y * Time.deltaTime;

        mBody.AddForce(new Vector3(x, 0.0f, z) * dSpeed, ForceMode.Impulse);
        Vector3 vel = mBody.velocity;

        mBody.velocity = new Vector3(x * dSpeed, vel.y, z * dSpeed);
    }

    void calculateLookRotation()
    {
        float x = -Input.GetAxis("HorizontalRotate");
        if (Mathf.Abs(x) >= Mathf.Epsilon) mRotation.x = x;

        float y = -Input.GetAxis("VerticalRotate");
        if (Mathf.Abs(y) >= Mathf.Epsilon) mRotation.y = y;

        Quaternion target = Quaternion.Euler(new Vector3(0, Mathf.Atan2(mRotation.y, mRotation.x) * 180 / Mathf.PI, 0));
        transform.rotation = Quaternion.Slerp(transform.rotation, target, TurnSpeed * Time.deltaTime);
    }

    void calculateLookRotationMouse()
    {
        float screenH = Screen.height / 2;
        float  screenW = Screen.width / 2;
        float hight = transform.position.y;

        float mouseX = Input.mousePosition.x - screenW;
        float mouseY = Input.mousePosition.y - screenH;
        Vector3 targetPos = new Vector3(mouseX, hight, mouseY);
        transform.LookAt(targetPos);
    }
}
