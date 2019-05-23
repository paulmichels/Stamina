using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dungeon
{
    public enum Zone : short { Mathematiques, Algorithmique, Physique, Communication, Reseaux }
    public enum Direction : short { Up, Down, Left, Right }

    public static int DungeonSizeXMax { get; private set; }
    public static int DungeonSizeXMin { get; private set; }
    public static int DungeonSizeYMax { get; private set; }
    public static int DungeonSizeYMin { get; private set; }
    public static int NumberOfRooms   { get; private set; }
    public static int NumberOfParcels { get; private set; }

    public static IRoom[,] Rooms { get; private set; }
    public static IParcel[,,] Parcels { get; private set; }
    public static Vector3Int PlayerPosition;

    //Scene Room
    public static IRoom CurrentRoom { get; set; }
    public static IRoom EndRoom { get; set; }
    public static IRoom PreviousRoom { get; set; }
    public static IRoom NextRoom { get; set; }
    public static Direction direction { get; set; }
    public static Zone zone { get; set; }

    private static List<Vector2Int> takenPositions = new List<Vector2Int>();

    public static  void CreateDungeon(int numberOfRooms, int numberOfParcels, Zone zone)
    {
        NumberOfRooms = numberOfRooms;
        NumberOfParcels = numberOfParcels;
        Dungeon.zone = zone;

        //La taille doit être pair
        DungeonSizeXMax = NumberOfRooms - (numberOfRooms % 2);
        DungeonSizeYMax = NumberOfRooms - (numberOfRooms % 2);

        //On s'assure que le nombre de salle n'est pas plus grand que la taille du donjon
        if (NumberOfRooms >= (DungeonSizeXMax * DungeonSizeYMax )/4)
        {
            NumberOfRooms = (DungeonSizeXMax * DungeonSizeYMax)/4;
        }

        Rooms = new IRoom[DungeonSizeXMax, DungeonSizeYMax];
        Parcels = new IParcel[DungeonSizeXMax, DungeonSizeYMax, NumberOfParcels];

        CreateRooms();
        CreateParcels();
        AdaptDungeonSize();
    }

    private static void CreateRooms()
    {
        //On instancie le début du niveau dans une position aléatoire aux coordonnées paires
        int randomStartPositionX = Random.Range(0, DungeonSizeXMax / 2) * 2;
        int randomStartPositionY = Random.Range(0, DungeonSizeYMax / 2) * 2;
        Vector2Int randomPosition = new Vector2Int(randomStartPositionX, randomStartPositionY);

        Rooms[randomStartPositionX, randomStartPositionY] = new StartRoom(randomPosition);
        Rooms[randomStartPositionX, randomStartPositionY].Explored = true;
        CurrentRoom = Rooms[randomStartPositionX, randomStartPositionY];
        PlayerPosition = new Vector3Int(randomStartPositionX, randomStartPositionY, 0);
        takenPositions.Insert(0, randomPosition);

        for (int i = 1; i < NumberOfRooms; i++)
        {
            bool condition = false;

            while (!condition)
            {
                //On prend une position de salle au hasard dans la liste
                Vector2Int randomTakenPosition = takenPositions[Random.Range(0, takenPositions.Count)];

                //Direction aléatoire
                bool isVertical = (Random.value < 0.5f);
                bool isPositive = (Random.value < 0.5f);

                if (isVertical) //Nouveau voisin en haut ou en bas
                {
                    //Check la direction, si cette position se trouve dans le tableau et si la place est libre
                    if (isPositive && randomTakenPosition.y + 2 <= DungeonSizeYMax - 1 && Rooms[randomTakenPosition.x, randomTakenPosition.y + 2] == null)
                    {
                        NewNeighbour(randomTakenPosition, new Vector2Int(randomTakenPosition.x, randomTakenPosition.y + 2), i);
                        condition = true;
                    } else if (!isPositive && randomTakenPosition.y - 2 >= 0 && Rooms[randomTakenPosition.x, randomTakenPosition.y - 2] == null)
                    {
                        NewNeighbour(randomTakenPosition, new Vector2Int(randomTakenPosition.x, randomTakenPosition.y - 2), i);
                        condition = true;
                    }
                }
                else //Nouveau voisin à gauche ou à droite
                {
                    if (isPositive && randomTakenPosition.x + 2 <= DungeonSizeXMax - 1 && Rooms[randomTakenPosition.x + 2, randomTakenPosition.y] == null)
                    {
                        NewNeighbour(randomTakenPosition, new Vector2Int(randomTakenPosition.x + 2, randomTakenPosition.y), i);
                        condition = true;
                    }
                    else if (!isPositive && randomTakenPosition.x - 2 >= 0 && Rooms[randomTakenPosition.x - 2, randomTakenPosition.y] == null)
                    {
                        NewNeighbour(randomTakenPosition, new Vector2Int(randomTakenPosition.x - 2, randomTakenPosition.y), i);
                        condition = true;
                    }
                }
            }
        }
    }

    //TODO : Vérifier si chemin existe
    private static void CreateParcels()
    {
        foreach(Vector2Int roomPosition in takenPositions)
        {
            if (roomPosition.y + 2 <= DungeonSizeYMax - 1 && Rooms[roomPosition.x, roomPosition.y + 2] != null)
            {
                NewParcelSet(new Vector2Int(roomPosition.x, roomPosition.y + 1));
            }
            if (roomPosition.y - 2 >= 0 && Rooms[roomPosition.x, roomPosition.y - 2] != null)
            {
                NewParcelSet(new Vector2Int(roomPosition.x, roomPosition.y - 1));
            }
            if (roomPosition.x + 2 <= DungeonSizeXMax - 1 && Rooms[roomPosition.x + 2, roomPosition.y] != null)
            {
                NewParcelSet(new Vector2Int(roomPosition.x + 1, roomPosition.y));
            }
            if (roomPosition.x - 2 >= 0&& Rooms[roomPosition.x - 2, roomPosition.y] != null)
            {
                NewParcelSet(new Vector2Int(roomPosition.x - 1, roomPosition.y));
            }
        }
    }

    private static void NewParcelSet(Vector2Int position)
    {
        if (Parcels[position.x, position.y, 0] == null)
        {
            for (int z = 0; z < NumberOfParcels; z++)
            {
                Parcels[position.x, position.y, z] = RandomizeParcel(new Vector3Int(position.x, position.y, z));
            }
        }
    }

    private static IParcel RandomizeParcel(Vector3Int position)
    {
        IParcel parcel = null;
        int random = Random.Range(1, 101);
        if (random >= 1 && random <= 20)
        {
            parcel = new BattleParcel(position);
        }
        else if (random >= 21 && random <= 40)
        {
            parcel = new TreasureParcel(position);
        }
        else if (random >= 41 && random <= 60)
        {
            parcel = new EmptyParcel(position);
        }
        else if (random >= 61 && random <= 80)
        {
            parcel = new TrapParcel(position);
        }
        else if (random >= 81 && random <= 100)
        {
            parcel = new BookParcel(position);
        }
        return parcel;
    }


    private static IRoom RandomizeRoom(Vector2Int position)
    {
        return new BattleRoom(position);
    }
    /*
    private static IRoom RandomizeRoom(Vector2Int position)
    {
        IRoom room = null;
        int random = Random.Range(1, 101);
        if (random >= 1 && random <= 20)
        {
            room = new BattleRoom(position);
        }
        else if (random >= 21 && random <= 40)
        {
            room = new TreasureRoom(position);
        }
        else if (random >= 41 && random <= 80)
        {
            room = new EmptyRoom(position);
        }
        else if (random >= 81 && random <= 100)
        {
            room = new BossRoom(position);
        }
        return room;
    }
    */

    //TODO : return bool
    private static void NewNeighbour(Vector2Int roomPosition, Vector2Int newNeighbourPosition, int i)
    {
        if(i == NumberOfRooms - 1)
        {
            Rooms[newNeighbourPosition.x, newNeighbourPosition.y] = new EndRoom(newNeighbourPosition);
        }
        else
        {
            Rooms[newNeighbourPosition.x, newNeighbourPosition.y] = RandomizeRoom(newNeighbourPosition);
        }
        takenPositions.Insert(i, newNeighbourPosition);
    }

    private static void AdaptDungeonSize()
    {
        int startX = DungeonSizeXMax, 
            endX = 0, 
            startY = DungeonSizeYMax, 
            endY = 0;
        foreach (Vector2Int position in takenPositions)
        {
            if (position.x < startX)
            {
                startX = position.x;
            }
            if (position.x> endX)
            {
                endX = position.x;
            }
            if(position.y < startY)
            {
                startY = position.y;
            }
            if (position.y > endY)
            {
                endY = position.y;
            }
        }
        DungeonSizeXMin = startX;
        DungeonSizeXMax = endX;
        DungeonSizeYMin = startY;
        DungeonSizeYMax = endY;
    }
}
