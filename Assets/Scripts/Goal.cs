using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered goal: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("ถึงเส้นชัยแล้ว!");
            GameManager.Instance.Win();
        }
    }
}
