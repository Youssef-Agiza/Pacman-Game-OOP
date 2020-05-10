#pragma once

#include <sstream>
#include "DEFINITIONS.h"
#include "MainMenu.h"
#include <iostream>

//namespace PacmanCS

//{
	MainMenuState::MainMenuState(Info data) : mData(data)
	{

	}

	void MainMenuState::Init()
	{
		this->mData->assets->LoadTexture("Game Title", TitleName);
		this->mData->assets->LoadTexture("Play Button", playButtonName);
		mTitle.setTexture(this->mData->assets->GetTexture("Game Title"));
		mplayb.setTexture(this->mData->assets->GetTexture("Play Button"));

		mTitle.setPosition((1840 / 2) - (mTitle.getGlobalBounds().width / 2), mTitle.getGlobalBounds().height / 2);
		mplayb.setPosition((1840 / 2) - (mplayb.getGlobalBounds().width / 2), (930 / 2) - (mplayb.getGlobalBounds().height / 2));
	}

	bool MainMenuState::HandleInput()
	{
		sf::Event event;

		while (this->mData->window->pollEvent(event))
		{

			if (this->mData->input.IsSpriteClicked(this->mplayb, sf::Mouse::Left, *this->mData->window))
			{
				//mData->machine->AddState(StateRef(new State))
				std::cout << "Go To Game Screen" << std::endl;
				return true;
			}
		}
		return true;
	}

	void MainMenuState::Update(float dt)
	{
		
	}

	void MainMenuState::Draw(float dt)
	{
		this->mData->window->clear(sf::Color::Black);

		this->mData->window->draw(this->mbckgrnd);
		this->mData->window->draw(this->mTitle);
		this->mData->window->draw(this->mplayb);

		this->mData->window->display();
	}
//}