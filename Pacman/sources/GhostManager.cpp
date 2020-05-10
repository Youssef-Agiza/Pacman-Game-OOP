#pragma once
#include "../headers/GhostManager.h"

#define SPEED_LATENCE 250

GhostManager::GhostManager(Graph* graph):mGraph(graph)
{}

GhostManager::~GhostManager()
{
	mGraph = nullptr;
	for (auto ghost : mGhostList)
		ghost = nullptr;
}

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

void GhostManager::moveAll(Pacman* pacman)
{
	for (auto ghost : mGhostList)
	{
		if (ghost->isInHome() && ghost->getCurrentTime().asSeconds() >= ghost->getHomeTimer())
				ghost->setInHome(false); 
		ghost->callAI(pacman);
	}

}



void GhostManager::createGhost(Board* board,Graph* graph,Pacman* pacman, int speedTimer) //create four ghosts and adds them to ghost manager
{
	//blinky
	Ghost* Blinky = new Ghost(11, 14, board->getTileSize(), board);
	Blinky->resetPosition();
	Blinky->setResource("../images/blinky.png");
	Blinky->setTexture("../images/blinky.png",8,1);
	Blinky->setGraph(graph);
	Blinky->setIntialRow(14).setIntialCol(14);
	(Blinky->ai)=&Ghost::blinky;
	pacman->addObserver(Blinky);
	Blinky->setSpeedTimer( speedTimer);
	Blinky->setHomeTimer(0);

	//Pinky
	Ghost* Pinky = new Ghost(14, 13, board->getTileSize(), board);
	Pinky->resetPosition();
	Pinky->setResource("../images/pinky.png");
	Pinky->setTexture("../images/pinky.png",8,1);
	Pinky->setGraph(graph);
	Pinky->ai = &Ghost::pinky;
	pacman->addObserver(Pinky);
	Pinky->setSpeedTimer(speedTimer);
	Pinky->setHomeTimer(3);

	//Inky
	Ghost* Inky = new Ghost(14, 12, board->getTileSize(), board);
	Inky->resetPosition();
	Inky->setResource("../images/inky.png");
	Inky->setTexture("../images/inky.png",8,1);
	Inky->setGraph(graph);
	(Inky->ai) = &Ghost::inky;
	pacman->addObserver(Inky);
	Inky->setSpeedTimer(speedTimer);
	Inky->setHomeTimer(5);

	

	//Clyde
	Ghost* Clyde = new Ghost(14, 14, board->getTileSize(), board);
	Clyde->resetPosition();
	Clyde->setResource("../images/clyde.png");
	Clyde->setTexture("../images/clyde.png",8,1);
	Clyde->setGraph(graph);
	Clyde->ai = &Ghost::clyde;
	pacman->addObserver(Clyde);
	Clyde->setSpeedTimer(speedTimer);
	Clyde-> setHomeTimer(7);

	addGhost(Blinky);
	addGhost(Inky);
	addGhost(Pinky);
	addGhost(Clyde);

}

