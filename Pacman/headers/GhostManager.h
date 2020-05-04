#pragma once
#include <vector>
#include "Ghost.h"
#include "Board.h"
#include "Pacman.h"
#include "Pellets.h"
class GhostManager
{
private:
	std::vector<Ghost*> mGhostList;
public:
	GhostManager();

	const std::vector<Ghost*>& getGhostList()const;
	
	void addGhost(Ghost* mGhost);
	void draw(sf::RenderWindow& w);

	void createGhost(Board* board);
	void checkGhost2Pacman(Pacman* pacman, Pellets &P);
};