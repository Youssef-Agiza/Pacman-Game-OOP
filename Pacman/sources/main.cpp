#include <SFML/Graphics.hpp>
#include <iostream>
#include <fstream>
#include <string>
#include <cstdlib>
//#include "Player.h"
#include "Pacman.h"
#include "Ghost.h"
#include "Board.h"
#include "GhostManager.h"

using namespace std;
using namespace sf;
using namespace PacmanCS;
/************************************************************************/


/************************************************************************/

int main()
{
	const int ROWS = 31, COLUMNS = 28,BLOCK=30, POSITION=25;
	RenderWindow window;
	window.create(VideoMode(1000, 1000), "Simple Maze");
	
	vector<vector<int>> arr(31,vector<int>(28));
	
ifstream inputFile;
	inputFile.open("../BoardTexts/BoardText2.txt");
	if (inputFile.is_open())
	{
		for (int i = 0; i < ROWS; i++)
			for (int j = 0; j < COLUMNS; j++)
				if (!inputFile.eof())
					inputFile >> arr[i][j];
	}
	inputFile.close();

	Board myBoard( arr,BLOCK, POSITION);
	

Pacman pacman(1, 1, BLOCK, POSITION);
pacman.setBoard(&myBoard);
pacman.setSpeed(0.5);
pacman.resetPosition();
pacman.setTexture("../images/pacman.png");

GhostManager manager;
manager.createGhost(&myBoard);

window.setFramerateLimit(10);
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
					pacman.setDirection(UP);
					break;
				case Keyboard::Down:
					pacman.setDirection(DOWN);
					break;
				case Keyboard::Right:
					pacman.setDirection(RIGHT);
					break;
				case Keyboard::Left:
					pacman.setDirection(LEFT);
					break;
				 default:break;
				}
			}
		}
	/*	switch (rand() % 4)
		{
		case 0:pinky.setDirection(UP); break;
		case 1:pinky.setDirection(RIGHT); break;
		case 2: pinky.setDirection(DOWN); break;
		case 3:pinky.setDirection(LEFT); break;
		default: break;
		}
		pinky.move();*/
		pacman.move();
		manager.checkGhost2Pacman(&pacman);
		window.clear();

		myBoard.drawOnWindow(window);
		pacman.drawOnWindow(window);
		manager.draw(window);
		//pinky.drawOnWindow(window);
		window.display();
	}

	return 0;

}

