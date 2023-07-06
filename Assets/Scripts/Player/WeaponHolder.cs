//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WeaponHolder : MonoBehaviour
//{
//    [SerializeField] Gun gun;
//    [SerializeField] WeaponSO currentWeapon; 

//    List<Gun> gunList = new List<Gun>();

//    private void Start()
//    {
//        currentWeapon = GameManager.Resource.Instantiate<WeaponSO>("Data/Weapon/Ranged_Shotgun"); 
//    }
//    public void Swap(int index)
//    {
//        gun = gunList[index];
//    }
//    public void Fire()
//    {
//        gun.Fire();
//    }

//    public void GetWeapon(Gun gun)
//    {
//        gunList.Add(gun);
//    }
//}
