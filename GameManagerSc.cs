using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManagerSc : Singleton<GameManagerSc>
{
    [SerializeField] private GameObject PlayerPrefab;
    private GameObject PlayerGo;//player game objesini oluşturduk
    private PlayerHealt PlayerHealtSc;
    [SerializeField] private GameObject InGameCanvasPrefab;
    private GameObject InGameCanvas;
    private InGameCanvasSc InGameCanvasScript;
    public static GameManagerSc instance;
    Scene currentScene;//sahne bilgisi tutmak için
    private bool  startAnewGame;
    PlayerData Pxdata;

    void Start()
    { startAnewGame =false; 
      //get current scene
        currentScene = SceneManager.GetActiveScene();//aktif sahne

        //instantiate player
       PlayerGo = Instantiate(PlayerPrefab,new Vector3(0f,0f,0f),Quaternion.identity);//playergoyu tanımladık
       PlayerHealtSc = PlayerGo.GetComponent<PlayerHealt>();//playerhealtsc atadık

       //instantiate IngameCanvas
       InGameCanvas = Instantiate(InGameCanvasPrefab,new Vector3(0f,0f,0f),Quaternion.identity);//tanımladık
       InGameCanvasScript = InGameCanvas.GetComponent<InGameCanvasSc>();  //InagmevancasScripti atadık

       if(currentScene.buildIndex == 0)
       {
         PlayerGo.SetActive(false);//player goyu deactive  ettik
         InGameCanvas.SetActive(false);

       }
      }
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {

    }
    void OnSceneLoaded(Scene scene ,LoadSceneMode mode)
    {
      Debug.Log("sahne adı:"+scene.name);
      Debug.Log("sahne mod:"+mode);
      if(startAnewGame ==false)
      {//load game
        PlayerHealtSc.setPlayerData(Pxdata);
        PlayerGo.transform.position = PlayerHealtSc.getPlayerPos(); 

      }
      else
      {
     //  PlayerGo.transform.position = new Vector3(0f,2f,0f);
      }
      InGameCanvasScript.setPlayerGUIobjects();
      GameObject cineMachine = GameObject.FindWithTag("Cinemachine");
      if(cineMachine !=null)
      {
        CinemachineVirtualCamera vcam = cineMachine.GetComponent<CinemachineVirtualCamera>();
        //vcam.Follow = PlayerGo.transform;
      }
      else
      {
        Debug.Log("camera bulunamadı");
      }

    }


    public void MainMenuStart()//menüde start kısmı
    {
      startAnewGame =true;
      PlayerGo.SetActive(true);
      InGameCanvas.SetActive(true);
     SceneManager.LoadScene("SampleScene");//sample sceneyi yükle
     SceneManager.sceneLoaded += OnSceneLoaded;

    }
    
    public void MainMenuLoad()
    { startAnewGame = false;
    Pxdata = SaveLoad.LoadData();
      PlayerGo.SetActive(true);
      InGameCanvas.SetActive(true);
      SceneManager.LoadScene(Pxdata.getCurrentSceneId());
      SceneManager.sceneLoaded += OnSceneLoaded;

    }
    
    public void MainMenuExit()//menüden çıkma
    {
          Application.Quit();//çıkma metodu
    }
}
