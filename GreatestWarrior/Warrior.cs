using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatestWarrior
{
    public class Warrior
    {
        public int Level { get; set; }
        public int Experience { get; set; }
        public Rank Rank { get; set; }
        public List<string> Achievments { get; set; }
        private const int _experienceByLevel = 100;
        private bool IsEnded = false;

        public Warrior()
        {
            Level = 1;
            Experience = 100;
            Rank = Rank.Pushover;
            Achievments = new List<string>();
        }

        public void UpdateLevel()
        {
            if (Experience <= 10000)
                Level = (int)Decimal.Truncate(Experience / _experienceByLevel);
            else
                Level = 100;
        }

        public void UpdateRank()
        {
            Rank = (Rank) Decimal.Truncate(Level / 10) + 1;
        }

        public string Battle(int enemyLevel)
        {
            if (Level - enemyLevel >= 2)
            {
                return "Easy fight";
            } 
            else if (Level - enemyLevel == 1) 
            {
                Experience += 5;
                return "Good fight";
            }
            else if (Level == enemyLevel)
            {
                Experience += 10;
                return "Good fight";
            }
            else if (Level - enemyLevel <= -5)
            {
                IsEnded = true;
                return "Battle lost";
            }
            else if (Level - enemyLevel < 0)
            {
                Experience += 20 * (Level - enemyLevel) * (Level - enemyLevel);
                return "Intense fight";
            }

            return "Case unhandled";
        }

        // La méthode d'entrainement a été grandement simplifiée dans ce corrigé
        // TODO: à faire complètement
        public void Training(string achievment, int experienceEarned)
        {
            Experience += experienceEarned;
            Achievments.Add(achievment);
        }

        public bool GetIsEnded()
        {
            return IsEnded;
        }

        public int CountAchievments()
        {
            return Achievments.Count;
        }
    }
}
