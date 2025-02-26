using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
   public GameManager gameManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScore(1);
        }
        else if (collision.gameObject.CompareTag("Trap"))
        {
            gameManager.GameOver();
        }

       // else if (collision.gameObject.CompareTag("Enemy"))
       // {
       //     gameManager.GameOver();
      //  }

        else if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            gameManager.GameWin();        
        }
    }
}
