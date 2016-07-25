using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MovingObject {

	public float restartLevelDelay = 1f;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public int wallDamage = 1;
	public Text foodText;

	private Animator animator;
	private int food;

	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator> ();

		food = GameManger.instance.playerFoodPoints;
		foodText.text = "Food: " + food;
  		base.Start ();  //mm
	}

	private void OnDisable(){
	
		GameManger.instance.playerFoodPoints = food;
	}

	// Update is called once per frame
	void Update () {
		if (!GameManger.instance.playersTurn)
			return;

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)(Input.GetAxisRaw ("Horizontal"));
		vertical = (int)(Input.GetAxisRaw ("Vertical"));

		if (horizontal != 0) {
			vertical = 0;
		}

		if(horizontal != 0 || vertical != 0)
		{
			AttemptMove<Wall>(horizontal, vertical);
		}
	}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		food--;

		foodText.text = "Food: " + food;
		base.AttemptMove<T> (xDir, yDir);

		RaycastHit2D hit;

//		if (Move (xDir, yDir, out hit)) {
//			//ca;; sfx
//		
//		}
		CheckIfGameOver ();

		GameManger.instance.playersTurn = false;
	}

	protected override void OnCantMove<T>(T component)
	{
		Wall hitWall = component as Wall;
		hitWall.DamageWall (wallDamage);
		animator.SetTrigger ("playerChop");
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Exit") {
			Invoke ("Restart", restartLevelDelay);
			enabled = false;
		} else if (other.tag == "Food") {
			food += pointsPerFood;
			other.gameObject.SetActive (false);
		}else if (other.tag == "Soda") {
			food += pointsPerSoda;
			other.gameObject.SetActive (false);
		}
	}

	private void Restart()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void LoseFood(int loss)
	{
		animator.SetTrigger ("playerHit");
		food -= loss;
		CheckIfGameOver ();
	}

	private void CheckIfGameOver()
	{
		if (food <= 0) {
			GameManger.instance.GameOver ();
		}
	}
}
