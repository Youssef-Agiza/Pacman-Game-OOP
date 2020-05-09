#pragma once
#include "../headers/Common.h"
#include "../headers/Pacman.h"
#include "../headers/Ghost.h"
#include "../headers/Board.h"
#include "../headers/GhostManager.h"
#include "../headers/Pellets.h"
#include "../headers/Graph.h"
#include "../headers/Words.h"
using namespace std;
using namespace sf;
using namespace PacmanCS;
class GameManager

{
private:
	float TileSize;
	RenderWindow window;
	bool once = true;
	vector<vector<int>> arr;
	Graph graph;
	Pacman* pacman;
	Board* myBoard;
	Pellets* P;
	GhostManager* manager;
	Words* text ;
	
public:
	GameManager();

	void loadBoardText();

	void createEdges();

	
	
	void loadPacman();

	void Play();

	void checkCollision();


};

