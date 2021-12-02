#pragma once
#include <list>
#include "IObserver.h"
class ISubject
{
protected:
	std::list<IObserver*> mObserverList;
public:
	virtual ~ISubject() = default;
	virtual void addObserver(IObserver* observer) = 0;
	virtual void removeObserver(IObserver* observer) = 0;
	virtual void notify() = 0; //notify pacman powerup
};