using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string name = "Пистолет";
    public int damage = 10;
    public float range = 100f;
    public float delay = 10f;
    public Animator animator;
    public ParticleSystem particleSystem;
}
