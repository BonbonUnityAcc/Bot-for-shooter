using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public List<Transform> visiblePoints;

    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private PlayerUI canvasUI;

    private int currHealth;

    public Weapon weapon;

    public Camera newCam;
    public Transform point;

    private void Awake()
    {
        currHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        canvasUI.ChangeHealth(currHealth);
        if (currHealth <= 0)
        {
            Instantiate(newCam, point);
            Cursor.lockState = CursorLockMode.None;
            Destroy(gameObject);
        }
    }
    public void WeaponAnimatorPlay()
    {
        weapon.animator.Play(0);
    }
}
