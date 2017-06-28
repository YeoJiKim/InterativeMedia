using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


//1030514308余智钦
public class GameManager : MonoBehaviour {

    public Texture Replay;
    public Texture Next;
    public Texture Return;
    public Texture BloodBg;
    public Texture BloodPg1;
    public Texture BloodPg2;
    public Texture myCursor;//鼠标未点击时的图片
    public Texture myClickCursor;//鼠标点击时的图片

    // Use this for initialization
    private RaycastHit hitInfo;
    private Ray ray;
    public AudioClip tapSfx;							//tap sound for buttons click
    private float buttonAnimationSpeed = 9;
    private bool canTap = true;

    private float cursorWidth = 25;
    private float cursorHeight = 25;//绘制鼠标图片的大小
    private bool isClicked;

    public AudioSource m_AudioBg;

    void Start () {
        Cursor.visible = false;
        isClicked = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (canTap)
        {
            StartCoroutine(tapManager());
        }
        if (Input.GetMouseButton(0))
            isClicked = true;

        else
            isClicked = false;


    }

    IEnumerator tapManager()
    {
        if (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Ended)
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        else if (Input.GetMouseButtonUp(0))
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        else
            yield break;
        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject objectHit = hitInfo.transform.gameObject;

            switch (objectHit.name)
            {

                case "Start":
                    playSfx(tapSfx);                            //声音
                    PlayerPrefs.SetInt("GameMode", 0);          
                    StartCoroutine(animateButton(objectHit));   //点击动作模拟
                    yield return new WaitForSeconds(0.2f);      //等待时间
                   // Debug.Log("start");
                    SceneManager.LoadScene("Choose");         //加载下一场景
                   // Application.LoadLevel("Chaos");
                    break;

                case "levelone":
                    playSfx(tapSfx);
                    PlayerPrefs.SetInt("GameMode", 0);
                    StartCoroutine(animateButton(objectHit));
                    yield return new WaitForSeconds(0.2f);
                    SceneManager.LoadScene("Chaos");

                    break;

                case "leveltwo":
                    playSfx(tapSfx);
                    PlayerPrefs.SetInt("GameMode", 0);
                    StartCoroutine(animateButton(objectHit));
                    yield return new WaitForSeconds(0.2f);
                    SceneManager.LoadScene("Fractal");

                    break;


                case "levelthree":
                    playSfx(tapSfx);
                    PlayerPrefs.SetInt("GameMode", 0);
                    StartCoroutine(animateButton(objectHit));
                    yield return new WaitForSeconds(0.2f);
                    SceneManager.LoadScene("G&A");

                    break;

                case "choose2start":
                    RoleMove1.dead = false;//防止卡在零分过关的bug
                    RoleMove1.life = 100;
                    Chaser.m_BChase = false;
                    playSfx(tapSfx);
                    PlayerPrefs.SetInt("GameMode", 0);
                    StartCoroutine(animateButton(objectHit));
                    yield return new WaitForSeconds(0.2f);
                    SceneManager.LoadScene("Start");

                    break;
                case "tochoose":
                    RoleMove1.dead = false;//防止卡在零分过关的bug
                    RoleMove1.life = 100;
                    RoleMove1.eatenFood = 0;
                    RoleMove1.eatall = false;
                    Chaser.m_BChase = false;
                    playSfx(tapSfx);
                    PlayerPrefs.SetInt("GameMode", 0);
                    StartCoroutine(animateButton(objectHit));
                    yield return new WaitForSeconds(0.2f);
                    SceneManager.LoadScene("Choose");

                    break;
            }

          }

        }

    void playSfx(AudioClip _clip)
    {
        GetComponent<AudioSource>().clip = _clip;
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator animateButton(GameObject _btn)
    {
        canTap = false;
        Vector3 startingScale = _btn.transform.localScale;  //initial scale	
        Vector3 destinationScale = startingScale * 1.1f;        //target scale

        //Scale up
        float t = 0.0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime * buttonAnimationSpeed;
            _btn.transform.localScale = new Vector3(Mathf.SmoothStep(startingScale.x, destinationScale.x, t),
                                                    Mathf.SmoothStep(startingScale.y, destinationScale.y, t),
                                                    _btn.transform.localScale.z);
            yield return 0;
        }

        //Scale down
        float r = 0.0f;
        if (_btn.transform.localScale.x >= destinationScale.x)
        {
            while (r <= 1.0f)
            {
                r += Time.deltaTime * buttonAnimationSpeed;
                _btn.transform.localScale = new Vector3(Mathf.SmoothStep(destinationScale.x, startingScale.x, r),
                                                        Mathf.SmoothStep(destinationScale.y, startingScale.y, r),
                                                        _btn.transform.localScale.z);
                yield return 0;
            }
        }

        if (r >= 1)
            canTap = true;
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 26;

        Vector2 mousePos = Input.mousePosition;//记录鼠标位置

        if (isClicked)
            GUI.DrawTexture(new Rect(mousePos.x - cursorWidth / 2,
                Screen.height - mousePos.y - cursorHeight / 2,
                    cursorWidth, cursorHeight), myClickCursor);//鼠标点下后绘制的图片
        else
            GUI.DrawTexture(new Rect(mousePos.x - cursorWidth / 2, 
                Screen.height - mousePos.y - cursorHeight / 2,
                    cursorWidth, cursorHeight), myCursor);//鼠标未点下绘制的图片

        GUIStyle style1 = new GUIStyle();
        style1.normal.textColor = new Color(0.0f / 256f, 0.0f / 256f, 0.0f / 256f);

        GUIStyle style2 = new GUIStyle();
        style2.normal.textColor = new Color(100f / 256f, 100f / 256f, 100f / 256f);


        if (SceneManager.GetActiveScene().name == "Chaos")
        {
            Rect rctBloodBar;
            rctBloodBar = new Rect(50, 9, 80, 9);

          
            
            //GUI.DrawTexture(rctBloodBar, bloodText);
            GUI.Label(rctBloodBar, "生命值：", style1);

            rctBloodBar = new Rect(110, 12, 200, 10);
            GUI.DrawTexture(rctBloodBar, BloodBg);
            rctBloodBar = new Rect(110, 12, 2 * RoleMove1.life, 10);//之所以乘2是因为生命值最大是一百，但矩形长度最大为200；
            //如果大于20则画绿色血条，小于20则画红色血条
            if (RoleMove1.life > 20)
            {
                GUI.DrawTexture(rctBloodBar, BloodPg1);
            }
            else
            {
                GUI.DrawTexture(rctBloodBar, BloodPg2);
            }

           // GUI.Label(new Rect(20, 70, 150, 90), RoleMove1.life.ToString(), style);
            if (RoleMove1.eatall)
            {
                
                GUI.skin.label.fontSize = 26;              
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                style.normal.textColor = new Color(100f / 256f, 100f / 256f, 100f / 256f);
                GUI.Label(new Rect(0, Screen.height * 0.2f, Screen.width, 60), "恭喜进入下一关");

                GUI.skin.label.fontSize = 10;             
                GUI.backgroundColor = new Color(0, 0, 0, 0);
                if (GUI.Button(new Rect(Screen.width * 0.5f - 150, Screen.height * 0.5f, 300, 50), Next))
                {
                    //前往下一关卡
                    SceneManager.LoadScene("Fractal");
                    RoleMove1.dead = false;//防止卡在零分过关的bug
                    RoleMove1.life = 100;
                    Chaser.m_BChase = false;
                }

            }
            else if (RoleMove1.dead)
            {
              
                if (m_AudioBg.isPlaying)
                {

                      m_AudioBg.Stop();
                }
            
                GUI.skin.label.fontSize = 26;
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                style.normal.textColor = new Color(100f / 256f, 100f / 256f, 100f / 256f);
                GUI.Label(new Rect(0, Screen.height * 0.2f, Screen.width, 60), "挑战失败");

                GUI.skin.label.fontSize = 10;
                GUI.backgroundColor = new Color(0, 0, 0, 0);
                if (GUI.Button(new Rect(Screen.width * 0.5f - 150, Screen.height * 0.5f, 300, 50), Replay))
                {
                   
                    SceneManager.LoadScene("Chaos");
                    RoleMove1.dead = false; 
                    RoleMove1.life = 100;
                    Chaser.m_BChase = false;
                }

            }

        }

        if (SceneManager.GetActiveScene().name == "Fractal")
        {
            Rect rctBloodBar;
            rctBloodBar = new Rect(50, 9, 80, 9);
            GUI.Label(rctBloodBar, "生命值：", style2);

            rctBloodBar = new Rect(110, 12, 200, 10);
            GUI.DrawTexture(rctBloodBar, BloodBg);
            rctBloodBar = new Rect(110, 12, 2 * RoleMove1.life, 10);//之所以乘2是因为生命值最大是一百，但矩形长度最大为200；
            //如果大于20则画绿色血条，小于20则画红色血条
            if (RoleMove1.life > 20)
            {
                GUI.DrawTexture(rctBloodBar, BloodPg1);
            }
            else
            {
                GUI.DrawTexture(rctBloodBar, BloodPg2);
            }
            
            
            //GUI.Label(new Rect(20, 70, 150, 90), RoleMove1.life.ToString(), style);
            if (RoleMove1.eatall)
            {               
                GUI.skin.label.fontSize = 26;
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                style.normal.textColor = new Color(100f / 256f, 100f / 256f, 100f / 256f);
                GUI.Label(new Rect(0, Screen.height * 0.2f, Screen.width, 60), "恭喜进入下一关");

                GUI.skin.label.fontSize = 10; 
                GUI.backgroundColor = new Color(0, 0, 0, 0);
                if (GUI.Button(new Rect(Screen.width * 0.5f - 150, Screen.height * 0.5f, 300, 50), Next))
                {
                    //前往下一关卡
                    SceneManager.LoadScene("G&A");
                    RoleMove1.dead = false;
                    RoleMove1.life = 100;
                    Chaser.m_BChase = false;
                }

            }
            else if (RoleMove1.dead)
            {
                if (m_AudioBg.isPlaying)
                {

                    m_AudioBg.Stop();
                }
               
                GUI.skin.label.fontSize = 26;            
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                style.normal.textColor = new Color(100f / 256f, 100f / 256f, 100f / 256f);
                GUI.Label(new Rect(0, Screen.height * 0.2f, Screen.width, 60), "挑战失败");

                GUI.skin.label.fontSize = 10;
                GUI.backgroundColor = new Color(0, 0, 0, 0);
                if (GUI.Button(new Rect(Screen.width * 0.5f - 150, Screen.height * 0.5f, 300, 50), Replay))
                {
                    //读取当前关卡
                    SceneManager.LoadScene("Fractal");
                    RoleMove1.dead = false;
                    RoleMove1.life = 100;
                    Chaser.m_BChase = false;
                }

            }

        }

        if (SceneManager.GetActiveScene().name == "G&A")
        {
            Rect rctBloodBar;
            rctBloodBar = new Rect(50, 13, 80, 20);

            GUI.Label(rctBloodBar,"生命值：",style2);

            rctBloodBar = new Rect(110, 15, 200, 10);
            GUI.DrawTexture(rctBloodBar, BloodBg);
            rctBloodBar = new Rect(110, 15, 2 * RoleMove1.life, 10);//之所以乘2是因为生命值最大是一百，但矩形长度最大为200；
            
            //如果大于20则画绿色血条，小于20则画红色血条
            if (RoleMove1.life > 20)
            {
                GUI.DrawTexture(rctBloodBar, BloodPg1);
            }
            else
            {
                GUI.DrawTexture(rctBloodBar, BloodPg2);
            }
            
            // GUI.Label(new Rect(20, 70, 150, 90), RoleMove1.life.ToString(), style);
            if (RoleMove1.eatall)
            {
               
                GUI.skin.label.fontSize = 26;
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                style.normal.textColor = new Color(100f / 256f, 100f / 256f, 100f / 256f);
                GUI.Label(new Rect(0, Screen.height * 0.2f, Screen.width, 60), "恭喜游戏通关");

                GUI.skin.label.fontSize = 10;
                GUI.backgroundColor = new Color(0, 0, 0, 0);
                if (GUI.Button(new Rect(Screen.width * 0.5f - 150, Screen.height * 0.5f, 300, 50), Return))
                {
                    //返回游戏界面
                    SceneManager.LoadScene("Start");
                    RoleMove1.dead = false;
                    RoleMove1.life = 100;
                    Chaser.m_BChase = false;
                }

            }
            else if (RoleMove1.dead)
            {
                if (m_AudioBg.isPlaying)
                {

                    m_AudioBg.Stop();
                }
                             
                GUI.skin.label.fontSize = 26;
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                style.normal.textColor = new Color(100f / 256f, 100f / 256f, 100f / 256f);
                style.alignment = TextAnchor.LowerCenter;
                GUI.Label(new Rect(0, Screen.height * 0.2f, Screen.width, 60), "挑战失败",style);

                GUI.skin.label.fontSize = 10;
                GUI.backgroundColor = new Color(0, 0, 0, 0);            
                if (GUI.Button(new Rect(Screen.width * 0.5f - 150, Screen.height * 0.5f, 300, 50), Replay))
                {
                    //读取当前关卡
                    SceneManager.LoadScene("G&A");
                    RoleMove1.dead = false;
                    RoleMove1.life = 100;
                    Chaser.m_BChase = false;
                }

            }

        }

    }
}
