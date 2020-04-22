#include "Character.h"

Character::Character(int intialRow, int intialColumn, float size,float posWindow):
	mCurrentRow(intialRow),mCurrentColumn(intialColumn),mSize(size),mPositionOnWindow(posWindow), mAlive(true),mSpeed(1),mDirection(UP)
{
	mShape.setSize(Vector2f(mSize, mSize));
	mShape.setPosition(mPositionOnWindow + mCurrentColumn * mSize, mPositionOnWindow+ mCurrentRow * mSize);
}


//getters
bool Character::isAlive()const { return mAlive; }
int Character::getDirection()const { return mDirection; }
unsigned int Character::getRow()const { return mCurrentRow; }
unsigned int Character::getCol()const { return mCurrentColumn; }
float Character::getSize()const { return mSize; }
float Character::getPosition()const { return mPositionOnWindow; }
const Texture& Character::getTexture()const { return mTexture; }
float Character::getSpeed()const { return mSpeed; }


//setters
Character& Character::setSize(float s)
{
	mSize = s;
	return *this;
}
Character& Character::setPosition(float position)
{
	mPositionOnWindow = position;
	return *this;
}
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
	if (mTexture.loadFromFile(file))
		mShape.setTexture(&(this->mTexture));
	else
		mShape.setFillColor(Color::Yellow);
	return *this;
}
Character& Character::setSpeed(float s)
{
	mSpeed = s;
	return *this;
}


void Character::resetPosition() {/*nothing*/ }
bool Character::isValidDirection(Direction d)const {/*not*/ }
void Character::move()
{
	switch (mDirection)
	{
	case UP: 
	{	mCurrentRow--;
		mShape.move(0, -mSize * mSpeed);
		break; }
	case DOWN:
	{ mCurrentRow++; 
	mShape.move(0, mSize * mSpeed);
		break; }
	case LEFT: 
	{
		mCurrentColumn--;
		mShape.move(-mSize * mSpeed, 0);
		break;
	}
	case RIGHT: {mCurrentColumn++; mShape.move(mSize * mSpeed, 0);
		break; }
	default:break;
	}

}
void Character::drawOnWindow(RenderWindow& window)
{
	updateShape();
	window.draw(mShape);
}
void Character::updateShape()
{
	mShape.setPosition(mPositionOnWindow + mCurrentColumn * mSize, mPositionOnWindow + mCurrentRow * mSize); //updates position of shape
}

