using System.Reflection;
using System.Reflection.Emit;

namespace GreatestWarrior.Tests
{
    [TestClass]
    public class WarriorTests
    {
        private Warrior _warrior;

        [TestInitialize]
        public void Init()
        {
            _warrior = new Warrior();
        }

        [TestMethod]
        public void CreateWarrior_LevelShouldBeOne()
        {
            Assert.AreEqual(1, _warrior.Level);
        }

        [TestMethod]
        public void CreateWarrior_ExperieneSHould100()
        {
            Assert.AreEqual(100, _warrior.Experience);
        }

        [TestMethod]
        public void CreateWarrior_RankShouldBePushover()
        {
            Assert.AreEqual(Rank.Pushover, _warrior.Rank);
        }

        [TestMethod]
        public void CreateWarrior_AchievmentsShouleBeEmpty()
        {
            Assert.AreEqual(0, _warrior.CountAchievments());
        }

        [TestMethod]
        [DataRow(100, 1)]
        [DataRow(500, 5)]
        [DataRow(555, 5)]
        [DataRow(789, 7)]
        [DataRow(8500, 85)]
        [DataRow(10000, 100)]
        [DataRow(11000, 100)]
        public void WarriorLevel_ShouldBeBasedOnExperience(int actualExperience, int expectedLevel)
        {
            // Arrange
            _warrior.Experience = actualExperience;

            // Action
            _warrior.UpdateLevel();

            // Assert
            Assert.AreEqual(expectedLevel, _warrior.Level);
        }

        [TestMethod]
        [DataRow(5, Rank.Pushover)]
        [DataRow(9, Rank.Pushover)]
        [DataRow(10, Rank.Novice)]
        [DataRow(55, Rank.Sage)]
        [DataRow(99, Rank.Master)]
        [DataRow(100, Rank.Greatest)]
        public void WarriorRank_ShouldBeBasedOnLevel(int actualLevel, Rank expectedRank)
        {
            _warrior.Level = actualLevel;
            _warrior.UpdateRank();

            Assert.AreEqual(expectedRank, _warrior.Rank);
        }

        [TestMethod]
        [DataRow(100, "Defeat a slime")]
        [DataRow(9000, "Defeat Chuck Norris")]
        public void WarriorExperienceAndAchievments_AreCorrectlyModifiedAfterTraining(int experienceEarned, string achievmentLabel)
        {
            _warrior.Training(achievmentLabel, experienceEarned);

            Assert.AreEqual(experienceEarned + 100, _warrior.Experience);
            Assert.AreEqual(1, _warrior.Achievments.Count);
            Assert.AreEqual(achievmentLabel, _warrior.Achievments.First());
        }

        [TestMethod]
        [DataRow(50)]
        [DataRow(90)]
        [DataRow(990)]
        [DataRow(5000)]
        [DataRow(10000)]
        public void WarriorExperienceIsXEnemyLevelIsEqual_Battle_WarriorEarn10XP(int warriorExperience)
        {
            _warrior.Experience = warriorExperience;
            _warrior.UpdateLevel();

            var enemyLevel = _warrior.Level;

            _warrior.Battle(enemyLevel);

            Assert.AreEqual(warriorExperience + 10, _warrior.Experience);
        }

        [TestMethod]

        [DataRow(50, 2)]
        [DataRow(90, 5)]
        [DataRow(990, 10)]
        [DataRow(5000, 2)]
        [DataRow(10000, 7)]
        public void WarriorExperienceIsXEnemyLevelIsLowerByTwoOrMore_Battle_WarriorEarnNoExperience(int warriorExperience
            , int enemySubstractLevel)
        {
            _warrior.Experience = warriorExperience;
            _warrior.UpdateLevel();

            var enemyLevel = _warrior.Level - enemySubstractLevel;

            _warrior.Battle(enemyLevel);

            Assert.AreEqual(warriorExperience, _warrior.Experience);
        }

        [TestMethod]
        [DataRow(90)]
        [DataRow(990)]
        [DataRow(5000)]
        [DataRow(10000)]
        public void WarriorExperienceIsXEnemyLevelIsLowerByOne_Battle_WarriorEarn5Experience(int warriorExperience)
        {
            _warrior.Experience = warriorExperience;
            _warrior.UpdateLevel();

            var enemyLevel = _warrior.Level - 1;

            _warrior.Battle(enemyLevel);

            Assert.AreEqual(warriorExperience + 5, _warrior.Experience);
        }

        [TestMethod]
        [DataRow(90, 3)]
        [DataRow(990, 4)]
        [DataRow(5000, 3)]
        [DataRow(10000, 4)]
        public void WarriorExperienceIsXEnemyLevelIsGreaterThanTwoButLessThanFive_Battle_WarriorEarnExperienceFormula(
            int warriorExperience,
            int enemyAdditionalLevel)
        {
            _warrior.Experience = warriorExperience;
            _warrior.UpdateLevel();

            var enemyLevel = _warrior.Level + enemyAdditionalLevel;

            _warrior.Battle(enemyLevel);
            Assert.AreEqual(warriorExperience + 20 * (_warrior.Level - enemyLevel) * (_warrior.Level - enemyLevel)
                , _warrior.Experience);
        }

        [TestMethod]
        [DataRow(90, 5)]
        [DataRow(990, 7)]
        [DataRow(5000, 9)]
        [DataRow(10000, 5)]
        public void WarriorExperienceIsXEnemyLevelIsGreaterByFiveOrMore_Battle_BattleIsEnded(int warriorExperience
            , int enemyAdditionalLevel)
        {
            _warrior.Experience = warriorExperience;
            _warrior.UpdateLevel();

            var enemyLevel = _warrior.Level + enemyAdditionalLevel;

            _warrior.Battle(enemyLevel);

            Assert.IsTrue(_warrior.GetIsEnded());
        }

        // TODO: Faire quelques tests fonctionnels (multiples batailles check XP / batailles + training
        // bataille + end / batailles jusqu'à rank max)
    }
}