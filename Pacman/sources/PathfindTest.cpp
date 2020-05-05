#include "Common.h"
#include "Pacman.h"
#include "Ghost.h"
#include "Board.h"
#include "GhostManager.h"
#include "Pellets.h"
#include "Graph.h"
using namespace std;
using namespace sf;
using namespace PacmanCS;
/************************************************************************/

 Direction Path2Movement(std::list<int>*,Ghost*,Board*);
/************************************************************************/

int main()
{
	const int ROWS = 31, COLUMNS = 28, BLOCK = 30;
	sf::Vector2f POSITION(0, 100);
	RenderWindow window;
	window.create(VideoMode(1000, 1000), "Simple Maze");

	vector<vector<int>> arr(31, vector<int>(28));

	int a[ROWS][COLUMNS];
	Graph graph(302);	//   int graph[302][302];
	ifstream ins("../boardTexts/BoardText3.txt");
	if (ins.is_open())
	{
		for (int i = 0; i < ROWS; i++)
			for (int j = 0; j < COLUMNS; j++)
				ins >> a[i][j];
	}
	

	for (int i = 0; i < ROWS; i++)
		for (int j = 0; j < COLUMNS; j++)
		{
			if (a[i][j] > -1)
			{
				if (j + 1 != COLUMNS && a[i][j + 1] > -1)
					graph.addEdge( a[i][j], a[i][j + 1]);
				if (i + 1 != ROWS && a[i + 1][j] > -1)
					graph.addEdge( a[i][j], a[i + 1][j]);
			}
		}




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

	Board myBoard(arr, BLOCK, POSITION);


	Pacman* pacman = new Pacman(1, 1, BLOCK, POSITION);
	pacman->setBoard(&myBoard);
	pacman->resetPosition();
	Pellets P(arr);

	pacman->setTexture("../images/pacman.png", 2, 4);

	GhostManager manager;
	manager.createGhost(&myBoard);
	for (int i = 0; i < 4; i++)
		pacman->addObserver(manager.getGhostList()[i]);

	Ghost* blinky = manager.getGhostList()[0];
	
	sf::Clock pactimer, gtimer;
	pactimer.restart();
	gtimer.restart();
	window.setFramerateLimit(20);
	Event e;
	Direction d;
	std::cout << "\n";


	blinky->setCol(1).setRow(1);
	blinky->updateShape();


	int src = myBoard.getBoard()[blinky->getRow()][blinky->getCol()];
	int dest = myBoard.getBoard()[pacman->getRow()][pacman->getCol()];
	std::list<int>* path=graph.dijkstra( 0,dest);
	





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
				{blinky->setDirection(UP); break; }
				case Keyboard::D:
					blinky->setDirection(RIGHT); break;
				case Keyboard::A:
					blinky->setDirection(LEFT); break;
				case Keyboard::S:
					blinky->setDirection(DOWN); break;
				case Keyboard::F:
					pacman->setPowerUp(!pacman->getPowerUp()); break;
				default:break;
				}
			}
		}
	
		if (gtimer.getElapsedTime().asMilliseconds() >= 500)
		{
			d = Path2Movement(path, blinky, &myBoard);
			blinky->setDirection(d);
			blinky->move();
			gtimer.restart();
		}
		if (pactimer.getElapsedTime().asMilliseconds() >= 250)
		{
			pactimer.restart();
			pacman->move();
		}

		manager.checkGhost2Pacman(pacman,P);
		window.clear();

		myBoard.drawOnWindow(window);
		P.intersectPellets(pacman->getRow(), pacman->getCol());
		P.drawPellets(window, myBoard.mShape);
		pacman->drawOnWindow(window);
		manager.draw(window);

		blinky->drawOnWindow(window);
		window.display();
	}

	return 0;

}


Direction Path2Movement(std::list<int>* path, Ghost* ghost, Board* board)
{
	if (path->empty())
		return STOP;
	int vertex = path->front();
	std::cout << "Vertex: " << vertex << std::endl;
	path->pop_front();
	if (path == nullptr||vertex==-1)
		return STOP;
	unsigned int row = ghost->getRow(), col = ghost->getCol();
	if (row+1<=board->getBoard().size()&& board->getBoard()[row+ 1][col] == vertex)
		return DOWN;
	if (col + 1 <= board->getBoard()[0].size() && board->getBoard()[row ][col+1] == vertex)
		return RIGHT;
	if (col - 1 >= 0 && board->getBoard()[row][col - 1] == vertex)
		return LEFT;
	if (row - 1 >= 0 && board->getBoard()[row - 1][col] == vertex)
		return UP;
	return STOP;
	
}