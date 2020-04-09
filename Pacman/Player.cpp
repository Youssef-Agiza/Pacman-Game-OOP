#include "Player.h"
Player::Player(int initialRow, int initialColumn, std::string imageName)
{
	currentRow = initialRow;
	currentColumn = initialColumn;
	shape.setRadius(35);
	shape.setPosition(50 + 70.0f * initialColumn, 50 + 70.0f * initialRow);
	texture.loadFromFile(imageName);
	shape.setTexture(&texture);
}
void Player::move(char direction, int** arr)
{
	if (direction == 'U' || direction == 'u')
	{
		if (arr[currentRow - 1][currentColumn] != -1)
		{
			currentRow--;
			shape.move(0, -70);
		}
	}
	else if (direction == 'D' || direction == 'd')
	{
		if (arr[currentRow + 1][currentColumn] != -1)
		{
			currentRow++;
			shape.move(0, 70);
		}
	}
	else if (direction == 'R' || direction == 'r')
	{
		if (arr[currentRow][currentColumn + 1] != -1)
		{
			currentColumn++;
			shape.move(70, 0);
		}
	}
	else if (direction == 'L' || direction == 'l')
	{
		if (arr[currentRow][currentColumn - 1] != -1)
		{
			currentColumn--;
			shape.move(-70, 0);
		}
	}
}
void Player::drawOnWindow(sf::RenderWindow& window)
{
	window.draw(shape);
}
int Player::getRow()
{
	return currentRow;
}
int Player::getColumn()
{
	return currentColumn;
}


