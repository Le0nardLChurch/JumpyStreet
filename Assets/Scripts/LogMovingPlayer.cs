using System.Collections;
using UnityEngine;

public class LogMovingPlayer : MonoBehaviour
{
    WaitForSeconds animationTime = new WaitForSeconds(PlayerMovement.animPlayerTime + 1.0f / 60.0f);


    IEnumerator AttachPlayer(Collider collision)
    {
        collision.transform.SetParent(transform.parent);
        yield return animationTime;
        collision.transform.localPosition = transform.localPosition;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            StartCoroutine(AttachPlayer(collision));
            //Debug.Log(collision.transform.parent);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Player>() && collision.transform.IsChildOf(transform.parent))
        {
            collision.transform.SetParent(null);
        }
    }
}
