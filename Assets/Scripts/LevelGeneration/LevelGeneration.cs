using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
	Vector2 worldSize = new Vector2(4,4);
	Room[,] rooms;
    Path[,,] paths;
	List<Vector2> takenPositions = new List<Vector2>();
	int gridSizeX, gridSizeY, numberOfRooms = 10, numberOfPath = 4;
	public GameObject RoomSpritePrefab, PathSpritePrefab, gameManager;

	public void CreateLevel (GameObject LevelCreator) {
      
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2)){
			numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
		}
		gridSizeX = Mathf.RoundToInt(worldSize.x);
		gridSizeY = Mathf.RoundToInt(worldSize.y);
		Create();
		SetRoomDoors();
        LevelContainer levelContainer = LevelCreator.GetComponent<LevelContainer>();
        foreach (Room room in rooms)
        {
            if (room != null)
            {
                levelContainer.rooms.Add(room);
            }
        }
        foreach (Path path in paths)
        {
            if (path != null)
            {
                levelContainer.paths.Add(path);
            }
        }
        levelContainer.worldSize = new Vector2((worldSize.x*2)-1, (worldSize.y*2)-1);
        levelContainer.numberOfPath = numberOfPath;
    }

	void Create(){
		rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        paths = new Path[gridSizeX * 2, gridSizeY * 2, numberOfPath];
		rooms[gridSizeX,gridSizeY] = new Room(Vector2.zero, Room.Type.Start, true); // La salle est le début du niveau
        gameManager.GetComponent<GameManager>().CurrentRoom = rooms[gridSizeX, gridSizeY];
		takenPositions.Insert(0,Vector2.zero);
		Vector2 checkPos = Vector2.zero;
		//magic numbers
		float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
		//add rooms
		for (int i = 0; i < numberOfRooms -1; i++){
			float randomPerc = ((float) i) / (((float)numberOfRooms - 1));
			randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
			//grab new position
			checkPos = NewPosition();
			//test new position
			if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare){
				int iterations = 0;
				do{
					checkPos = SelectiveNewPosition();
					iterations++;
				}while(NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
				if (iterations >= 50)
					print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
			}
            //finalize position
            if(i != numberOfRooms - 2)
            {
                int random = Random.Range(1, 10);
                if (random >= 1 && random <= 3) // La salle est une salle vide (30% de chance)
                {
                    rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, Room.Type.Empty, false);
                    takenPositions.Insert(0, checkPos);
                }
                else if (random >= 4 && random <= 5) // La salle contient un trésor (20% de chance)
                {
                    rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, Room.Type.Treasure, false);
                    takenPositions.Insert(0, checkPos);
                }
                else if (random >= 6 && random <= 10) // La salle contient un combat (50% de chance)
                {
                    rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, Room.Type.Battle, false);
                    takenPositions.Insert(0, checkPos);
                }
            }
            else // Dernière itération : la salle est la fin du niveau
            {
                rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, Room.Type.End, false);
                takenPositions.Insert(0, checkPos);
            }
        }	
	}

	Vector2 NewPosition(){
		int x = 0, y = 0;
		Vector2 checkingPos = Vector2.zero;
		do{
			int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
			x = (int) takenPositions[index].x;//capture its x, y position
			y = (int) takenPositions[index].y;
			bool UpDown = (Random.value < 0.5f);//randomly pick wether to look on hor or vert axis
			bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
			if (UpDown){ //find the position bnased on the above bools
				if (positive){
					y += 2;
				}else{
					y -= 2;
				}
			}else{
				if (positive){
					x += 2;
				}else{
					x -= 2;
				}
			}
			checkingPos = new Vector2(x,y);
		}while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); //make sure the position is valid
		return checkingPos;
	}

	Vector2 SelectiveNewPosition(){ // method differs from the above in the two commented ways
		int index = 0, inc = 0;
		int x =0, y =0;
		Vector2 checkingPos = Vector2.zero;
		do{
			inc = 0;
			do{ 
				//instead of getting a room to find an adject empty space, we start with one that only 
				//as one neighbor. This will make it more likely that it returns a room that branches out
				index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
				inc ++;
			}while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
			x = (int) takenPositions[index].x;
			y = (int) takenPositions[index].y;
			bool UpDown = (Random.value < 0.5f);
			bool positive = (Random.value < 0.5f);
			if (UpDown){
				if (positive){
					y += 2;
				}else{
					y -= 2;
				}
			}else{
				if (positive){
					x += 2;
				}else{
					x -= 2;
				}
			}
			checkingPos = new Vector2(x,y);
		}while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
		if (inc >= 100){ // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
			print("Error: could not find position with only one neighbor");
		}
		return checkingPos;
	}

	int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions){
		int ret = 0; // start at zero, add 1 for each side there is already a room
		if (usedPositions.Contains(checkingPos + Vector2.right)){ //using Vector.[direction] as short hands, for simplicity
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.left)){
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.up)){
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.down)){
			ret++;
		}
		return ret;
	}

    //Pour chaque coordonnées de la grille, on regarde si elle contient une pièce
    //Si oui, on regarde si elle possède des voisins dans chaque directions
    //Si oui, on crée un chemin entre ces deux pièces
	void SetRoomDoors(){
		for (int x = 0; x < ((gridSizeX * 2)); x++){
			for (int y = 0; y < ((gridSizeY * 2)); y++){
				if (rooms[x,y] == null){ //Si pas de pièce on continue
					continue;
				}
				if (y - 2 < 0){ //Vérification en bas
                    rooms[x,y].doorBot = false;
				}else{
                    if(rooms[x, y - 2] != null)
                    {
                        rooms[x, y].doorBot = true;
                        if (paths[x, y, 0] == null)
                        {
                            for(int z = 0; z < numberOfPath; z++)
                            {
                                paths[x, y - 1, z] = new Path(new Vector2(rooms[x, y].gridPos.x, rooms[x, y].gridPos.y - 1), z, true);
                            }
                        }
                    }
                }
				if (y + 2 >= gridSizeY * 2){ //Vérification en haut
                    rooms[x,y].doorTop = false;
				}else{
                    if(rooms[x, y + 2] != null)
                    {
                        rooms[x, y].doorTop = true;
                        if (paths[x, y, 0] == null)
                        {
                            for (int z = 0; z < numberOfPath; z++)
                            {
                                paths[x, y + 1, z] = new Path(new Vector2(rooms[x, y].gridPos.x, rooms[x, y].gridPos.y + 1), z, true);
                            }
                        }
                    }
                }
				if (x - 2 < 0){ //Vérification à gauche
                    rooms[x,y].doorLeft = false;
				}else{
                    if(rooms[x - 2, y] != null)
                    {
                        rooms[x, y].doorLeft = true;
                        if (paths[x, y, 0] == null)
                        {
                            for (int z = 0; z < numberOfPath; z++)
                            {
                                paths[x - 1, y, z] = new Path(new Vector2(rooms[x, y].gridPos.x - 1, rooms[x, y].gridPos.y), z, false);
                            }
                        }
                    }
                }
				if (x + 2 >= gridSizeX * 2){ //Vérification à droite
					rooms[x,y].doorRight = false;
				}else{
                    if(rooms[x + 2, y] != null)
                    {
                        rooms[x, y].doorRight = true;
                        if (paths[x, y, 0] == null)
                        {
                            for (int z = 0; z < numberOfPath; z++)
                            {
                                paths[x + 1, y, z] = new Path(new Vector2(rooms[x, y].gridPos.x + 1, rooms[x, y].gridPos.y), z, false);
                            }
                        }
                    }
                }
			}
		}
	}
}
