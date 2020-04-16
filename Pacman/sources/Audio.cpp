#include "Audio.h"
namespace PacmanCS
{
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
		if (!mMusic.openFromFile(mSoundtracksPath + track))
			return false;
		mMusic.setLoop(loop);
		mMusic.play();
		return true;
	}
}