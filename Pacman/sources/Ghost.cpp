#include "Ghost.h"
#include "../design patterns/ISubject.h"
Ghost::Ghost(int intialRow, int intialColumn, float size, sf::Vector2f position) :
		Character( intialRow,  intialColumn,  size,  position),mFreight(false),
	mIntialRow(intialRow),mIntialCol(intialColumn)
{}

Ghost& Ghost::setIntialRow(unsigned int row)
{
	mIntialRow = row;
	return*this;
}
Ghost& Ghost::setIntialCol(unsigned int col)
{
	mIntialCol = col;
	return *this;
}
unsigned int Ghost::getIntialRow()const { return mIntialRow; }
unsigned int Ghost::getIntialCol()const { return mIntialCol; }

Ghost& Ghost::setResource(std::string r) { mResource = r; return *this; }
const std::string& Ghost::getResource() const { return mResource; }


Ghost& Ghost::setFreight(bool v)
{
	mFreight = v;
	return *this;
}
bool Ghost::getFreight()const { return mFreight; }

void Ghost::resetPosition()
{
	mCurrentColumn = mIntialCol;
	mCurrentRow = mIntialRow;
	updateShape();
	
}
void Ghost::die()
{
	resetPosition();
	if (mFreight)
	{
		setTexture(this->getResource(), 8, 1);
		setFreight(false);
	}
	mDirection = STOP;
}

void Ghost::update(bool powerUp)
{
	if (powerUp && !mFreight)
	{
		this->setFreight(1);
		this->setTexture("../images/freight.png", 2, 1);
	}
	
	if (!powerUp && mFreight)
	{
		this->setFreight(0);
		this->setTexture(this->getResource(), 8, 1);
	}
	//do other freight mode logic here
	/*
	. change ghost color
	. change ghost behavior
	*/
}

void Ghost::move()
{

	if (checkDestination(mDirection) == 0)
		return;

	switch (mDirection)
	{
	case UP:
	{
		mCurrentRow--;
		mSprite.move(0, -mSize);
		break;
	}
	case DOWN:
	{
		mCurrentRow++;
		mSprite.move(0, mSize);
		break; }
	case LEFT:
	{
		if (checkDestination(mDirection) == 2) //portal
		{
			mCurrentColumn = mBoard->getBoard()[mCurrentRow].size() - 1;
			updateShape();

		}
		else {
			mCurrentColumn--;
			mSprite.move(-mSize, 0);
		}
		break;
	}
	case RIGHT: {
		if (checkDestination(mDirection) == 2)//portal
		{
			mCurrentColumn = 1;
			updateShape();
		}
		else {
			mCurrentColumn++;
			mSprite.move(mSize, 0);
		}
		break;
	}
	default:break;
	}
	animateMove();
}


void Ghost::animateMove()
{
	if (mTexture == nullptr)
		return;
	sf::IntRect rect = mSprite.getTextureRect();
	int i = 0,j=0;
	if (mFreight)
		rect.left = (rect.left == 0) ? rect.width : 0;
	else {
		switch (mDirection)
		{
		case UP:i = 4; break;
		case RIGHT:i = 0; break;
		case DOWN:i = 6; break;
		case LEFT:i = 2; break;
		default:break;
		}
		j = ((rect.left % (2 * rect.width)) == 0) ? 1 : 0;
		rect.left = (i+j) *rect.width ;
	}
	
	mSprite.setTextureRect(rect);
}

void Ghost::animateDie()
{
}
