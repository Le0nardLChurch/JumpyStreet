using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public const float animPlayerTime = 25.0f;
#pragma warning disable 0649 
    [SerializeField] InputData inputData;
    [SerializeField] Scoring scoring;
    [SerializeField] AnimationController animationController;
#pragma warning restore 0649

    Vector3 startPos;
    Vector3 endPos;
    Quaternion startRot;
    Quaternion endRot;
    float lerpTime = (animPlayerTime / 60.0f);  //Based on hop animation length. Make assignment call in start maybe?
    float lerpTimeCurrent = 0;

    Player player;

    public InputData PlayerInputData { get { return inputData; } }


    void SetStartLerp()
    {
        startPos = transform.position;
        startRot = transform.localRotation;
    }

    Quaternion RotatePlayer(float direction)
    {
        return Quaternion.Euler(Vector3.up * direction);
    }
    Vector3 MovePlayer(Vector3 direction)
    {
        animationController.PlayHop();

        Vector3 fix = Normalize(transform.position, transform.parent);

        return (fix + direction);
    }

    //To correct positon if moved off 1unit grid
    Vector3 Normalize(Vector3 position, Transform parent)
    {
        if (transform.position.x % 1 != 0)
        {
            position.x = Mathf.RoundToInt(transform.position.x);
        }
        if (transform.position.y % 1 != 0)
        {
            position.y = 1;
        }
        if (transform.position.z % 1 != 0)
        {
            position.z = Mathf.RoundToInt(transform.position.z);
        }

        return position;
    }

    bool GetInputData(KeyCode inputData)
    {
        return Input.GetKey(inputData);
    }

    private void FixedUpdate()
    {
        if (player.GameStarted)
            if (!AnimationController.IsHopping && !player.IsDead)
            {
                if (GetInputData(inputData.Forward) &&
                    Player.CanMove(transform.position, Vector3.forward))
                {
                    SetStartLerp();
                    endPos = MovePlayer(Vector3.forward);
                    endRot = RotatePlayer(0);
                }
                else
                if (GetInputData(inputData.Back) &&
                    Player.CanMove(transform.position, Vector3.back))
                {
                    SetStartLerp();
                    endPos = MovePlayer(Vector3.back);
                    endRot = RotatePlayer(180);
                }
                else
                if (GetInputData(inputData.Left) &&
                    Player.CanMove(transform.position, Vector3.left))
                {
                    SetStartLerp();
                    endPos = MovePlayer(Vector3.left);
                    endRot = RotatePlayer(-90);
                }
                else
                if (GetInputData(inputData.Right) &&
                    Player.CanMove(transform.position, Vector3.right))
                {
                    SetStartLerp();
                    endPos = MovePlayer(Vector3.right);
                    endRot = RotatePlayer(90);
                }
                lerpTimeCurrent = 0;
            }
            else
            {
                lerpTimeCurrent += Time.deltaTime;
                if (lerpTimeCurrent >= lerpTime)
                {
                    lerpTimeCurrent = lerpTime;
                }
                float lerpPercent = lerpTimeCurrent / lerpTime;

                transform.position = Vector3.Lerp(startPos, endPos, lerpPercent);
                if (!startRot.Equals(endRot))
                {
                    transform.localRotation = Quaternion.Lerp(startRot, endRot, lerpPercent);
                }
            }
    }

    private void Awake()
    {
        animationController = animationController == null ? gameObject.GetComponentInChildren<AnimationController>() : animationController;
        scoring = scoring == null ? FindObjectOfType<Scoring>() : scoring;
        player = FindObjectOfType<Player>();
    }
    private void Start()
    {

    }
}





