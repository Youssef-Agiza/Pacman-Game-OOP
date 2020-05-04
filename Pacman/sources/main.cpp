#include "Common.h"
#include "Pacman.h"
#include "Ghost.h"
#include "Board.h"
#include "GhostManager.h"
#include "../Visual Studio/Pellets.h"
using namespace std;
using namespace sf;
using namespace PacmanCS;
/************************************************************************/


/************************************************************************/

int main()
{
	const int ROWS = 31, COLUMNS = 28,BLOCK=10;
	sf::Vector2f POSITION(0, 100);
	RenderWindow window;
	window.create(VideoMode(1920, 1080), "Simple Maze");
	
	vector<vector<int>> arr(31,vector<int>(28));
	
ifstream inputFile;
	inputFile.open("../BoardTexts/BoardText3.txt");
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
pacman->resetPosition();
Pellets P(arr);

pacman->setTexture("../images/pacman.png",2,4);

GhostManager manager;
manager.createGhost(&myBoard);
for (int i=0;i<4;i++)
	pacman->addObserver(manager.getGhostList()[i]);

Ghost* pinky = manager.getGhostList()[0];


sf::Clock pactimer, gtimer;
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
				case Keyboard::W:
				{pinky->setDirection(UP);break; }
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
		
		if (gtimer.getElapsedTime().asMilliseconds() >= 500)
		{
			pinky->move();
			gtimer.restart();
		}
		if (pactimer.getElapsedTime().asMilliseconds() >= 250)
		{
			pactimer.restart();
			pacman->move();
		}
		
		manager.checkGhost2Pacman(pacman);
		window.clear();

		myBoard.drawOnWindow(window);
		P.intersectPellets(pacman->getRow(), pacman->getCol());
		P.drawPellets(window, myBoard.mShape);
		pacman->drawOnWindow(window);
		manager.draw(window);
		
		pinky->drawOnWindow(window);
		window.display();
	}

	return 0;

}

