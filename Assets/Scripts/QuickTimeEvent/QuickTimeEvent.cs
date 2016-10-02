using UnityEngine;
using System.Collections;
using System;

public class QuickTimeEvent : MonoBehaviour
{
    private int type;
    private int Type { get { return type; } set { type = value; } }

    public static int lurkingEnemy = 0;
    public static int fallingNet = 1;
    public static int ballWithSpikes = 2;

    public Attack attackButton;
    private bool quickTimeEventMode = false;
    public bool QuickTimeEventMode { get { return quickTimeEventMode; } set { quickTimeEventMode = value; } }

    public Zoom zoomCamera;

    private LurkingEnemy lurkingEnemyComponent;
    public LurkingEnemy LurkingEnemyComponent { get { return lurkingEnemyComponent; } set { lurkingEnemyComponent = value; } }

    private TrapExecutor trapExecutorComponent;
    public TrapExecutor TrapExecutorComponent { get { return trapExecutorComponent; } set { trapExecutorComponent = value; } }

    private bool slowMotion;
    public bool SlowMotion { get { return slowMotion; } } 

    private float slowMotionSpeed = 0.1f;

    public DisableEnableMovement player;
    private string buttonInQTETag;


    public void Start()
    {
        attackButton.QuickTimeEventHandler = this;
    }

    public void StartQTE(int type, LurkingEnemy lurkingEnemy, bool slowMotion, string buttonTag = null)
    {
        lurkingEnemyComponent = lurkingEnemy;
        CommonStartQTE(type, slowMotion, buttonTag);
    }

    public void StartQTE (int type, TrapExecutor trapExecutor, bool slowMotion, string buttonTag = null)
    {
        trapExecutorComponent = trapExecutor;
        CommonStartQTE(type, slowMotion, buttonTag);
    }

    private void CommonStartQTE(int type, bool slowMotion, string buttonTag = null)
    {
        this.type = type;
        this.slowMotion = slowMotion;
        this.buttonInQTETag = buttonTag;

        quickTimeEventMode = true;
        zoomCamera.ZoomIn();
        player.disableMovementButAttack();

        if(slowMotion)
        {
            Time.timeScale = slowMotionSpeed;
        }

        if(buttonInQTETag == "AttackButton")
        {
            attackButton.glowImage.enabled = true;
        }
    }

    public void EndQTE()
    {
        quickTimeEventMode = false;
        zoomCamera.ZoomOut();
        player.enableMovement();

        if(slowMotion)
        {
            Time.timeScale = 1.0f;
        }

        if (buttonInQTETag == "AttackButton")
        {
            attackButton.glowImage.enabled = false;
        }
    }

    public void AttackButtonClicked()
    {
        if(type == lurkingEnemy)
        {
            lurkingEnemyComponent.EnemyGotHit();
        }

        else if(type == fallingNet)
        {

        }
    }
}
