using UnityEngine;

public class GiveHealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Поднял аптечку");
            Player player = other.gameObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("player не присвоил компоненту");
                return;
            }
            player.TakeDamage(-25);
            Destroy(gameObject);
        }
    }
}
