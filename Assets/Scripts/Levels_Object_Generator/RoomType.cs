using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    // Ici on detruit un pieces non desiré et on la remplace par une autre
    //ex: si une piece n'a pas de sortie vers le bas et que la pieces suivante a une sortie vers le haut

    //Respecte l'ordre des prefabs
    //si index 0 on spawn prefabs LR -- si 1 LRB, si 2 LRT, si 3 LRBT
    //Donc LR = 0, LRB = 1, LRT = 2, LRBT = 3
    //Ce script est sur chaque room prefabs et possète un entier type différent
    public int type;
    //Methode public a appeler dans LevelGeneration
    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
   
}
