#pragma once
class ISubject;
class IObserver
{
public:
	virtual ~IObserver() = default;
	virtual void update(bool powerUp) = 0;
};