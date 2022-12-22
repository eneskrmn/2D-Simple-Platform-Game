using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealt : Singleton<PlayerHealt>
{   
    private PlayerData Player1data;//referancı oluştursuk
     //HealtBar Game object
    [SerializeField] private float HealtBarWidth=400f;//can barının boyutu
    public GameObject HealtBarImageObj;//bar için nesne 
    public GameObject HealtBarTextObj;//can texti için nensen
    public GameObject GoldTextObj;

    private RectTransform HealtBarTransform;//componenti elde ettik
    private Text HealtBarText;//text
    private Text GoldCountText;

    Scene scene;
    void Awake()
    {
      
        Player1data = new PlayerData();//palyer datadan player1data nesne oluşturduk
        
        }


    public void setGUIobject(GameObject HealtBarImageGO, GameObject HealtBarTextGO , GameObject GoldTextGO)
    {
        HealtBarImageObj=HealtBarImageGO;
        HealtBarTextObj= HealtBarTextGO;
        GoldTextObj=GoldTextGO;
        HealtBarTransform = HealtBarImageObj.GetComponent<RectTransform>();//
        HealtBarText = HealtBarTextObj.GetComponent<Text>();
        GoldCountText = GoldTextObj.GetComponent<Text>();
        GoldCountText.text = Player1data.getGoldCount().ToString();//
        HealtBarText.text = Player1data.getCurrentHealth().ToString();//şuanki canı texte gönderdim
        HealtBarTransform.sizeDelta = new Vector2((Player1data.getCurrentHealth()/Player1data.getMaxHealth()*HealtBarWidth) ,HealtBarTransform.sizeDelta.y);
    }
    // Start is called before the first frame update
    void Start()
    {//ana menü de değilsek bu nesneleri ata
        scene = SceneManager.GetActiveScene();
        if(scene.buildIndex >0)
        {
          HealtBarTransform = HealtBarImageObj.GetComponent<RectTransform>();//
          HealtBarText = HealtBarTextObj.GetComponent<Text>();
          GoldCountText = GoldTextObj.GetComponent<Text>();
          GoldCountText.text = Player1data.getGoldCount().ToString();//
          HealtBarText.text = Player1data.getCurrentHealth().ToString();//şuanki canı texte gönderdim
          HealtBarTransform.sizeDelta = new Vector2((Player1data.getCurrentHealth()/Player1data.getMaxHealth()*HealtBarWidth) ,HealtBarTransform.sizeDelta.y);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
      
        UpdateHealtBar();

    }
     
    void UpdateHealtBar()
    {  scene = SceneManager.GetActiveScene();
      if(Input.GetKeyDown(KeyCode.KeypadPlus))//keykode işlmleri
      {//artı tuşuna basınca can 10 artıccak
        Player1data.setHealth(Player1data.getCurrentHealth()+10f);//
        
      } 
      if(Input.GetKeyDown(KeyCode.KeypadMinus))
      {//- tuşuna basınca can 10 azalcak
          Player1data.setHealth(Player1data.getCurrentHealth()-10f);
         
      }
       GoldCountText.text = Player1data.getGoldCount().ToString();
       HealtBarText.text = Player1data.getCurrentHealth().ToString();
       HealtBarTransform.sizeDelta = new Vector3((Player1data.getCurrentHealth()/Player1data.getMaxHealth()*HealtBarWidth) ,HealtBarTransform.sizeDelta.y,0f);
      
    }
    public void changeHealth(float Amount)//değişen can 
    {
          Player1data.setHealth(Player1data.getCurrentHealth()+Amount);
          HealtBarText.text = Player1data.getCurrentHealth().ToString();
          HealtBarTransform.sizeDelta = new Vector2((Player1data.getCurrentHealth()/Player1data.getMaxHealth()*HealtBarWidth) ,HealtBarTransform.sizeDelta.y);
    }
    public float getPlayerHealth()
    {
      return Player1data.getCurrentHealth();
    }

    void OnTriggerEnter2D(Collider2D col){//altın alanına girme

       if(col.gameObject.CompareTag("Gold"))//tagi altın olanı
       {
         Debug.Log("altın");
         Player1data.increaseGoldCount();//metodu aktif et
         Destroy(col.gameObject);//nesneyi yok et
       }

    }

    public PlayerData getPlayerData()//Kaydetme işlemleri
    {
      Player1data.setPlayerPos(this.transform.position.x,this.transform.position.y);
      scene = SceneManager.GetActiveScene();
      Player1data.setCurrentSceneId(scene.buildIndex);
      return Player1data;
      }//
    public void setPlayerData(PlayerData Pxdata)//yükleme işlemleri
    {
      scene = SceneManager.GetActiveScene();
      Player1data.setPlayerData(Pxdata);
      this.transform.position = new Vector3(Player1data.playerPosX, Player1data.playerPosY, 0f);
    }
    public Vector3 getPlayerPos(){return new Vector3(Player1data.playerPosX ,Player1data.playerPosY,0f);}
    //sahneler arası geçişte kullanılacak
}
