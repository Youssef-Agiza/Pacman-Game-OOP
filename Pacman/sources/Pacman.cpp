#include "Pacman.h"
//random numbers
#define INTIAL_ROW  23
#define INTIAL_COL  14
Pacman::Pacman(int intialRow, int intialColumn, float size, sf::Vector2f posWindow) :
	Character( intialRow,  intialColumn,  size,  posWindow),mLives(3),mPowerUp(false)
{
	
}

Pacman& Pacman::setPowerUp(bool p)
{
	mPowerUp = p;
	notify(); 
	return *this;
}
bool Pacman::getPowerUp()const { return mPowerUp; }

Pacman& Pacman::setLives(unsigned int l) 
{
	mLives = l;
	return *this;
}
unsigned int Pacman::getLives()const { return mLives; }

void Pacman::addLive(unsigned int l)
{
	mLives += l;
}

void Pacman::resetPosition()
{
	mDirection = STOP;
	mAlive = true;
	mCurrentColumn = INTIAL_COL;
	mCurrentRow = INTIAL_ROW;
	mSprite.setPosition(mPositionOnWindow.x + mCurrentColumn * mSize, mPositionOnWindow.y + mCurrentRow * mSize);
}

void Pacman::die()
{
	mAlive = false;
	if (--mLives <=0)	
		mSprite.setColor(sf::Color::Black);
	resetPosition();
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
		(*itr)->update(this->mPowerUp);
}



void Pacman::animateMove()
{
	{
		if (mTexture == nullptr)
			return;
		float left = (float) mSprite.getTextureRect().left, top = (float)mSprite.getTextureRect().top;
		switch (mDirection)
		{
		case UP: top = 2; break;
		case RIGHT:top = 0; break;
		case LEFT:top = 1; break;
		case DOWN:top = 3; break;
		default:break;

		}

		int i = (int) left / 16;
		++i %= 2;
		mSprite.setTextureRect(sf::IntRect(16 * i, (int) top * 15, 16, 15));

	}
}
void Pacman::move()
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