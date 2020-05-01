#pragma once
#include "GhostManager.h"

const float SPEED = 0.4f;
GhostManager::GhostManager()
{}

void GhostManager::addGhost(Ghost* ghost)
{
	if (ghost != nullptr && mGhostList.size() < 4)
		mGhostList.push_back(ghost);
}

const std::vector<Ghost*>& GhostManager::getGhostList()const
{
	return mGhostList;
}
void GhostManager::draw(sf::RenderWindow& window)
{
	for (unsigned int i = 0; i < mGhostList.size(); i++)
		mGhostList[i]->drawOnWindow(window);
}
void GhostManager::checkGhost2Pacman(Pacman* pacman, Pellets &P)
{
	for (auto ghost : mGhostList)
		if (ghost->getRow() == pacman->getRow() && ghost->getCol() == pacman->getCol())
		{
			if (ghost->getFreight())
			{
				ghost->die();
				P.incrementScore(500);
			}
			else
				pacman->die();
			return;
		}
	
}

void GhostManager::createGhost(Board* board) //create four ghosts and adds them to ghost manager
{
	//blinky
	Ghost* Blinky = new Ghost(11, 14, board->getTileSize(), board->getPositionOneWindow());
	Blinky->resetPosition();
	Blinky->setBoard(board);
	Blinky->setResource("../images/blinky.png");
	Blinky->setTexture("../images/blinky.png",8,1);

	//Inky
	Ghost* Inky = new Ghost(14, 12, board->getTileSize(), board->getPositionOneWindow());
	Inky->resetPosition();
	Inky->setBoard(board);
	Inky->setResource("../images/inky.png");
	Inky->setTexture("../images/inky.png",8,1);

	//Pinky
	Ghost* Pinky = new Ghost(14, 13, board->getTileSize(), board->getPositionOneWindow());
	Pinky->resetPosition();
	Pinky->setBoard(board);
	Pinky->setResource("../images/pinky.png");
	Pinky->setTexture("../images/pinky.png",8,1);

	
	//Clyde
	Ghost* Clyde = new Ghost(14, 14, board->getTileSize(), board->getPositionOneWindow());
	Clyde->resetPosition();
	Clyde->setBoard(board);
	Clyde->setResource("../images/clyde.png");
	Clyde->setTexture("../images/clyde.png",8,1);
	

	addGhost(Blinky);
	addGhost(Inky);
	addGhost(Pinky);
	addGhost(Clyde);

}
