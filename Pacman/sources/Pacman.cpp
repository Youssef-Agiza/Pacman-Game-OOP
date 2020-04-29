#include "Pacman.h"
//random numbers
#define INTIAL_ROW  23
#define INTIAL_COL  14
Pacman::Pacman(int intialRow, int intialColumn, float size, float posWindow) :
	Character( intialRow,  intialColumn,  size,  posWindow),mLives(3),mPowerUp(false)
{
	
}

Pacman& Pacman::setPowerUp(bool p)
{
	mPowerUp = p;
	return *this;
}
bool Pacman::getPowerUp()const { return mPowerUp; }

Pacman& Pacman::setLives(unsigned int l) 
{
	mLives = l;
	return *this;
}
unsigned int Pacman::getLives()const { return mLives; }

void Pacman::resetPosition()
{
	mDirection = STOP;
	mAlive = true;
	mCurrentColumn = INTIAL_COL;
	mCurrentRow = INTIAL_ROW;
	mShape.setPosition(mPositionOnWindow + mCurrentColumn * mSize, mPositionOnWindow + mCurrentRow * mSize);
}

void Pacman::die()
{
	mAlive = false;
	if (--mLives > 0)
		resetPosition();
	else
		mShape.setFillColor(sf::Color::Black);
	
}

void Pacman::addObserver(IObserver* observer)
{
	if (observer == nullptr)
		return;
	mObserverList.push_back(observer);

}
void Pacman::removeObserver(IObserver* observer)
{
	if (observer == nullptr)
		return;
	mObserverList.remove(observer);
}
void Pacman::notify()
{
	std::list<IObserver*>::iterator itr= mObserverList.begin();
	for (itr; itr != mObserverList.end(); itr++)
		(*itr)->update();
}