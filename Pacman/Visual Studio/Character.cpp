#include "Character.h"

Character::Character(int intialRow, int intialColumn, int size):
	mCurrentRow(intialRow),mCurrentColumn(intialColumn),mSize(size)			
{
	updateShape();
}


//getters
bool Character::isAlive()const { return mAlive; }
int Character::getDirection()const { return mDirection; }
unsigned int Character::getRow()const { return mCurrentRow; }
unsigned int Character::getCol()const { return mCurrentColumn; }
unsigned int Character::getSize()const { return mSize; }


//setters
Character& Character::setAlive(bool status)
{
	mAlive = status;
	return *this;
}
Character& Character::setRow(unsigned int row)
{
	mCurrentRow = row;

	return *this;
}
Character& Character::setCol(unsigned int col)
{
	mCurrentColumn = col;
	return *this;

}
Character& Character::setDirection(Direction dir)
{
	mDirection = dir;
	return*this;
}
Character& Character::setTexture(std::string file)
{
	mTexture.loadFromFile(file);
	return *this;
}



void Character::resetPosition() {/*nothing*/ }
void Character::move(Direction dir)
{
	switch (dir)
	{
	case UP: mCurrentRow--; break;
	case DOWN: mCurrentRow++; break;
	case LEFT:mCurrentColumn--; break;
	case RIGHT: mCurrentColumn++; break;
	default:break;
	}

}
void Character::drawOnWindow(RenderWindow& window)
{
	window.draw(mShape);
}
void Character::updateShape()
{
	mShape.setPosition(mCurrentColumn, mCurrentRow); //note column and row are reverse
	mShape.setTexture(&mTexture);
}

