#pragma once
#include "../headers/Common.h"
#include "../headers/Pacman.h"
#include "../headers/Ghost.h"
#include "../headers/Board.h"
#include "../headers/GhostManager.h"
#include "../headers/Pellets.h"
#include "../headers/Graph.h"
#include "../headers/Words.h"
#include"../headers/Email.h"
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
	Board* boardManager;
	Pellets* pelletManager;
	GhostManager* ghostManager;
	Words* textManager ;
	Email emailManager;
public:
	GameManager();
	void loadBoardText();
	void createEdges();
	void loadPacman();
	void startGame();
	void Play();
	void checkCollision();
	void draw();
	void sendEmail();
	void gameWon();
	void gameLost();

};

