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
	//music.playChomp(1, "PacmanWakaWaka04.wav");
	//music.playBeginning(1);

	sf::Clock atime;
	atime.restart();
	sf::Sound chomp;
	sf::SoundBuffer chompBuffer; chompBuffer.loadFromFile("../soundtracks/PacmanWakaWaka04.wav");
	chomp.setBuffer(chompBuffer);
	Audio audio;
	sf::SoundSource::Status status;
while (window.isOpen())
	{
		while (window.pollEvent(e))
		{
			if (e.type == Event::Closed)
				window.close();
			
				
			
		}
		
			audio.playChomp();
		
	}
	return 0;
}