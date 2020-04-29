#include "../Visual Studio/Common.h"
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
	

Pacman* pacman=new Pacman(1, 1, BLOCK, POSITION);
pacman->setBoard(&myBoard);
pacman->setSpeed(10);
pacman->resetPosition();
pacman->setTexture("../images/pacman.png");

GhostManager manager;
manager.createGhost(&myBoard);
Ghost* pinky = manager.getGhostList()[2];
int x = 0;
//thread t[2];
pacman->addObserver(pinky);
pacman->setPowerUp(1);
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
				case Keyboard::W:
					pinky->setDirection(UP); break;
				case Keyboard::D:
					pinky->setDirection(RIGHT); break;
				case Keyboard::A:
					pinky->setDirection(LEFT); break; 
				case Keyboard::S:
						pinky->setDirection(DOWN); break;
				case Keyboard::F:
					pacman->setPowerUp(!pacman->getPowerUp()); break;
				 default:break;
				}
			}
		}
		

		switch (x)
		{
		case 0: {pinky->move(); pacman->move() ; x = 1; break; }
		case 1: x = 2; break;
		case 2:x = 0; break;
		default:break;
		}
		
		manager.checkGhost2Pacman(pacman);
		window.clear();

		myBoard.drawOnWindow(window);
		pacman->drawOnWindow(window);
		manager.draw(window);
		//pinky.drawOnWindow(window);
		window.display();
	}

	return 0;

}

