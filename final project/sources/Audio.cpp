#include "../headers/Audio.h"
namespace PacmanCS
{
	Audio* Audio::instance = nullptr;
	Audio* Audio::getInstance()
	{
		if (Audio::instance == nullptr)
			instance = new Audio();
		return instance;
	}

	Audio::Audio() :mSoundtracksPath("../soundtracks/") {}



	Audio& Audio::setPath(std::string path)
	{
		if (!path.empty())
			mSoundtracksPath = path;
		return *this;
	}
	bool Audio::playBeginning(bool loop, std::string track)
	{
		if (!mMusic.openFromFile(mSoundtracksPath + track))
			return false;
		mMusic.setLoop(loop);
		mMusic.play();
		return true;

	}
	bool Audio::playDeath(bool loop, std::string track)
	{
		if (!mMusic.openFromFile(mSoundtracksPath + track))
			return false;
		mMusic.setLoop(loop);
		mMusic.play();
		return true;
	}
	bool Audio::playEatGhost(bool loop, std::string track)
	{
		if (!mMusic.openFromFile(mSoundtracksPath + track))
			return false;
		mMusic.setLoop(loop);
		mMusic.play();
		return true;
	}
	bool Audio::playChomp(bool loop, std::string track)
	{
		if (mMusic.getStatus() == sf::SoundSource::Playing || !mMusic.openFromFile(mSoundtracksPath + track))
			return false;
		mMusic.setLoop(loop);
		mMusic.play();
		return true;
	}
	void Audio::stop()
	{
		mMusic.stop();
	}
	sf::SoundSource::Status Audio::getStatus() const
	{
		return mMusic.getStatus();
	}
}