#include "Ghost.h"
#include "../design patterns/ISubject.h"
Ghost::Ghost(int intialRow, int intialColumn, float size, float position) :
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
	setFreight(false);
	mDirection = STOP;
}

void Ghost::update(bool powerUp)
{
	if (powerUp && !mFreight)
		this->setFreight(1);
	
	if (!powerUp && mFreight)
		this->setFreight(0);

	//do other freight mode logic here
	/*
	. change ghost color
	. change ghost behavior
	*/
}