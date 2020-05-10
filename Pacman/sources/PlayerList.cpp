#include "../headers/PlayerList.h"

PlayerList::PlayerList(std::string fname):playerList(std::list<PlayerData>()),fileName(fname)
{
	

}

PlayerList::~PlayerList()
{
	if(ins.is_open())
	ins.close();
	if(outs.is_open())
	outs.close();
}

void PlayerList::LoadFiletoList()
{
	if (outs.is_open())
		outs.close();
	ins.open(fileName);
	if (!ins.is_open())
		return;
	string line = "";
	string userName = "";
	string email = "";
	string score = "";

	while (getline(ins,line))
	{
		stringstream iss(line);
		getline(iss, userName, ',');
		getline(iss, email, ',');
		getline(iss, score, ',');
		

		playerList.push_back(PlayerData(userName,email,stoi(score.c_str())));
	}
	ins.close();
}

void PlayerList::loadListToFile()
{
	if (ins.is_open())
		ins.close();
	outs.open(fileName);
	if (!outs.is_open())
		return;
	for (auto player : playerList)
	{
		outs << player.userName << "," << player.email << "," << 
			player.highScore<<","<<std::endl;
	}
}

std::list<PlayerData>& PlayerList::getList()
{
	return playerList;
}

PlayerData& PlayerList::getHighestScore() const
{
	int currentIndex=0;
	int index=0;
	int highestScore=0;
	for (auto player:playerList)
	{
		currentIndex++;
		if (player.highScore > highestScore)
		{
			index = currentIndex;
			highestScore = player.highScore;
		}
	}
	currentIndex = 0;
	for (auto player : playerList)
	{
		currentIndex++;
		if (currentIndex == index)
			return player;
	}

		

}

void PlayerList::addPlayer(PlayerData player)
{
	playerList.push_back(player);
}
