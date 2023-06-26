using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item_Basic_", menuName = "SO/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject prefab; 
}
