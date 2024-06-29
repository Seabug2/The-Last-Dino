using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnList : MonoBehaviour
{
    List<GameObject> myList;
    
    public void SetMyList(List<GameObject> _myList) 
    {
        myList = _myList;
    }
    private void OnDisable()
    {
        myList.Add(gameObject);
    }
 
}
