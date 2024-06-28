using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfEnqueue : MonoBehaviour
{
    Queue<GameObject> myQueue;
    
    public void SetMyQueue(Queue<GameObject> _myQueue) {
        myQueue = _myQueue;
    }

    private void OnBecameInvisible()
    {
        myQueue.Enqueue(gameObject);
    }
}
