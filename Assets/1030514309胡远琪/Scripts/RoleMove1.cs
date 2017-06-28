using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//1030514309胡远琪
public class RoleMove1 : MonoBehaviour {
    float newA = 0;
    
    //显示的字符
    private string show;
    //用时
    private float usetime = 0;
    //生命值
    public static int life = 100;
    Color ParticleColor;
    //小球的速度
    private float maxvel;
    //小球所受到的力
    private Vector2 force;
    //保存此关卡中食物总数与当前吃掉的总数
    public static int allFood, eatenFood;
    //是否全吃掉了
    public static bool eatall;
    //是否碰到终止图标
    public static bool dead = false;

    public GameObject m_Splash;

   
    private Rigidbody2D m_Rigidbody;

    public UnityEvent m_EventKnock;
    void Start () {

        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        ParticleColor = psmain.startColor.color;

        show = "";
        //得到主角对象
        m_Rigidbody = GetComponent<Rigidbody2D>();
        //给小球一个初始的力
        maxvel = 5.0f;
        force = new Vector2(1, 1);
        //设置食物初始值
        if(SceneManager.GetActiveScene().name == "Chaos")
        {
            allFood = 12;
        }
        else if (SceneManager.GetActiveScene().name == "Fractal")
        {
            allFood = 10;
        }
        else
        {
            allFood = 10;
        }

        eatenFood = 0;
        eatall = false;
        
    }

    // Update is called once per frame
    void Update() {

        float dt = Time.deltaTime;
        if (life <= 0)
        {
            life = 0;
            dead = true;
        }

        //判断是否将所有的食物都吃到了
        eatall = eatAll();


        if (!dead && !eatall)
        {
            if (transform.position.x < -3 && transform.position.x > -8)
            {
                if (transform.position.y > 0 && transform.position.y <3)

                    changeAlpha(100);

            }


            //监听键盘事件
            keyListener();
            //限制最大速度
            if (m_Rigidbody.velocity.magnitude > maxvel)
            {
                m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * maxvel;
            }

            //透明度随时间减小
            usetime += dt;
            if (usetime >= 1)
            {
                changeAlpha(-15);
                usetime = 0;
            }
            
        }else 
        {
            //停止游戏
            m_Rigidbody.velocity = new Vector2(0, 0);
        }

    }
     private void FixedUpdate()
    {
        keyListener();
    }
  
    //键盘监听函数
    void keyListener()
    {
        if (dead) { }
        else
        {
            //由SWAD或上下左右键控制小球的移动
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                force = new Vector2(0, 8);
                //为小球施加一个力
                m_Rigidbody.AddForce(force);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                force = new Vector2(0, -8);
                //为小球施加一个力
                m_Rigidbody.AddForce(force);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                force = new Vector2(8, 0);
                //为小球施加一个力
                m_Rigidbody.AddForce(force);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                force = new Vector2(-8, 0);
                //为小球施加一个力
                m_Rigidbody.AddForce(force);
            }
        }

    }


    //判断是否吃掉了所有的食物
    bool eatAll()
    {

        if (allFood == eatenFood)
            return true;

        return false;
    }

    
    //当吃到食物时改变粒子颜色
    public void changeColor(Color newColor)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        //先将粒子的颜色等于食物的颜色，但透明度不变
        psmain.startColor= new Color(newColor.r, newColor.g, newColor.b, psmain.startColor.color.a);
        ParticleColor = newColor;
        //改变透明度
        changeAlpha(50);
    }

    //随着时间改变透明度与生命值,increaseA应该为一个介于0-255之间的数，为透明度的增量
    public void changeAlpha(float increaseA)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        //改变透明度
        
        newA = psmain.startColor.color.a + (float)(increaseA / 255.0f);

        psmain.startColor = new Color(psmain.startColor.color.r, psmain.startColor.color.g, 
            psmain.startColor.color.b, newA);
        //生命值是一个介于0-100之间的整数
        life = (int)(100.0f * psmain.startColor.color.a);
   
    }

    //对墙进行碰撞检测
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.name == "W")
        {
            changeAlpha(-50);
            m_EventKnock.Invoke();
           
            //将slash的颜色改为粒子的颜色
            m_Splash.GetComponent<SpriteRenderer>().color = ParticleColor;

            //根据墙的位置，确定飞溅的旋转角度
            if (coll.collider.tag == "bottomW") {

                Instantiate(m_Splash, transform.position, Quaternion.AngleAxis(90, Vector3.forward));
            }
            else if (coll.collider.tag == "topW")
            {
                Instantiate(m_Splash, transform.position, Quaternion.AngleAxis(-90, Vector3.forward));
            }
            else if (coll.collider.tag == "leftW")
            {
                Instantiate(m_Splash, transform.position, Quaternion.AngleAxis(180, Vector3.forward));
            }
            else
            {
                Instantiate(m_Splash, transform.position, Quaternion.AngleAxis(0, Vector3.forward));
            }
        }
    }


}
