using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Fox_Move : MonoBehaviour {

    public float speed,jumpForce,cooldownHit;
	public bool up,down,jumping,crouching,dead,attacking; //,special;
	const bool running = true;
    private Rigidbody2D rb;
    private Animator anim;
	private SpriteRenderer sp;
	private float rateOfHit;
	public float hitPoints;
    public float timeToDie;
    private float maxHealth;
    public Slider healthbar;
	//private float life;
	//private int qtdLife;

	// Use this for initialization
	void Start() 
	{
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
		hitPoints = 100;
		maxHealth = hitPoints;
        //healthbar.value = CalculateHealth(); //connects the in game health to UI 
	}

	private void Update()
    {
        //This is a dealing damage test code
        if (Input.GetKeyDown(KeyCode.R))
            DealDamage(10);
	}

	 /// Update is called once per frame
	void FixedUpdate() 
	{
		if(dead==false)
		{
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
			Death();
		}
	}

    // Update is called once per frame
    void LateUpdate()
    {
        if (hitPoints <= 0)
        {
            StartCoroutine(Death());
        }
    }

    void DealDamage(float damageValue)
    {
        hitPoints -= damageValue;
        healthbar.value = CalculateHealth();
        if (hitPoints <= 0)
            Death();
			TryAgain();
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(timeToDie);
        hitPoints = 0;
        //Destroy(gameObject);
        Debug.Log("You Died");
		anim.SetTrigger("Dead");
		dead=true;
    }

    float CalculateHealth()
    {
        return hitPoints / maxHealth; //calculates health from max health. Returns in decimals representing percentage. 99/100 = .99
    }

    public float HitPoints
    {
        get { return hitPoints; }
        set
        {
            hitPoints += value;
            if (hitPoints > maxHealth)
            {
                hitPoints = maxHealth;
            }
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
		if(Input.GetKeyDown(KeyCode.X)){
			rb.velocity=new Vector2(0,0);
			anim.SetTrigger("Attack");
			attacking=true;
		}
	}

	void AttackEnd(){
		attacking=false;
	}

	void Special(){
		if(Input.GetKey(KeyCode.Space)){
			anim.SetBool("Special",true);
		}else{
			anim.SetBool("Special",false);
		}
	}

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
		}
		if(other.tag=="death_floor")
			Death();
	}								

	/*void OnCollisionEnter2D(Collision2D other) {						//Case of Touch
		if(other.gameObject.tag=="Enemy"){
			anim.SetTrigger("Damage");
			DealDamage(10);
		}
		else if(other.gameObject.tag == "death_floor"){
			Death();
			//TryAgain();
		}
	}*/

	/*void Hurt(){
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
	}*/

	public void TryAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}