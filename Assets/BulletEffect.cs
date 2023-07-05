using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    private void Start()
    {
        GameManager.Resource.Destroy(gameObject, 1f);
    }
}
