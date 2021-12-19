using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    // Tableau des positions
    public Transform[] startingposition;
    //Tableau des pieces = rooms
    //Ici si index 0 on spawn prefabs LR -- si 1 LRB, si 2 LRT, si 3 LRBT
    //Ordre des prefabs dans la hiearchie unity
    public GameObject[] rooms;
    //la direction des pieces
    private int direction;
    //Nombre de case de deplacement
    public float moveAmount;

    //Delay entre chaque instance de pieces
    public float timeBtwRoom;
    //Valeur par defaut 0
    public float startTimeBtwRoom = .25f;

    //Les limites de la zone de jeu
    //Les abcisse
    public float minX;
    public float maxX;
    //Les ordonnées
    public float minY;

    //Booleen  = stop 
    public bool stopGeneration = false;

    //On ajoute un layermask room a chaque prefab
    public LayerMask room;

    //Decompter le type de piece
    private int downCounter;


    void Start()
    {
        //Random des positions
        int randStartingPosition = Random.Range(0, startingposition.Length);
        //Position de depart randomisée
        transform.position = startingposition[randStartingPosition].position;
        //Instance des piéces a la position
        Instantiate(rooms[0], transform.position, transform.rotation);
        //Random entre 0 et 5
        direction = Random.Range(1, 6);
    }

    private void Move()
    {
        //Si direction (random) === 1 ou 2
        //On bouge a droite
        if (direction == 1 || direction == 2)
        {

            //Si la position en x est < a maxX = on va a droite
            if (transform.position.x < maxX)
            {
                //reset downcounter quand la piece va a droite
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                //Random des pieces
                int rand = Random.Range(0, rooms.Length);
                //Piece (LR + LRB + LRT + LRBT) au hasard
                Instantiate(rooms[rand], transform.position, transform.rotation);

                //Eviter les piéces l'une sur l'autre
                //On manipule les directions
                direction = Random.Range(1, 6);
                //Le resultat:
                //si le random sort 3 (soit gauche) on l'oblige a aller a droite
                if (direction == 3)
                {
                    direction = 2;

                }
                //si apres 3 il va encore a gauche (donc 4)
                //On va vers le bas = 5
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                //Sinon on va vers les bas
                direction = 5;
            }

        }
        else if (direction == 3 || direction == 4)
        {

            //Si la position est > a minX = on va a gauche
            if (transform.position.x > minX)
            {
                //Reset dowcounter pour les piece a gauche
                downCounter = 0;
                //Va a gauche
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                //Random des pieces
                int rand = Random.Range(0, rooms.Length);
                //Piece (LR + LRB + LRT + LRBT) au hasard
                Instantiate(rooms[rand], transform.position, transform.rotation);

                //apres le 1er a gauche on random entre 3 et 6
                direction = Random.Range(3, 6);

            }
            else
            {
                //Sinon on va vers le bas
                direction = 5;
            }

        }
        else if (direction == 5)
        {

            //A chaque instance de piece vers le bas On incremente downCounter = 0
            downCounter++;
            //Si la postion de y est > que minY = on va en bas
            if (transform.position.y > minY)
            {

                //On creer un colider pour detecter le type de room
                //1 = (vecteur2) postion --  2 (radius = rayon d'un cercle(2d) ou d'une sphere(3d)) -- 3 = le masque de detection = layermask room
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                //Dans une condition on recup le type de piece du prefab grace au script roomType
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {

                    //Si downCounter est > a 2 on instance une  piece a 4 sortie (si 3 LRBT)
                    if (downCounter >= 2)
                    {
                        //On detruit la mauvaise piece et on remplace avec LRBT
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, transform.rotation);
                    }
                    else
                    {
                        //Si c pas LRB et LRBT = on detruit la piece pour la remplacer
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        //On la remplace par 1 ou 3
                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, transform.rotation);
                    }
                }

                //Va vers le bas
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                //ici on s'assure que la piece au dessus est LRB sinon on est coincé
                //Random des pieces = on impose index 2 et 3 soit si 2 LRT, si 3 LRBT
                int rand = Random.Range(2, 4);
                //Piece (LR + LRB + LRT + LRBT) au hasard
                Instantiate(rooms[rand], transform.position, transform.rotation);

                //Apres une piece vers le bas peu importe la direction
                direction = Random.Range(1, 6);
            }
            else
            {
                //On stop la generation
                stopGeneration = true;
            }

        }
    }


    // 60fps
    void Update()
    {
        //Debug.Log(downCounter);
        //Si le temps entre chaque insatnce de piece est <= 0 : on appel Move()
        //Soit toutes les 0.25 seconde
        //Quand c fini timeBtwRoom est decementer pour atteindre 0 grace a Time.deltaTime
        if (timeBtwRoom <= 0 && !stopGeneration)
        {
            Move();
            //Quand c egale a 0.25
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }
}
