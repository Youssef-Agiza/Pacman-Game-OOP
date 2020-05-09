#include "../headers/Common.h"
#include "../headers/Pacman.h"
#include "../headers/Ghost.h"
#include "../headers/Board.h"
#include "../headers/GhostManager.h"
#include "../headers/Pellets.h"
#include "../headers/Graph.h"
#include "../headers/Words.h"
#include "../Visual Studio/GameManager.h"
using namespace std;
using namespace sf;
using namespace PacmanCS;
/************************************************************************/

/************************************************************************/

int main()
//{
//	const int ROWS = 31, COLUMNS = 28, BLOCK = 30;
//	sf::Vector2f POSITION(500, 0);
//	RenderWindow window;
//	window.create(VideoMode(1840, 930), "Simple Maze");
//	
//	vector<vector<int>> arr(31, vector<int>(28));
//
//	Graph graph(320);	//   int graph[302][302];
//	ifstream ins("../boardTexts/BoardText3.txt");
//	if (ins.is_open())
//	{
//		for (int i = 0; i < ROWS; i++)
//			for (int j = 0; j < COLUMNS; j++)
//				ins >> arr[i][j];
//	}
//	
//	bool once = true;
//	for (int i = 0; i < ROWS; i++)
//		for (int j = 0; j < COLUMNS; j++)
//		{
//			if (arr[i][j] > -1)
//			{
//				if (j + 1 != COLUMNS && arr[i][j + 1] > -1)
//					graph.addEdge( arr[i][j], arr[i][j + 1]);
//				if (i + 1 != ROWS && arr[i + 1][j] > -1)
//graph.addEdge(arr[i][j], arr[i + 1][j]);
//			}
//		}
//		graph.addEdge(149, 130);//connects portals
//
//
//
//
//		ifstream inputFile;
//		inputFile.open("../BoardTexts/BoardText3.txt");
//		if (inputFile.is_open())
//		{
//			for (int i = 0; i < ROWS; i++)
//				for (int j = 0; j < COLUMNS; j++)
//					if (!inputFile.eof())
//						inputFile >> arr[i][j];
//		}
//		inputFile.close();
//
//		Board myBoard(arr, BLOCK, POSITION);
//
//
//		Pacman* pacman = new Pacman(1, 1, BLOCK, POSITION);
//		pacman->setBoard(&myBoard);
//		pacman->resetPosition();
//		Pellets P(arr);
//
//		pacman->setTexture("../images/pacman.png", 2, 4);
//
//		GhostManager manager(&graph);
//		manager.createGhost(&myBoard, &graph, pacman);
//
//		Words* text = new Words;
//		Ghost* blinky = manager.getGhostList()[0];
//		Ghost* Pinky = manager.getGhostList()[2];
//		sf::Clock pactimer, gtimer;
//		pactimer.restart();
//		gtimer.restart();
//		window.setFramerateLimit(20);
//		Event e;
//		while (window.isOpen())
//		{
//			while (window.pollEvent(e))
//			{
//				if (e.type == Event::Closed)
//					window.close();
//				else if (e.type == Event::KeyPressed)
//				{
//					switch (e.key.code)
//					{
//					case Keyboard::Up:
//						pacman->setDirection(UP);
//						break;
//					case Keyboard::Down:
//						pacman->setDirection(DOWN);
//						break;
//					case Keyboard::Right:
//						pacman->setDirection(RIGHT);
//						break;
//					case Keyboard::Left:
//						pacman->setDirection(LEFT);
//						break;
//					case Keyboard::W:
//					{blinky->setDirection(UP); break; }
//					case Keyboard::D:
//						blinky->setDirection(RIGHT); break;
//					case Keyboard::A:
//						blinky->setDirection(LEFT); break;
//					case Keyboard::S:
//						blinky->setDirection(DOWN); break;
//					case Keyboard::F:
//						pacman->setPowerUp(!pacman->getPowerUp()); break;
//
//
//					default:break;
//					}
//				}
//			}
//
//
//
//			P.isPowerPelletEaten(pacman);
//			manager.checkGhost2Pacman(window, pacman, &P);
//
//			if (gtimer.getElapsedTime().asMilliseconds() >= 400)
//			{
//
//				manager.moveAll(pacman);
//				gtimer.restart();
//			}
//			if (pactimer.getElapsedTime().asMilliseconds() >= 250)
//			{
//				pactimer.restart();
//				pacman->move();
//			}
//
//
//			window.clear();
//			
//			if ((pacman->getScore() >= 10000) && (once))
//			{
//				P.incrementLife(pacman);//once score is greater than 10000 the life is incremented only once
//				once = false;
//			}
//		
//
//
//		if (!pacman->isAlive())
//		{
//			window.clear();
//
//			myBoard.drawOnWindow(window);
//			P.intersectPellets(pacman);
//			P.drawPellets(window, myBoard.mShape);
//			pacman->drawOnWindow(window);
//			blinky->drawOnWindow(window);
//			pacman->setTexture("../images/Death.png", 12, 1);
//
//			sf::Clock timer;
//			timer.restart();
//			int i = 0;
//			pacman->decrementLife();
//			while (i < 12) 
//			{
//				if (timer.getElapsedTime().asMilliseconds() > 150)
//				{
//					pacman->animateDie();
//					timer.restart();
//					i++;
//					pacman->drawOnWindow(window);
//					window.display();
//					
//				}
//			}
//			pacman->resetPosition();
//			//we need to check here if Pacman died> 3adad el lives then end game.
//			for (int i = 0; i < 4; i++)
//				manager.getGhostList()[i]->resetPosition();
//		}
//
//
//		myBoard.drawOnWindow(window);
//		P.intersectPellets(pacman);
//		P.drawPellets(window, myBoard.mShape);
//		pacman->drawOnWindow(window);
//		manager.draw(window);
//		text->drawText(window, pacman);
//		
//		blinky->drawOnWindow(window);
//		
//
//		window.display();
//		
//	}
//
//	return 0;
//
//}
{
GameManager Game;
Game.loadBoardText();
Game.createEdges();
Game.loadPacman();
Game.Play();
return 0;
}
