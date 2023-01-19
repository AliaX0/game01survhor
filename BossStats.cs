using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : CharacterStats // Deriving from the CharacterStats component
{
    public GameObject VictoryScreen;
    public override void Die(){
        base.Die(); // Standard death function 
        StartCoroutine(waiter());
        Destroy(gameObject);
    }

    IEnumerator waiter(){
        VictoryScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        VictoryScreen.SetActive(false);
        PlayerManager.instance.MovePlayer(); // Call upon the MovePlayer method in PlayerManager, to move the player to the next level
        yield break;
    }
}
