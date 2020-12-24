using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] GameObject playerGO;
    [SerializeField] GameObject eaglePrefab;
#pragma warning restore 0649

    Player player;
    float speed = 0.5f;
    float lerpTime = 3.0f;
    float lerpTimeCurrent = 0;
    float lerpPercent;

    float offset;
    float deathOffset = 0.5f;

    float CurrentOffset { get { return playerGO.transform.position.z - transform.position.z; } }


    float MoveCameraZ()
    {
        float pos = transform.position.z;
        pos += speed * Time.deltaTime;

        return Mathf.Clamp(pos, playerGO.transform.position.z - offset, playerGO.transform.position.z);
    }
    float MoveCameraX()
    {
        return Mathf.Clamp(playerGO.transform.position.x, -3.0f, 3.0f);
    }
    void MoveCamera()
    {
        lerpTimeCurrent += Time.deltaTime * speed;
        if (lerpTimeCurrent >= lerpTime)
        {
            lerpTimeCurrent = lerpTime;
        }
        lerpPercent = lerpTimeCurrent / lerpTime;

        Vector3 endPos = transform.position;
        endPos.x = MoveCameraX();
        endPos.z = MoveCameraZ();

        transform.position = Vector3.Lerp(transform.position, endPos, lerpPercent);
    }

    IEnumerator EagleDeath()
    {
        player.IsDead = true;
        Instantiate(eaglePrefab, player.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1);
        player.OnDeath();
    }

    void LateUpdate()
    {
        if (CurrentOffset <= deathOffset && !player.IsDead)
        {
            StartCoroutine("EagleDeath");
        }
        else if (UIController.gameStarted && !player.IsDead)
        {
            MoveCamera();
        }
    }
    void Awake()
    {
        if (Extensions.GameObjectExt.IsNull(FindObjectOfType<Player>(), "No Object with Player Script."))
        {
            player = FindObjectOfType<Player>();
        }
        playerGO = playerGO == null ? player.gameObject : playerGO;
    }
    void Start()
    {
        offset = playerGO.transform.position.z - transform.position.z;
    }
}
