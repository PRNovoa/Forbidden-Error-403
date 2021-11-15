﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  

/// <summary>
/// Clase con la funcionalidad del personaje
/// </summary>
public class BattleBehaviour : MonoBehaviour
{
    //Variables para manejar movimineto en combate
    public int moveRange;
    private BoundsInt moveArea;

    // Estados del combate
    [HideInInspector]
    public StateManager combatState;

    // Variables de stats personajes máximas
    public int megas, ram, attack, defense, iniciative;

    // Stats de personajes en el momento
    [HideInInspector]
    public int currentMegas, currentRam, currentXp;

    // Detectar si es enemigo en combaye, marcar true en el prefab
    public bool isEnemy;

    public BaseIA ia;

    //Lista de estados alterados
    private List<AlteredStateManager> alteratedState = new List<AlteredStateManager>();

    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        //se inician las estadisticas del personaje
        combatState = StateManager.ooc;
        currentXp = 0;
    }

    /// <summary>
    /// Getter de iniciativa de personaje
    /// </summary>
    public int getIniciative()
    {
        return iniciative;
    }

    /// <summary>
    /// Getter de area disponible del movimiento pasandole la posicion del objeto
    /// </summary>
    public BoundsInt getMoveArea(Vector3Int position)
    {
        moveArea.position = position + new Vector3Int(-1 * moveRange,-1 * moveRange, 0);
        moveArea.size = new Vector3Int(moveRange * 2 + 1, moveRange * 2 + 1, 1);
        return moveArea;
    }

    /// <summary>
    /// setter de estados de combate
    /// </summary>
    public void setState(int state)
    {
        switch (state)
        {
            case 0:
                combatState = StateManager.move;
                break;
            case 1:
                combatState = StateManager.attack;
                break;
            case 2:
                combatState = StateManager.ooc;
                break;
        }
    }

    /// <summary>
    /// Getter de estado de combate
    /// </summary>
    public StateManager getState()
    {
        return combatState;
    }

    /// <summary>
    /// Getter del texto de vida actual junto con el máximo
    /// </summary>
    public string getHpText()
    {
        //Bug con el valor actual currentMegas
        return currentMegas + " / " + megas;
    }

    /// <summary>
    /// Getter del texto de ram actual junto con el máximo
    /// </summary>
    public string getRamText()
    {
        //Bug con el valor de currentRam
        return currentRam + " / " + ram;
    }

    public void AlteredStatesSetter (int code, int turn)
    {
        bool incap = false;
        AlteredStateManager AltState = new AlteredStateManager(code, turn);
        if (!alteratedState.Contains(AltState))
        {
            if (AltState.isStackable())
            {
                alteratedState.Add(AltState);
            }
            else
            {

                for (int i = 0; i < alteratedState.Count; ++i)
                {
                    if (!alteratedState[i].isStackable()) incap = true;
                }
                if (incap == false)
                {
                    alteratedState.Add(AltState);
                }
            }
        }
    }

    public void ApplyAlteredState()
    {
        if (alteratedState.Any()) return;
        for (int i = 0; i < alteratedState.Count; i++)
        {
            switch (i)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
            }
            --alteratedState[i].turn;

            if (alteratedState[i].turn <= 0) alteratedState.RemoveAt(i);  
        } 
    }

    public bool GetHit(int quantity)
    {
        if (quantity >= currentMegas)
        {
            MovementOOC ooc = GetComponent<MovementOOC>();
            if (ooc != null && ooc.isLeader)
            {
                currentMegas = 0;
                GetComponent<SpriteRenderer>().enabled = false;
                GameManager.Instance.ChangeLeader();
            }
            else
            {
                currentMegas = 0;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            transform.position = new Vector3();
        }
        else
        {
            currentMegas -= quantity;
        }

        return currentMegas <= 0;
    }

    public void resetRam()
    {
        currentRam = ram;
    }
}
