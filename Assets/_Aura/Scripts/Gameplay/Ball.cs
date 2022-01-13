using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float debugDestroyTime = 5f;
    //for testing, destroy ball after launch
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(debugDestroyTime);
        Destroy(gameObject);
    }
}
