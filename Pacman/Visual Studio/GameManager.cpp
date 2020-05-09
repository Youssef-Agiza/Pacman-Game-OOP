#include "GameManager.h"

GameManager::GameManager() :arr(vector <vector<int>>(31, vector<int>(28))),TileSize(30.0f)
{
	textManager = new Words();
	Vector2f POSITION(500, 0);
	window.create(VideoMode(1840, 930), "Simple Maze");
	this->loadBoardText();
	boardManager = new Board(arr, TileSize, POSITION);
	pacman = new Pacman(1, 1, TileSize, POSITION);
	ghostManager = new GhostManager(&graph);
	ghostManager->createGhost(boardManager, &graph, pacman);
	pelletManager = new Pellets(arr);
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
					graph.addEdge(arr[i][j], arr[i][j + 1]);
				if (i + 1 != arr.size() && arr[i + 1][j] > -1)
					graph.addEdge(arr[i][j], arr[i + 1][j]);
			}
		}
	graph.addEdge(149, 130);//connects portals
}

void GameManager::loadPacman()
{
	pacman->setBoard(boardManager);
	pacman->resetPosition();
	pacman->setTexture("../images/pacman.png", 2, 4);
}

void GameManager:: sendEmail()
{
	emailManager.sendEmail();
}

void GameManager::startGame()
{
	
	Event x;
	window.clear(Color::Black);
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
				window.clear();
				Play();
			}

		}
	}
}

void GameManager::Play()
{
	Clock pactimer, gtimer;
	pactimer.restart();
	gtimer.restart();
	window.setFramerateLimit(20);
	Event e;
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



		


		if (gtimer.getElapsedTime().asMilliseconds() >= 400)
		{
			ghostManager->moveAll(pacman);
			gtimer.restart();
		}
		if (pactimer.getElapsedTime().asMilliseconds() >= 250)
		{
			pactimer.restart();
			pacman->move();
		}


		window.clear();

		if ((pacman->getScore() >= 10000) && (once))
		{
			pacman->addLive(1);//once score is greater than 10000 the life is incremented only once
			once = false;
		}

		checkCollision();
		pelletManager->intersectPellets(pacman);


		draw();

		window.display();
		if (pacman->getLives() == 0)
			this->gameLost();
		else if (pelletManager->mPelletCount==0)
			this->gameWon();
	}
}

void GameManager::checkCollision()
{
	pacman->checkPowerUpTime();


	for (auto ghost : manager->getGhostList())
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
				this->boardManager->drawOnWindow(window);
				pelletManager->drawPellets(window, boardManager->mShape);

				pacman->die(window);
				for (int i = 0; i < 4; i++)
					ghostManager->getGhostList()[i]->resetPosition();

			}
			return;
		}

}

void GameManager::draw()
{
//	boardManager->drawOnWindow(window);
	pelletManager->drawPellets(window, boardManager->mShape);
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
