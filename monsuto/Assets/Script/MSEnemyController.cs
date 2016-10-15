using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MSEnemyController : MonoBehaviour {
	[SerializeField] int life = 20;
	[SerializeField] int maxLife = 20;
	[SerializeField] Slider hpGauge;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			Destroy (gameObject);
		}
		hpGauge.value = (float)life / maxLife;
	}

	public void Damage(int value) {
		life -= value;
	}
}
