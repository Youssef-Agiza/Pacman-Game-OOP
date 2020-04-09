#pragma once
#include <SFML/Graphics.hpp>
#include <iostream>
#include <string>
class Player
{
private:
	int currentRow;
	int currentColumn;
	const float mSize;
	sf::CircleShape shape;
	sf::Texture texture;
public:
	Player(int initialRow, int initialColumn, float size,std::string imageName);
	void move(char direction, int** arr);
	void drawOnWindow(sf::RenderWindow& window);
	int getRow();
	int getColumn();
};
