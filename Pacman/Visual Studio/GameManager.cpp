#include "GameManager.h"

GameManager::GameManager() :arr(vector <vector<int>>(31, vector<int>(28))),TileSize(30.0f)
{
	text = new Words();
	Vector2f POSITION(500, 0);
	window.create(VideoMode(1840, 930), "Simple Maze");
	this->loadBoardText();
	myBoard = new Board(arr, TileSize, POSITION);
	pacman = new Pacman(1, 1, TileSize, POSITION);
	manager = new GhostManager(&graph);
	manager->createGhost(myBoard, &graph, pacman);
	P = new Pellets(arr);
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
	pacman->setBoard(myBoard);
	pacman->resetPosition();
	pacman->setTexture("../images/pacman.png", 2, 4);
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



		P->isPowerPelletEaten(pacman);
	//	manager->checkGhost2Pacman(window, pacman, P);

		if (gtimer.getElapsedTime().asMilliseconds() >= 400)
		{

			manager->moveAll(pacman);
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
			P->incrementLife(pacman);//once score is greater than 10000 the life is incremented only once
			once = false;
		}

		checkCollision();


		myBoard->drawOnWindow(window);
		P->intersectPellets(pacman);
		P->drawPellets(window, myBoard->mShape);
		pacman->drawOnWindow(window);
		manager->draw(window);
		text->drawText(window, pacman);
		window.display();

	}
}

void GameManager::checkCollision()
{
	for (auto ghost : manager->getGhostList())
		if (ghost->getSprite().getGlobalBounds().intersects(pacman->getSprite().getGlobalBounds()))
		{
			if (ghost->getFreight())
			{
				ghost->die(window);
				P->incrementScore(500);
			}
			else
			{
				window.clear();
				window.display();
				window.clear();
				this->myBoard->drawOnWindow(window);
				P->drawPellets(window, myBoard->mShape);

				pacman->die(window);
				for (int i = 0; i < 4; i++)
					manager->getGhostList()[i]->resetPosition();

			}
			return;
		}

}
