using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    //Player Combat: Materials 
    public UnityAction WeaponSwitch;
    public UnityAction<int> WeaponFire; 

    public UnityAction<bool> MeleeStrikeAtest;

    public GameObject playerHolder; 
    public GameObject player; 
    public Launcher currentWeapon; 
    public WeaponList weaponList;
    public Transform weaponHolder;
    public HashSet<Launcher> weaponsLists = new HashSet<Launcher>();
    public Queue<Launcher> weapons = new Queue<Launcher>();

    //Player Combat: Conditionals
    PlayerAttacker attacker;
    float flankThreshhold = -1 * 0.3f; 

    public UnityAction<string> CombatAlert;
    Vector3 enemyLookDir;
    Vector3 playerLookDir;

    Launcher tempContainer;
    private void Awake()
    {
        weaponList = Resources.Load<WeaponList>("Data/Weapon/Player_WeaponList");
        for (int i = 0; i < weaponList.RangedLists.Length; i++)
        {
            tempContainer = GameManager.Resource.Instantiate(weaponList.RangedLists[i].weapon.launcher, transform.position,
                weaponList.RangedLists[i].weapon.launcher.transform.localRotation, transform, true);
            tempContainer.transform.localScale = Vector3.one;
            weaponsLists.Add(tempContainer);
            weapons.Enqueue(tempContainer); 
        }
        WeaponSwitch += NewWeapon;
        GameManager.Instance.GameSetup += InitializeCombatMaterials;
        GameManager.Instance.ExitToMain += Reset; 
        //GameManager.Instance.GameSetup += InitializeCombatMaterials;
    }
    private void InitializeCombatMaterials()
    {
        if (player == null)
        {
            GameObject playerInstance = GameManager.Resource.Instantiate<GameObject>("Prefab/PlayerSetUp");
            playerHolder = playerInstance; 
            playerHolder.transform.SetParent(transform, false);
            player = playerInstance.transform.GetChild(1).gameObject; 
            player.transform.SetParent(transform, false);
        }
        SetPlayerLoc(); 
        attacker = player.GetComponent<PlayerAttacker>();
        attacker.InitializePlayer(); 
        if (currentWeapon == null)
        {
            SetWeapon();
            return;
        }
    }

    /// <summary>
    /// must precede from the InitializeFunctionCall 
    /// </summary>
    public void SetPlayerLoc()
    {
        Transform playerSpawnPos = GameObject.FindGameObjectWithTag("PlayerSpawner").transform;
        Vector3 spawnVector = GameObject.FindGameObjectWithTag("PlayerSpawner").transform.position;
        Vector3 spawnPos = transform.position + playerSpawnPos.localToWorldMatrix.MultiplyPoint3x4(Vector3.one);  // MultiplyPoint3x4(new Vector4(spawnVector.x, spawnVector.y, spawnVector.z, 1));
        player.transform.localPosition = spawnVector; 
        //player.transform.rotation = Quaternion.identity;
        player.transform.localScale = Vector3.one; 
    }

    public void Reset()
    {
        Destroy(player.gameObject);
        Destroy(playerHolder.gameObject); 
        player = null;
        playerHolder = null; 
    }

    public void SetWeapon()
    {
        if (weapons.Count <= 0)
            weapons.Enqueue(GameManager.Resource.Instantiate(weaponList.RangedLists[0].weapon.launcher, true));
        tempContainer = weapons.Dequeue();
        tempContainer.transform.position = weaponHolder.transform.position;
        tempContainer.transform.localScale = Vector3.one; 
        tempContainer.transform.rotation = weaponHolder.transform.rotation;
        tempContainer.transform.SetParent(weaponHolder.transform);
        currentWeapon = tempContainer; 
            //GameManager.Resource.Instantiate(tempContainer, weaponHolder.position, weaponHolder.rotation, weaponHolder, true); 
        weapons.Enqueue(tempContainer);
        attacker.SetWeapon(); 
    }

    private void NewWeapon()
    {
        currentWeapon.transform.SetParent(transform, false);
        //GameManager.Resource.Destroy(currentWeapon.gameObject); currentWeapon = null;
        tempContainer = weapons.Dequeue();
        tempContainer.transform.position = weaponHolder.transform.position;
        tempContainer.transform.rotation = weaponHolder.transform.rotation;
        tempContainer.transform.SetParent(weaponHolder.transform);
        tempContainer.transform.localScale = Vector3.one;
        currentWeapon = tempContainer;
            //currentWeapon = GameManager.Resource.Instantiate(tempContainer, weaponHolder.position, weaponHolder.rotation, weaponHolder, true);
        attacker.SetWeapon();
        weapons.Enqueue(tempContainer);
    }
    public void FlankJudgement(Transform target, System.Action<Transform, bool> flankAttempt)
    {
        playerLookDir = attacker.transform.forward;
        enemyLookDir = target.transform.forward;
        if (Vector3.Dot(enemyLookDir, playerLookDir) > flankThreshhold)
        {
            CombatAlert?.Invoke($"Player has flanked {target.gameObject.name}");
            Debug.Log($"Player has flanked {target.gameObject.name}"); 
            flankAttempt(target, true);
        }
        CombatAlert?.Invoke($"Player has failed to flanked {target.gameObject.name}");
        flankAttempt(target, false); 
    }
}
