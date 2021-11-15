using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase destinada al control de funcionamientos generales del juego jugable
/// </summary>
public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public GameObject[] viruses;
    public Mapa mapa;
    public GameObject menuPausa;
    public CombatManager combatScript;

    [HideInInspector]
    public bool canMove;
    //public GameObject tilemap;

    public static GameManager Instance
    {
        get { return instance; }
    }
    // Start is called before the first frame update
    void Start()
    {
        Room.numRoomsCreated = 0;
        mapa.createMap();
        SetStart();
        canMove = true;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuPausa.activeSelf)
            {
                if (combatScript.inCombat)
                    combatScript.inPause = true;
                canMove = false;
                menuPausa.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                if (!combatScript.inCombat)
                    canMove = true;
                else
                    combatScript.inPause = false;
                menuPausa.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Movement(0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                Movement(1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Movement(2);
            }
            if (Input.GetKey(KeyCode.A))
            {
                Movement(3);
            }
        }
    }

    private void Movement(int code)
    {
        if (viruses[0].GetComponent<MovementOOC>().moveState == MoveState.stand)
        {
            Vector3 movePos;
            movePos = viruses[0].GetComponent<MovementOOC>().SetMove(code);
            for (int i = 1; i < viruses.Length; ++i)
            {
                if (viruses[i].GetComponent<BattleBehaviour>().currentMegas != 0)
                    movePos = viruses[i].GetComponent<MovementOOC>().Cola(movePos);
            }
        }
    }

    public void ChangeLeader()
    {
        GameObject auxiliar = viruses[0];
        viruses[0].GetComponent<MovementOOC>().isLeader = false;
        int i;
        for (i = 0; i < viruses.Length; ++i)
        {
            if (viruses[i].GetComponent<BattleBehaviour>().currentMegas == 0)
                auxiliar = viruses[i];
            int j;
            for (j = i; j < viruses.Length - 1 && viruses[j].GetComponent<BattleBehaviour>().currentMegas == 0; ++j)
                ;
            viruses[i] = viruses[j];
            viruses[j] = auxiliar;
        }
        viruses[0].GetComponent<MovementOOC>().isLeader = true;
    }

    public void SetMap(int[,] map)
    {
        for (int i = 1; i < viruses.Length; ++i)
        {
            viruses[i].GetComponent<MovementOOC>().setMapa(map);
        }
    }

    private void SetStart()
    {
        for (int i = 0; i < viruses.Length; ++i)
            viruses[i].GetComponent<MovementOOC>().StartPos();
    }

    public void CombatMove()
    {
        if (canMove)
            canMove = false;
        else
            canMove = true;
    }

    public GameObject GetLeader()
    {
        return viruses[0];
    }
}
