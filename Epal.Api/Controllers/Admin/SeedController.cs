using Epal.Api.Controllers.Base;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Admin;

public class SeedController(ISender _, IEpalDbContext context, IPasswordService passwordService) : RestController(_)
{
    private static List<string> Names { get; set; } = [];
    
    [HttpGet("profiles")]
    public async Task SeedProfiles(int count)
    {
        var profiles = context.Users;
        var random = new Random();

        for (var i = 0; i < count; i++)
        {
            var passwordPlain = GenerateRandomString(5, random);
            var password = passwordService.HashPassword(passwordPlain);
            var email = GenerateRandomString(8, random) + "@nbl.ru";
            var bio = GenerateRandomBio(random);
            var username = GenerateRandomGamerNickname(4, 10, random);
            var epalStatusAcquiring = GenerateRandomPastDate(random);
            Names.Add(username);

            var profile = new Profile
            {
                Email = email,
                PasswordHash = password,
                Avatar = "https://global-oss.epal.gg/data/album/729833/1724368151270586.jpeg?x-oss-process=image/resize,m_fill,w_256,h_256",
                Gender = epalStatusAcquiring.GetHashCode() % 2 == 0 ? Gender.Man : Gender.Woman,
                Bio = bio,
                Username = username,
                Languages = "English",
                ProfileType = ProfileType.Epal,
                EpalStatusAcquiring = epalStatusAcquiring,
                Status = UserStatus.Confirmed
            };

            await profiles.AddAsync(profile, CancellationToken.None);
        }

        await context.SaveChangesAsync(CancellationToken.None);
        Names = [];
    }

    [HttpGet("categories")]
    public async Task SeedCategories(int count)
    {
        var categories = context.Categories;

        var seedingData = GetCategories(count);

        await categories.AddRangeAsync(seedingData, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    private string GenerateRandomString(int length, Random random)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private string GenerateRandomBio(Random random)
    {
        var words = new[]
        {
            "gamer", "fun", "play", "win", "team", "battle", "legend", "epic", "pro", "noob", "master", "quest",
            "victory", "challenge", "arena"
        };
        var wordCount = random.Next(2, 4); // Generates 2 or 3 words
        var bioWords = new List<string>();
        for (var i = 0; i < wordCount; i++) bioWords.Add(words[random.Next(words.Length)]);
        return string.Join(" ", bioWords);
    }

    private string GenerateRandomGamerNickname(int minLength, int maxLength, Random random)
    {
        
        var adjectives = new[]
        {
            "Dark", "Silent", "Furious", "Epic", "Crazy", "Mighty", "Wild", "Shadow", "Ghost", "Rapid", "Stealthy",
            "Brave", "Electric", "Swift"
        };
        var nouns = new[]
        {
            "Warrior", "Ninja", "Sniper", "Ranger", "Hunter", "Samurai", "Gamer", "Knight", "Dragon", "Wolf", "Phoenix",
            "Viper", "Falcon", "Reaper"
        };
        string nickname;
        do
        {
            nickname = adjectives[random.Next(adjectives.Length)] + nouns[random.Next(nouns.Length)];

            // Adjust nickname length to be within specified bounds
            if (nickname.Length > maxLength)
                nickname = nickname.Substring(0, maxLength);
            else if (nickname.Length < minLength)
                // Append random digits to reach minimum length
                nickname += random.Next(10, 99).ToString();
        } while (Names.Any(x => string.Equals(x, nickname)));

        return nickname;
    }

    private DateTime GenerateRandomPastDate(Random random)
    {
        // Generate a random date between 1 day ago and 1 year ago
        var daysAgo = random.Next(1, 365);
        return DateTime.UtcNow.AddDays(-daysAgo);
    }

    private Category[] GetCategories(int count)
    {
        Category[] seedingData =
        [
            new()
            {
                Name = "Make Friends",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_MakeFriends.png"
            },
            new()
            {
                Name = "E-Chat",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v4/img10_E_Chat.png"
            },
            new()
            {
                Name = "Valorant",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Valorant.png"
            },
            new()
            {
                Name = "League of Legends",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_LeagueofLegends.png"
            },
            new()
            {
                Name = "Movie",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_Movie.png"
            },
            new()
            {
                Name = "Fortnite",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Fortnite.png"
            },
            new()
            {
                Name = "Like Service",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_LikeService.png"
            },
            new()
            {
                Name = "Overwatch 2",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Overwatch2.png"
            },
            new()
            {
                Name = "Adding Socials",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_AddingSocials.png"
            },
            new()
            {
                Name = "Minecraft",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Minecraft.png"
            },
            new()
            {
                Name = "Teamfight Tactics",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_TeamfightTactics.png"
            },
            new()
            {
                Name = "Roblox",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Roblox.png"
            },
            new()
            {
                Name = "Emotional Support",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_EmotionalSupport.png"
            },
            new()
            {
                Name = "Dead by Daylight",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_DeadbyDaylight.png"
            },
            new()
            {
                Name = "eMeow Feeding",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_eMeowFeeding.png"
            },
            new()
            {
                Name = "Fall Guys",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_FallGuys.png"
            },
            new()
            {
                Name = "Apex Legends",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_ApexLegends.png"
            },
            new()
            {
                Name = "Among Us",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_AmongUs.png"
            },
            new()
            {
                Name = "VR Chat",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_VRChat.png"
            },
            new()
            {
                Name = "Phasmophobia",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_Phasmophobia.png"
            },
            new()
            {
                Name = "Karaoke",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_Karaoke.png"
            },
            new()
            {
                Name = "Genshin Impact",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_GenshinImpact.png"
            },
            new()
            {
                Name = "Language Exchange",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_LanguageExchange.png"
            },
            new()
            {
                Name = "Palworld",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_Palworld.png"
            },
            new()
            {
                Name = "Lethal Company",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_LethalCompany.png"
            },
            new()
            {
                Name = "Call of Duty",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_CallofDuty.png"
            },
            new()
            {
                Name = "Counter-Strike 2",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_CounterStrike2.png"
            },
            new()
            {
                Name = "Stardew Valley",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_StardewValley.png"
            },
            new()
            {
                Name = "Off My Chest",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_OffMyChest.png"
            },
            new()
            {
                Name = "Overcooked 2",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Overcooked2.png"
            },
            new()
            {
                Name = "Osu!",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Osu!.png"
            },
            new()
            {
                Name = "Grand Theft Auto V",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_GrandTheftAutoV.png"
            },
            new()
            {
                Name = "LoL: Wild Rift",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_LoL_WildRift.png"
            },
            new()
            {
                Name = "The Forest",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_TheForest.png"
            },
            new()
            {
                Name = "Rocket League",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_RocketLeague.png"
            },
            new()
            {
                Name = "Raft",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Raft.png"
            },
            new()
            {
                Name = "Drawing",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_Drawing.png"
            },
            new()
            {
                Name = "Chess",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Chess.png"
            },
            new()
            {
                Name = "CS:GO",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_CS_GO.png"
            },
            new()
            {
                Name = "Call of Duty®: Mobile - Garena",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_CallofDuty®_Mobile-Garena.png"
            },
            new()
            {
                Name = "It Takes Two",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_ItTakesTwo.png"
            },
            new()
            {
                Name = "Diablo 4",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_Diablo4.png"
            },
            new()
            {
                Name = "Free Fire",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_FreeFire.png"
            },
            new()
            {
                Name = "Brawl Stars",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_BrawlStars.png"
            },
            new()
            {
                Name = "Honor of Kings",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_HonorofKings.png"
            },
            new()
            {
                Name = "Rainbow Six",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_RainbowSix.png"
            },
            new()
            {
                Name = "Terraria",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Terraria.png"
            },
            new()
            {
                Name = "Don't Starve Together",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Don'tStarveTogether.png"
            },
            new()
            {
                Name = "Pokémon Unite",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_PokémonUnite.png"
            },
            new()
            {
                Name = "Fortune Telling",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_FortuneTelling.png"
            },
            new()
            {
                Name = "Farlight 84",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_Farlight84.png"
            },
            new()
            {
                Name = "Dota 2",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Dota2.png"
            },
            new()
            {
                Name = "Brawlhalla",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_Brawlhalla.png"
            },
            new()
            {
                Name = "Mario Kart Tour",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_MarioKartTour.png"
            },
            new()
            {
                Name = "8 Ball Pool™",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_8BallPool™.png"
            },
            new()
            {
                Name = "Party Animals",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_Partyanimal.png"
            },
            new()
            {
                Name = "Helldivers 2",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_helldivers2.png"
            },
            new()
            {
                Name = "Animal Crossing: New Horizons",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_AnimalCrossing_NewHorizons.png"
            },
            new()
            {
                Name = "Warframe",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Warframe.png"
            },
            new()
            {
                Name = "Pummel Party",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_PummelParty.png"
            },
            new()
            {
                Name = "NARAKA: BLADEPOINT",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_NARAKA_BLADEPOINT.png"
            },
            new()
            {
                Name = "Upbeat Conversation",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v3/img10_UpbeatConversation.png"
            },
            new()
            {
                Name = "Overwatch",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Overwatch.png"
            },
            new()
            {
                Name = "World of Warcraft",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_WorldofWarcraft.png"
            },
            new()
            {
                Name = "Mobile Legends: Bang Bang",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_MobileLegends_BangBang.png"
            },
            new()
            {
                Name = "Rust",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Rust.png"
            },
            new()
            {
                Name = "Valheim",
                Description = "Play",
                Avatar = "https://static-oss.epal.gg/data/static/v2/img10_v2_Valheim.png"
            }
        ];
        return seedingData.Take(count).ToArray();
    }
}