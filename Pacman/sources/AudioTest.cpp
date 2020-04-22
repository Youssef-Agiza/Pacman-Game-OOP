#include <SFML/Audio.hpp>
#include <SFML/Graphics.hpp>
#include <string>
#include <iostream>
#include "Audio.h"
using namespace PacmanCS;
using namespace sf;
using namespace std;
int main()
{
	RenderWindow window;
	window.create(VideoMode(1000, 1000), "Simple Maze");
	Audio music;
	string filename = "";
	//music.playBeginning(1);
	int x;
	Event e;
	music.playChomp(1, "PacmanWakaWaka04.wav");
	music.playBeginning(1);
	while (window.isOpen())
	{
		while (window.pollEvent(e))
		{
			if (e.type == Event::Closed)
				window.close();
			
				
			
		}
		
	}
	return 0;
}