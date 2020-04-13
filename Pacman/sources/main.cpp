#include <SFML/Graphics.hpp>
#include <iostream>
#include <fstream>
#include <string>
#include "Player.h"
#include "Board.h"

using namespace std;
using namespace sf;
using namespace PacmanCS;
/************************************************************************/


/************************************************************************/

int main()
{
	const int ROWS = 11, COLUMNS = 11,BLOCK=50;
	RenderWindow window;
	window.create(VideoMode(1000, 1000), "Simple Maze");

	int** arr;
	arr = new int* [ROWS];
	
	for (int i=0; i< COLUMNS;i++)
	{
		arr[i] = new int[COLUMNS];
	}

ifstream inputFile;
	inputFile.open("../BoardTexts/BoardText.txt");
	if (inputFile.is_open())
	{
		for (int i = 0; i < ROWS; i++)
			for (int j = 0; j < COLUMNS; j++)
				if (!inputFile.eof())
					inputFile >> arr[i][j];
	}
	inputFile.close();
	Player player(1, 1,50.0,1.0 ,"../images/Pacman.png");

	Board myBoard(ROWS,COLUMNS,50.0, 1.0,"../images/Block.png", "../images/grass_tiled.png");
	myBoard.setTextures(arr);

	
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
					player.move('U', arr);
					break;
				case Keyboard::Down:
					player.move('D', arr);
					break;
				case Keyboard::Right:
					player.move('R', arr);
					break;
				case Keyboard::Left:
					player.move('L', arr);
					break;
				}
			}
		}
		window.clear();

		myBoard.drawOnWindow(window);
		player.drawOnWindow(window);
		window.display();
	}
	return 0;

}
