#pragma once

#include <sstream>
#include "SplashScreen.h"
#include "DEFINITIONS.h"
#include "MainMenu.h"

#include <iostream>

//namespace PacmanCS
//{
	SplashState::SplashState(Info data) : mData(data)
	{

	}

	void SplashState::Init()
	{
		this->mData->assets->LoadTexture("Splash ", background);

		mbckgrnd.setTexture(this->mData->assets->GetTexture("Splash "));
		mbckgrnd.setPosition(500, 120 );

	}

	bool SplashState::HandleInput()
	{
		sf::Event event;

		while (this->mData->window->pollEvent(event))
		{
			if (sf::Event::Closed == event.type)
			{
				this->mData->window->close();
				return false;
			}
		}
	}

	void SplashState::Update(float dt)
	{
		if (this->clock.getElapsedTime().asSeconds() > SPLASH_STATE_SHOW_TIME)
		{

			this->mData->machine->AddState(StateRef(new MainMenuState(mData)), true);
		}
	}

	void SplashState::Draw(float dt)
	{
		this->mData->window->clear(sf::Color::Black);

		this->mData->window->draw( this->mbckgrnd );

		this->mData->window->display();
	}
//}