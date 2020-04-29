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
void GhostManager::checkGhost2Pacman(Pacman* pacman)
{
	for (auto ghost : mGhostList)
		if (ghost->getRow() == pacman->getRow() && ghost->getCol() == pacman->getCol())
		{
			if (ghost->getFreight())
				ghost->die();
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
	Blinky->setSpeed(SPEED);
	Blinky->setTexture("../images/blinky.png");

	//Inky
	Ghost* Inky = new Ghost(14, 12, board->getTileSize(), board->getPositionOneWindow());
	Inky->resetPosition();
	Inky->setBoard(board);
	Inky->setSpeed(SPEED);
	Inky->setTexture("../images/inky.png");

	//Pinky
	Ghost* Pinky = new Ghost(14, 13, board->getTileSize(), board->getPositionOneWindow());
	Pinky->resetPosition();
	Pinky->setBoard(board);
	Pinky->setSpeed(SPEED);
	Pinky->setTexture("../images/pinky.png");

	
	//Clyde
	Ghost* Clyde = new Ghost(14, 14, board->getTileSize(), board->getPositionOneWindow());
	Clyde->resetPosition();
	Clyde->setBoard(board);
	Clyde->setSpeed(SPEED);
	Clyde->setTexture("../images/clyde.png");
	

	addGhost(Blinky);
	addGhost(Inky);
	addGhost(Pinky);
	addGhost(Clyde);

}
