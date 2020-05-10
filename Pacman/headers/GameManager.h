#pragma once
#include "../headers/Common.h"
#include "../headers/Pacman.h"
#include "../headers/Ghost.h"
#include "../headers/Board.h"
#include "../headers/GhostManager.h"
#include "../headers/Pellets.h"
#include "../headers/Graph.h"
#include "../headers/Words.h"
//#include"../headers/Email.h"
using namespace std;
using namespace sf;
using namespace PacmanCS;
enum GameModes{Normal,infinity ,Turbo};
class GameManager

{
private:
	float tileSize;
	RenderWindow window;
	vector<vector<int>> arr;
	Graph* graph;
	Pacman* pacman;
	Board* board;
	Pellets* pellet;
	GhostManager* ghostManager;
	Words* textManager ;
	int levels;
	//Email emailManager;

private:
	void loadBoardText();
	void createEdges();
	void createEntities(int level);
	void gameWon();
	void gameLost();
	void checkCollision();
	void draw();
	void startGame();
public:
	GameManager();
	~GameManager();
	/*void initiate();*/
	void Play();
	void sendEmail();
	

};

