#include <SFML/Graphics.hpp>
#include <iostream>
#include <fstream>
#include <string>

//#include "Player.h"
#include "Pacman.h"
#include "Board.h"

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

	int** arr;
	arr = new int* [ROWS];
	
	for (int i=0; i< ROWS;i++)
	{
		arr[i] = new int[COLUMNS];
	}

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

	Board myBoard(ROWS, COLUMNS, BLOCK, POSITION);
	myBoard.setWallTexture("../images/wall.png");
	myBoard.setTextures(arr);
Pacman pacman(1, 1, BLOCK, POSITION);
pacman.resetPosition();
pacman.setTexture("../images/pacman.png");

window.setFramerateLimit(60);
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
		pacman.move();

		window.clear();

		myBoard.drawOnWindow(window);
		pacman.drawOnWindow(window);
		window.display();
	}

	return 0;

}
