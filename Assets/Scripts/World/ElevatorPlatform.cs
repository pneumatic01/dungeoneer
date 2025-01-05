using System.Collections;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public float Speed = 0.25f;
    public float DelayBetweenStops = 2f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isMoving = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = transform.position + (transform.up * 17f);
        StartCoroutine(ElevatorMove());
    }

    IEnumerator ElevatorMove() {
        while (true) {
            if (isMoving) {
                float elapsedTime = 0f;
                while (elapsedTime < 1f) {
                    elapsedTime += Time.deltaTime * Speed;
                    transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime);
                    yield return null; 
                }
                transform.position = targetPos;
                Vector3 temp = startPos;
                startPos = targetPos;
                targetPos = temp;
                isMoving = false;
                yield return new WaitForSeconds(DelayBetweenStops);
                isMoving = true;
            }
        }
    }
    
}
