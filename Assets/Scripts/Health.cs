using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    void OnAwake() {
        healthBar = GameObject.Find("HealthBar").GetComponent<RectTransform>();
    }

    public void TakeDamage(int amount) {
        if (isServer) {
            currentHealth -= amount;
            if(currentHealth <= 0) {
                currentHealth = 0;
                Debug.Log("Dead");
            }
        }
    }

    void OnGUI() {
        healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x * CalcHealth(), healthBar.sizeDelta.y);

        GUI.Label(new Rect(0,0,200,100), currentHealth.ToString());
        TextMesh healthText = new TextMesh();

    }

    private float CalcHealth() {
        return currentHealth / maxHealth;
    }
}
