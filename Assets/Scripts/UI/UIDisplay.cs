using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
public class UIDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text killedEnemies;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] TMP_Text towersHealthText;
    [SerializeField] EnemyCount enemyCounter;
    [SerializeField] WeaponAmmo[] weaponAmmo;
    public List<TowersHealth> towersHealth = new List<TowersHealth>();

    
    
    void Update()
    {
        killedEnemies.text = "Killed: " + enemyCounter.enemyCount.ToString() + "/" + enemyCounter.maxEnemy.ToString();
        if(weaponAmmo[0].isActiveAndEnabled) ammoText.text = "Ammo " + weaponAmmo[0].currentAmmo.ToString();
        else ammoText.text = "Ammo " + weaponAmmo[1].currentAmmo.ToString();
        towersHealthText.text = "";
        if(towersHealth.Count > 0) towersHealthText.text = "Tower 1: " + towersHealth[0].health.ToString() + "\nTower 2: " + towersHealth[1].health.ToString() + "\nTower 3: " + towersHealth[2].health.ToString();
        
    }
}
