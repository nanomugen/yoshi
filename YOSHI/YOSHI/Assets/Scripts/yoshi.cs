using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoshi : MonoBehaviour
{
  private float movement;
    public float Speed;
    //public Transform feetPos;
    //public float checkRadius;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator anim;

    //jump
    public float jumpForce;
    //private float jumpTimeCounter;
    //public float jumpTime;
    private bool isJumping;
    private bool doubleJump;


    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void FixedUpdate(){
        //Move();
    }

    void Update(){
        Move();
        Jump();
        Atk();
        
    }


    void Move(){
        movement = Input.GetAxisRaw("Horizontal");
        //transform.position += movement * Time.deltaTime * Speed;
        rb.velocity = new Vector2(Speed*movement,rb.velocity.y);
        if(movement>0f){
            anim.SetBool("RUN",true);  
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }   
        else{
            if(movement<0f){
                anim.SetBool("RUN",true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
            }
            else{
                anim.SetBool("RUN",false);
            }
        }
    }
    //(DAI)depois ajeitar isso, todos os verificadores de apertar botao devem estar no update e chamar a função especifica
    //DAI CRIAR A LAYER PARA AS COISAS TIPO WALL E GROUND E DEPOIS FAZER TAGS PARA CADA UM ESPECIFICA
    void Jump(){
        if(!isJumping && Input.GetButtonDown("Jump")){
            rb.velocity = Vector2.up * jumpForce;
            isJumping=true;
            //jumpTimeCounter=jumpTime;
            doubleJump=true;
            anim.SetBool("JUMP",true);
        }
        else{
            if(Input.GetButtonDown("Jump") && doubleJump){
                    doubleJump=false;
                    rb.velocity = Vector2.up * jumpForce;
                    //jumpTimeCounter=jumpTime;
                    anim.SetBool("DOUBLE JUMP",true);
                    isJumping = true;
                }
            
            if(Input.GetButtonUp("Jump") && rb.velocity.y > 0){
                //isJumping=false;
                rb.velocity = Vector2.zero;
                //Debug.Log(rb.velocity.y.ToString());
            }
        }
    }

    void Atk(){
        if(Input.GetButtonDown("Fire1")){
            anim.SetTrigger("ATK");
        }
    }
    
    
    //BUGADO QUANDO PULA ENCOSTADO NA PAREDE, PORQUE ELE NÃO SAI, VAI PRECISAR DIFERENCIAR FLOOR E WALL
    void OnCollisionEnter2D(Collision2D other){
        
        
    }
    //DAI CIRCLE COLLIDER PRA CONSEGUIR OPULAR DA BEIRADA
    void OnTriggerEnter2D(Collider2D other){
        //if(Physics2D.OverlapCircle(feetPos.position,checkRadius,whatIsGround){
        if(other.gameObject.layer == 8){
            Ground();
        }
        
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.layer == 8){
            anim.SetBool("JUMP",true);
            doubleJump=true;
            isJumping=true;
        }
        
    }
    void Ground(){
        
        anim.SetBool("JUMP",false);
        anim.SetBool("DOUBLE JUMP",false);
        isJumping = false;
        doubleJump=false;
        
    }
    void EventExemple(){
        Debug.Log("event on animation example(atk)");
    }
}
