#include "../headers/GameManager.h"

GameManager::GameManager() :arr(vector <vector<int>>(31, vector<int>(28))), tileSize(30.0f)
{
	//	Vector2f POSITION(500, 0);
	window.create(VideoMode(1840, 930), "Simple Maze");
	graph = new Graph();
	this->loadBoardText();
	this->createEdges();
	this->createEntities(1);
	this->Play();
}
GameManager::~GameManager()
{
	if (board != nullptr)
		delete board;
	if (pacman != nullptr)
		delete pacman;
	if (ghostManager != nullptr)
		delete ghostManager;
	if (pellet != nullptr)
		delete pellet;
	if (textManager != nullptr)
		delete textManager;

	board = nullptr;
	pacman = nullptr;
	ghostManager = nullptr;
	textManager = nullptr;
	pellet = nullptr;
	
}
void GameManager::loadBoardText()
{
	ifstream ins("../boardTexts/BoardText3.txt");
	if (ins.is_open())
	{
		for (unsigned int i = 0; i < arr.size(); i++)
			for (unsigned int j = 0; j < arr[i].size(); j++)
				ins >> arr[i][j];
	}
}

void GameManager::createEdges()
{
	for (unsigned int i = 0; i < arr.size(); i++)
		for (unsigned int j = 0; j < arr[i].size(); j++)
		{
			if (arr[i][j] > -1)
			{
				if (j + 1 != arr[i].size() && arr[i][j + 1] > -1)
					graph->addEdge(arr[i][j], arr[i][j + 1]);
				if (i + 1 != arr.size() && arr[i + 1][j] > -1)
					graph->addEdge(arr[i][j], arr[i + 1][j]);
			}
		}
	graph->addEdge(149, 130);//connects portals
}

void GameManager::createEntities(int level)
{
	this->loadBoardText();
	if (board != nullptr)
		delete board;
	if (pacman != nullptr)
		delete pacman;
	if (ghostManager != nullptr)
		delete ghostManager;
	if (pellet != nullptr)
		delete pellet;
	if (textManager != nullptr)
		delete textManager;

	board = new Board(arr, tileSize, Vector2f(500, 0));
	pacman = new Pacman(1, 1, tileSize, board);
	ghostManager = new GhostManager(graph);
	ghostManager->createGhost(board, graph, pacman);
	pellet = new Pellets(arr);
	textManager = new Words();
}


void GameManager:: sendEmail()
{
	//emailManager.sendEmail();
}

void GameManager::startGame()
{
	
	Event x;
	draw(); 
	textManager->drawTextInHome(window, "Ready ");
	window.display();

	while (window.isOpen())
	{
		
		while (window.pollEvent(x))
		{
			
			if (x.type == Event::Closed)
				window.close();
			else if (x.key.code == Keyboard::Space)
			{
				return;
			}

		}

	}
}

void GameManager::Play()
{

	startGame();
	
	Event e;
	pacman->resetTime();
	while (window.isOpen())
	{
		while (window.pollEvent(e))
		{
			if (e.type == Event::Closed)
				window.close();
			else if (e.type == Event::KeyPressed)
			{
				switch (e.key.code)
				{
				case Keyboard::Up:
					pacman->setDirection(UP);
					break;
				case Keyboard::Down:
					pacman->setDirection(DOWN);
					break;
				case Keyboard::Right:
					pacman->setDirection(RIGHT);
					break;
				case Keyboard::Left:
					pacman->setDirection(LEFT);
					break;
				case Keyboard::F:
					pacman->setPowerUp(!pacman->getPowerUp()); break;


				default:break;
				}
			}
		}

		ghostManager->moveAll(pacman);
		pacman->move();
		checkCollision();
		pellet->intersectPellets(pacman);

		window.clear();
		draw();
		window.display();

		if (pacman->getLives() == 0)
			this->gameLost();
		else if (pellet->mPelletCount == 0)
			this->gameWon();
	}
}

void GameManager::checkCollision()
{
	pacman->checkPowerUpTime();


	for (auto ghost : ghostManager->getGhostList())
		if (ghost->getSprite().getGlobalBounds().intersects(pacman->getSprite().getGlobalBounds()))
		{
			if (ghost->getFreight())
			{
				ghost->die(window);
				Audio::getInstance()->playEatGhost();
				pacman->incrementScore(500);
			}
			else
			{
				window.clear();
				window.display();
				window.clear();
				this->board->drawOnWindow(window);
				pellet->drawPellets(window, board->mShape);

				pacman->die(window);
				for (int i = 0; i < 4; i++)
					ghostManager->getGhostList()[i]->resetPosition();

			}
			return;
		}

}

void GameManager::draw()
{
	board->drawOnWindow(window);
	pellet->drawPellets(window, board->mShape);
	pacman->drawOnWindow(window);
	ghostManager->draw(window);
	textManager->drawText(window, pacman);
}

void GameManager:: gameWon()
{
	Clock timer;
	while (timer.getElapsedTime().asSeconds() < 3)
	{
		window.clear();
		textManager->drawTextInHome(window, "WON!!!");
		window.display();
	}
	window.close();

}

void GameManager::gameLost()
{
	Clock timer;
	while (timer.getElapsedTime().asSeconds() < 3)
	{
		window.clear();
		textManager->drawTextInHome(window, "You SUCK");
		window.display();
	}
	window.close();
}
