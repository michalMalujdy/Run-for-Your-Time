using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	public Bar HealthBar;

	private float currentHealth = 100.0f;
	private float maxHealth = 100.0f;

    public float getCurrentHealth()
    {
        return currentHealth;
    }
    
    public float getMaxHealth()
    {
        return maxHealth;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddSubstractCurrentHealth(float value)
	{
		currentHealth += value;

		if (currentHealth < 0.0f) {
			currentHealth = 0.0f;
		}
		else if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		HealthBar.SetStatus (currentHealth);
	}
}
