#include "Player.h"
const int DEFAULT_POSTION = 50;
Player::Player(int initialRow, int initialColumn, float size=50,float pos=DEFAULT_POSTION,std::string imageName=""):
	mSize(size),mPosition(pos)
{
	currentRow = initialRow;
	currentColumn = initialColumn;
	shape.setRadius(mSize/2);
	shape.setPosition(mPosition + mSize * initialColumn, mPosition + mSize * initialRow);
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
			shape.move(0, -mSize);
		}
	}
	else if (direction == 'D' || direction == 'd')
	{
		if (arr[currentRow + 1][currentColumn] != -1)
		{
			currentRow++;
			shape.move(0, mSize);
		}
	}
	else if (direction == 'R' || direction == 'r')
	{
		if (arr[currentRow][currentColumn + 1] != -1)
		{
			currentColumn++;
			shape.move(mSize, 0);
		}
	}
	else if (direction == 'L' || direction == 'l')
	{
		if (arr[currentRow][currentColumn - 1] != -1)
		{
			currentColumn--;
			shape.move(-mSize, 0);
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


