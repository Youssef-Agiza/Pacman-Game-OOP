#include "Common.h"
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
	const int ROWS = 31, COLUMNS = 28, BLOCK = 30, POSITION = 25;
	RenderWindow window;
	window.create(VideoMode(1000, 1000), "Simple Maze");

	vector<vector<int>> arr(31, vector<int>(28));

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



	
	window.setFramerateLimit(20);
	Sprite sprite;
	Texture *t= new Texture;
	t->loadFromFile("../images/pinky.png");
	sprite.setTexture(*t);
	sf::Vector2u v = t->getSize();
	sprite.setScale((8*30.0f)/v.x, 30.0f/v.y);
	sprite.setTextureRect(sf::IntRect(0, 0, 128/8.0f, 18));
	sprite.setPosition(500, 500);
	sf::Vector2f v2;
//	delete t;
	int i=0;
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
				{
					t->loadFromFile("../images/freight.png");
					sprite.setTexture(*t);
					v = t->getSize();
					sprite.setScale(60.f / v.x, 30.f / v.y);
					sprite.setTextureRect(sf::IntRect(0, 0, 16, 16));
					v2=sprite.getScale();
					break;
				}
				}
			}
		}
		

		window.clear();
		window.draw(sprite);
	
		window.display();
	}

	return 0;

}

