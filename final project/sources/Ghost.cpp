#include "../headers/Ghost.h"
#include "../headers/Pacman.h"
#include "../design patterns/ISubject.h"
Ghost::Ghost(int intialRow, int intialColumn, float size, Board* board) :
		Character( intialRow,  intialColumn,  size,  board)
	,mFreight(false),mGraph(nullptr), ai(nullptr),
	mIntialRow(intialRow),mIntialCol(intialColumn)
	,homeTimer(0),inHome(true)
{
}

Ghost::~Ghost()
{
	mGraph = nullptr;
}

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



Ghost& Ghost::setGraph(Graph* graph)
{
	mGraph = graph;
	return *this;
}


const std::string& Ghost::getResource() const { return mResource; }


Ghost& Ghost::setFreight(bool v)
{
	mFreight = v;
	return *this;
}
bool Ghost::getFreight()const { return mFreight; }


void Ghost::resetPosition()
{
	inHome = true;
	mCurrentColumn = mIntialCol;
	mCurrentRow = mIntialRow;
	updateShape();
	
}
void Ghost::die(sf::RenderWindow& w)
{
		speedTimer /=2 ;
		resetPosition();
		setTexture(this->getResource(), 8, 1);
		setFreight(false);
		mDirection = STOP;
	
}

void Ghost::update(bool powerUp)
{
	if (powerUp && !mFreight)
	{
		this->setFreight(1);
		this->setTexture("../images/freight.png", 2, 1);
		speedTimer *= 2;
	}
	
	if (!powerUp && mFreight)
	{
		this->setFreight(0);
		this->setTexture(this->getResource(), 8, 1);
		speedTimer /= 2;
	}

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
			mCurrentColumn = 0;
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

Ghost& Ghost::setDirection(Direction d)
{
	if (checkDestination(d)/*&&!reverse(d)*/)
		mDirection = d;
	return *this;
}

void Ghost::clyde(Pacman* pacman)
{
	//random movement
	
		if (!mFreight)
		{
			Direction d;
			int randTarget;
			do {
				randTarget = (pacman->getVertex() + rand()) % 302;
				d = Path2Movement(mGraph->dijkstra(getVertex(), randTarget));
			} while (reverse(d));

			setDirection(d);
		}
		else //freight
		{
			setDirection(Path2Movement(mGraph->dijkstra(getVertex(), mBoard->getBoard()[mIntialRow][mIntialCol])));
		}
	
}

void Ghost::blinky(Pacman* pacman)
{
	//chase pacman
	if (!mFreight)
		setDirection(Path2Movement(mGraph->dijkstra(getVertex(), pacman->getVertex())));
	else //freight
		setDirection( Path2Movement(mGraph->dijkstra(getVertex(), mBoard->getBoard()[mIntialRow][mIntialCol])) );
	
}

void Ghost::pinky(Pacman* pacman)
{
	//moves ahead of pacman
	if (!mFreight)
	{
		unsigned int pacRow = pacman->getRow();
		unsigned int pacCol = pacman->getCol();
		Direction pacDir = pacman->getDirection();
		if (pacDir == STOP)
			pacDir = RIGHT;
		if (pacDir == UP)
		{
			for (int i = 4; i >= 0; i--)
				if (mBoard->checkVertex(pacRow - i, pacCol) == 1)
				{
				setDirection( Path2Movement(mGraph->dijkstra(getVertex(), mBoard->getBoard()[pacRow - i][pacCol])) );
					break;
				}
		}
		else if (pacDir == DOWN)
		{
			for (int i = 4; i >= 0; i--)
				if (mBoard->checkVertex(pacRow + i, pacCol) == 1)
				{
					setDirection( Path2Movement(mGraph->dijkstra(getVertex(), mBoard->getBoard()[pacRow + i][pacCol])) );
					break;
				}
		}
		else if (pacDir == RIGHT)
		{
			for (int i = 4; i >= 0; i--)
				if (mBoard->checkVertex(pacRow , pacCol+i) == 1)
				{
					setDirection( Path2Movement(mGraph->dijkstra(getVertex(), mBoard->getBoard()[pacRow ][pacCol+i])) );
					break;
				}
		}
		else if (pacDir == LEFT)
		{

			for (int i = 4; i >= 0; i--)
				if (mBoard->checkVertex(pacRow , pacCol-i) == 1)
				{
					setDirection( Path2Movement(mGraph->dijkstra(getVertex(), mBoard->getBoard()[pacRow ][pacCol-i])) );

				}
		}
	}
	else//mFrieght
	{
		setDirection(Path2Movement(mGraph->dijkstra(getVertex(), mBoard->getBoard()[mIntialRow][mIntialCol])));
	}
}

void Ghost::inky(Pacman* pacman)
{
	
	//chase pacman
	if (!mFreight)
	{
		setDirection(Path2Movement(mGraph->dijkstra(getVertex(), pacman->getVertex())));
	}
	else //freight
	{
		setDirection(Path2Movement(mGraph->dijkstra(getVertex(), mBoard->getBoard()[mIntialRow][mIntialCol])));
	}
}

void Ghost::callAI(Pacman* pacman)
{

	if (currentTime.getElapsedTime().asMilliseconds() >= speedTimer&&!inHome)
	{
		currentTime.restart();
		(this->*ai)(pacman);
		move();
	}
}
Direction Ghost::Path2Movement(std::list<int>* path)
{
	if (path == nullptr)
		return STOP;
	if (path->empty())
		return mDirection;
	int vertex = path->front();
	path->pop_front();
	

	unsigned int row = this->getRow(), col = this->getCol();
	if (row + 1 < mBoard->getBoard().size() && col < mBoard->getBoard()[row].size() && mBoard->getBoard()[row + 1][col] == vertex)
		return DOWN;
		if (col + 1 < mBoard->getBoard()[row].size() && row<mBoard->getBoard().size()&& mBoard->getBoard()[row][col + 1] == vertex)
			return RIGHT;
		if (col  > 0 && row < mBoard->getBoard().size() && mBoard->getBoard()[row][col - 1] == vertex)
			return LEFT;
		if (row - 1 >= 0 && col < mBoard->getBoard()[row].size()&& mBoard->getBoard()[row - 1][col] == vertex)
			return UP;
		return mDirection;

	
}

bool Ghost::reverse(Direction d) const
{
	if (mFreight)
		return false;
	switch (d)
	{
	case UP: return   (mDirection == DOWN) ? true:false;
	case RIGHT:return (mDirection == LEFT) ? true : false;
	case DOWN:return  (mDirection == UP) ? true : false;
	case LEFT:return  (mDirection == RIGHT) ? true : false;
	default: return false;
	}
}

int Ghost::getHomeTimer() const
{
	return homeTimer;
}



bool Ghost::isInHome() const
{
	return inHome;
}

Ghost& Ghost::setHomeTimer(int htime)
{
	homeTimer = htime; return *this;
}



Ghost& Ghost::setInHome(bool h)
{
	inHome = h; return *this;
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


