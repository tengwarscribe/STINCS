using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Fox_Move : MonoBehaviour {

    public float speed,jumpForce,cooldownHit;
	public bool up,down,jumping,crouching,dead,attacking; //,special;
	const bool running = true;
    private Rigidbody2D rb;
    private Animator anim;
	private SpriteRenderer sp;
	private float rateOfHit;
	private GameObject[] life;
	private int qtdLife;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		sp=GetComponent<SpriteRenderer>();
		//running=true;
		up=false;
		down=false;
		jumping=false;
		crouching=false;
		dead = false;
		rateOfHit=Time.time;
		life=GameObject.FindGameObjectsWithTag("Life");
		qtdLife = 4;
	}
	
	// Update is called once per frame
	// This update was not fixed there was no else statement for "if(dead==false)"
	void FixedUpdate () {
		if(dead==false){
		//Character doesnt choose direction in Jump									//If you want to choose direction in jump
			if(attacking==false){													//just delete the (jumping==false)
				if(crouching==false){
					Movement();
					Attack();
					//Special();   
				}
				Jump();
				Crouch();
			}
		}
		else 
		{
			Dead();
			TryAgain();
		}

	}

	void Movement(){
		//Character Move
		float move = Input.GetAxisRaw("Horizontal");
		if(Input.GetKey(KeyCode.Z)){
			//Run
			rb.velocity = new Vector2(move*speed*Time.deltaTime*3,rb.velocity.y);
			//running=true;
		}else{
			//Walk
			rb.velocity = new Vector2(move*speed*Time.deltaTime,rb.velocity.y);
			//running=false;
		}

		//Turn
		if(rb.velocity.x<0){
			sp.flipX=true;
		}else if(rb.velocity.x>0){
			sp.flipX=false;
		}
		//Movement Animation
		if(rb.velocity.x!=0&&running==false){
			anim.SetBool("Walking",true);
		}else{
			anim.SetBool("Walking",false);
		}
		if(rb.velocity.x!=0&&running==true){
			anim.SetBool("Running",true);
		}else{
			anim.SetBool("Running",false);
		}
	}

	void Jump(){
		//Jump
		if(Input.GetKeyDown(KeyCode.Space)&&rb.velocity.y==0){
			rb.AddForce(new Vector2(0,jumpForce));
		}
		//Jump Animation
		if(rb.velocity.y>0&&up==false){
			up=true;
			jumping=true;
			anim.SetTrigger("Up");
		}else if(rb.velocity.y<0&&down==false){
			down=true;
			jumping=true;
			anim.SetTrigger("Down");
		}else if(rb.velocity.y==0&&(up==true||down==true)){
			up=false;
			down=false;
			jumping=false;
			anim.SetTrigger("Ground");
		}
	}

	void Attack(){																//I activated the attack animation and when the 
		//Atacking																//animation finish the event calls the AttackEnd()
		if(Input.GetKeyDown(KeyCode.C)){
			rb.velocity=new Vector2(0,0);
			anim.SetTrigger("Attack");
			attacking=true;
		}
	}

	void AttackEnd(){
		attacking=false;
	}

	/*void Special(){
		if(Input.GetKey(KeyCode.Space)){
			anim.SetBool("Special",true);
		}else{
			anim.SetBool("Special",false);
		}
	}*/

	void Crouch(){
		//Crouch
		if(Input.GetKey(KeyCode.DownArrow)){
			anim.SetBool("Crouching",true);
		}else{
			anim.SetBool("Crouching",false);
		}
	}

	void OnTriggerEnter2D(Collider2D other){							//Case of Bullet
		if(other.tag=="Enemy"){
			anim.SetTrigger("Damage");
			Hurt();
		}
	}								

	void OnCollisionEnter2D(Collision2D other) {						//Case of Touch
		if(other.gameObject.tag=="Enemy"){
			anim.SetTrigger("Damage");
			Hurt();
		}
		if(other.gameObject.tag == "death_floor"){
			Dead();
			TryAgain();
		}
	}

	void Hurt(){
		if(rateOfHit<Time.time){
			rateOfHit=Time.time+cooldownHit;
			Destroy(life[qtdLife-1]);
			qtdLife-=1;
		}
		if(qtdLife == 0)
		{
			Dead();
			TryAgain();
		}
	}

	void Dead(){
		if(qtdLife<=0){
			anim.SetTrigger("Dead");
			dead=true;
		}
	}

	public void TryAgain(){														//Just to Call the level again
		SceneManager.LoadScene("day pt.1");
	}
}
