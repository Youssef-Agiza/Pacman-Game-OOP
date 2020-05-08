#pragma once
#include "../headers/Common.h"
#include "../headers/Pacman.h"
#include "../headers/Ghost.h"
#include "../headers/Board.h"
#include "../headers/GhostManager.h"
#include "../headers/Pellets.h"
#include "../headers/Graph.h"
#include "../headers/Words.h"
using namespace std;
using namespace sf;
using namespace PacmanCS;
class GameManager

{
private:
	const int ROWS = 31, COLUMNS = 28, BLOCK = 30;
	RenderWindow window;
	bool once = true;
	vector<vector<int>> arr;
	Graph graph;
	Pacman* pacman;
	Board* myBoard;
	Pellets* P;
	GhostManager* manager;
	Words* text = new Words;
	
public:
	GameManager():arr(vector <vector<int>>(31, vector<int> (28)))
	{
		Vector2f POSITION(500, 0);
		window.create(VideoMode(1840, 930), "Simple Maze");
		this->loadBoardText();
		myBoard= new Board(arr, BLOCK, POSITION); 
		pacman = new Pacman(1, 1, BLOCK, POSITION);
		manager=new GhostManager(&graph);
		manager->createGhost(myBoard, &graph, pacman);
		P = new Pellets(arr);
	}
	void loadBoardText()
	{
		ifstream ins("../boardTexts/BoardText3.txt");
		if (ins.is_open())
		{
			for (int i = 0; i < arr.size(); i++)
				for (int j = 0; j < arr[i].size(); j++)
					ins >> arr[i][j];
		}

	}
	void createEdges()
	{
		for (int i = 0; i < arr.size(); i++)
			for (int j = 0; j < arr[i].size(); j++)
			{
				if (arr[i][j] > -1)
				{
					if (j + 1 != COLUMNS && arr[i][j + 1] > -1)
						graph.addEdge(arr[i][j], arr[i][j + 1]);
					if (i + 1 != ROWS && arr[i + 1][j] > -1)
						graph.addEdge(arr[i][j], arr[i + 1][j]);
				}
			}
		graph.addEdge(149, 130);//connects portals
	}
	void loadInputFile()
	{
		ifstream inputFile;
		inputFile.open("../BoardTexts/BoardText3.txt");
		if (inputFile.is_open())
		{
			for (int i = 0; i < arr.size(); i++)
				for (int j = 0; j < arr[i].size(); j++)
					if (!inputFile.eof())
						inputFile >> arr[i][j];
		}
		inputFile.close();
	}
	void loadPacman()
	{
		pacman->setBoard(myBoard);
		pacman->resetPosition();
		pacman->setTexture("../images/pacman.png", 2, 4);
	}
	void Play()
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
			manager->checkGhost2Pacman(window, pacman, P);

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



			if (!pacman->isAlive())
			{
				window.clear();

				myBoard->drawOnWindow(window);
				P->intersectPellets(pacman);
				P->drawPellets(window, myBoard->mShape);
				pacman->drawOnWindow(window);
				pacman->setTexture("../images/Death.png", 12, 1);

				sf::Clock timer;
				timer.restart();
				int i = 0;
				pacman->decrementLife();
				while (i < 12)
				{
					if (timer.getElapsedTime().asMilliseconds() > 150)
					{
						pacman->animateDie();
						timer.restart();
						i++;
						pacman->drawOnWindow(window);
						window.display();

					}
				}
				pacman->resetPosition();
				//we need to check here if Pacman died> 3adad el lives then end game.
				for (int i = 0; i < 4; i++)
					manager->getGhostList()[i]->resetPosition();
			}


			myBoard->drawOnWindow(window);
			P->intersectPellets(pacman);
			P->drawPellets(window, myBoard->mShape);
			pacman->drawOnWindow(window);
			manager->draw(window);
			text->drawText(window, pacman);
			window.display();

		}
	}


};

