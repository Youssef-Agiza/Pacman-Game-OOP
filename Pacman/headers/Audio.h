#pragma once
#include <SFML/Audio.hpp>
#include <string>
namespace PacmanCS {

	class Audio
	{
	private:
		sf::Music mMusic;
		std::string mSoundtracksPath;
	public:
		Audio();
		Audio& setPath(std::string);
		bool  playDeath(bool loop=false, std::string track="pacman_death.wav");
		bool playBeginning( bool loop = false,std::string track = "pacman_beginning.wav");
		bool playEatGhost(bool loop=false, std::string track = "pacman_eatghost.wav");
		bool playChomp(bool loop = false, std::string track = "pacman_chomp.wav");
		void stop();
		sf::SoundSource::Status getStatus()const;
	};
}