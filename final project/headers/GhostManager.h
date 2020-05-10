#pragma once
#include <vector>
#include "../headers/Ghost.h"
#include "../headers/Board.h"
#include "../headers/Pacman.h"
#include "../headers/Pellets.h"
#include"../headers/Graph.h"
class GhostManager
{
private:
	std::vector<Ghost*> mGhostList;
	Graph* mGraph;
public:
	GhostManager(Graph* graph);
	~GhostManager();

	const std::vector<Ghost*>& getGhostList()const;
	
	void addGhost(Ghost* mGhost);
	void draw(sf::RenderWindow& w);

	void createGhost(Board* board,Graph* graph,Pacman* pacman, int speed=250);

	void moveAll(Pacman* pacman);
};