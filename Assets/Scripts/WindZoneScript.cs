using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FireElemental;
using MyBox;

public class Wind : MonoBehaviour
{
    [SerializeField] private float damageDelay;
    [ReadOnly] [SerializeField] private bool canDamage;
    private const string PlayerTagString = "Player";

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag(PlayerTagString)) return;
        if (canDamage)
        {
            other.GetComponent<FireElementalController>().Death();
            canDamage = false;
            return;
        }
        StartCoroutine(nameof(CooldownRoutine));
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(damageDelay);
        canDamage = true;
    }

    // This part is for trigger to recognize rigidbody every frame no matter what
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTagString))
        {
            other.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTagString))
        {
            other.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.StartAwake;
        }
    }
}
