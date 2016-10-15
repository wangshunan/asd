using UnityEngine;
using System.Collections;

public class MSPlayerController : MonoBehaviour {

	const string TOP_WALL = "TopWall";
	const string BOTTOM_WALL = "BottomWall";
	const string RIGHT_WALL = "RightWall";
	const string LEFT_WALL = "LeftWall";


	bool isHolded = false;	//タッチし続けている状態かどうか
	bool standby = true;

	Vector3 force;	//移動量
	[SerializeField] float friction = 0.985f;	//摩擦係数
	[SerializeField] float minForce = 0.2f;		//この数値を下回ったら停止する
	[SerializeField] int power = 4;
	[SerializeField] SpriteRenderer arrow;		//矢印画像
	[SerializeField] float arrowScale = 100;	//矢印の表示倍率


	// Use this for initialization
	void Start () {
		//最初は非表示
		arrow.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//このスクリプトがアタッチされているGameObjectの座標を動かす
		transform.position += force * Time.deltaTime;

		force *= friction;
		standby = false;
		if (force.magnitude < minForce) {
			force = default(Vector3);
			standby = true;
		}

		if (Input.GetMouseButtonDown (0) && standby) {
			var hit = Physics2D.Raycast(
				Camera.main.ScreenToWorldPoint(Input.mousePosition),
				Vector2.zero
			);

			if ( hit != null )Debug.Log("touch");

			if (hit.collider != null) {
				arrow.gameObject.SetActive (true);
				isHolded = true;
			}
		}
		if (Input.GetMouseButton (0) && standby) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos.z = 0;
			float scale = (transform.position - pos).magnitude / Screen.width * arrowScale;
			arrow.transform.localScale = new Vector3 (scale, scale, 1);

			float angle = Vector3.Angle (
				transform.position - pos,
				Vector3.up
				);

			Vector3 cross = Vector3.Cross (
				                transform.position - pos,
				                Vector3.up
			                );
			if (cross.z > 0)
				angle *= -1;
			arrow.transform.rotation = Quaternion.Euler ( new Vector3(0, 0, angle + 90));

		}
		if (Input.GetMouseButtonUp (0) && standby && isHolded ) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos.z = transform.position.z;
			force = transform.position - pos;
			force *= power;
			isHolded = false;
			arrow.gameObject.SetActive (false);
		}

	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log ("Wall hit");
		//壁に当たったら移動量を反転させる
		if ( coll.CompareTag(TOP_WALL) || coll.CompareTag(BOTTOM_WALL)) {
			force.y *= -1;
		} else if ( coll.CompareTag(RIGHT_WALL) || coll.CompareTag(LEFT_WALL)) {
			force.x *= -1;
		} else if( coll.CompareTag("Enemy")) {
			//敵に当たったら、プレイヤーを跳ね返らせる
			Vector3 vecToEnemy = coll.gameObject.transform.position - this.transform.position;

			//to make sure
			force.z = 0;
			vecToEnemy.z = 0;

			//get angle
			float deg = Vector2.Angle(force, vecToEnemy);
			//neagte if angle is negative
			Vector3 cross = Vector3.Cross(force, vecToEnemy);
			if ( cross.z > 0 ) deg *= -1;

			//rotate vector
			force = Quaternion.AngleAxis(deg * 2, Vector3.back) * force * -1;

			coll.GetComponent<MSEnemyController>().Damage(10);
		}

	}
}
