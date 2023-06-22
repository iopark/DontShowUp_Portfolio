using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ShopType_Basic_", menuName = "SO/Shop")]
public class Shop : ScriptableObject
{
    [SerializeField] private string shoptypename;
    [SerializeField] private ItemInfo[] itemList;

    [Serializable]
    public class ItemInfo
    {
        public Item item;
        public int price; 
    }
}
