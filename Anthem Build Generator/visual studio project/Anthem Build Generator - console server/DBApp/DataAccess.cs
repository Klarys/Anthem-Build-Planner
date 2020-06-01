using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using AnthemBuilderLibrary;
using Caliburn.Micro;
using System.Transactions;

namespace DBApp
{
    /// <summary>
    /// Class containing methods used in communication with the database
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// Test method
        /// </summary>
        /// <returns></returns>
        public List<Test> GetTests()
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<Test>("Select * from test").ToList();
                return output;
            }
        }

        /// <summary>
        /// Gets build's details from the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public Build GetBuild(int buildId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<Build>($"SELECT * FROM Builds WHERE buildId = {buildId}").ToList();
                return output[0];
            }
        }

        /// <summary>
        /// Gets user's details from the database
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public List<User> GetUser(string login)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<User>($"Select * from Users where Login = '{login}'").ToList();
                return output;
            }
        }

        /// <summary>
        /// Gets user's salt from the database
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public string GetSalt(string login)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<User>($"SELECT u.Salt FROM Users u Where Login='{login}'").ToList();
                if (!output.Any())
                {
                    return "false";
                }
                else
                {
                    return output[0].Salt;
                }
            }
        }

        /// <summary>
        /// Gets builds components from the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public List<Component> GetBuildsComponents(int buildId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<Component>($"SELECT c.ComponentId, c.Name, c.ShieldReinforcement, c.ArmorReinforcement, c.NormalEffect, c.SpecialEffect, c.ClassId, c.RarityId FROM Components c JOIN ComponentInBuild cc ON (cc.ComponentId = c.ComponentId) Where cc.BuildId ='{buildId}'").ToList();
                return output;
            }
        }

        /// <summary>
        /// Returns a user with given login and password from the database (if user exists)
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public List<User> GetUser(string login, string password)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<User>($"SELECT * FROM Users Where Login='{login}' AND Password = '{password}'").ToList();
                return output;
            }
        }

        /// <summary>
        /// Gets list of classes from the database
        /// </summary>
        /// <returns></returns>
        public List<Class> GetClassesWithoutUniversal()
        {
            var result = new List<Class>();
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<Class>($"SELECT * From Class").ToList();              
                foreach(Class Cl in output)
                {
                    if(Cl.ClassId != 0)
                    {
                        result.Add(Cl);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if User with given login exists in the database
        /// </summary>
        /// <param name="logIn"></param>
        /// <returns></returns>
        public bool UserExits(string logIn)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<User>($"Select * from Users where Login = '{logIn}'").ToList();
                if(output.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Checks if User with given email exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool EmailExits(string email)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<User>($"Select * from Users where Email = '{email}'").ToList();
                if (output.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets a new, unused userId
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public int GetNewUserId(User newUser)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<int>($"Select UserId from Users where Login = '{newUser.Login}'").ToList();
                return output[0];
            }
        }

        /// <summary>
        /// Inserts a new user into the database
        /// </summary>
        /// <param name="newUser"></param>
        public void RegisterNewUser(User newUser)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                connection.Query($"INSERT INTO Users (`UserName`, `Email`, `Login`, `Password`, `Salt`, `Description`) VALUES('{newUser.UserName}', '{newUser.Email}', '{newUser.Login}', '{newUser.Password}', '{newUser.Salt}', '{newUser.Description}')");
            }
        }

        /// <summary>
        /// Gets strike systems suitable for the given class from the database
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<StrikeSystem> GetStrikeSystems(int classId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<StrikeSystem>($"Select * from StrikeSystem where ClassId = '{classId}'").ToList();
                return output;
            }
        }

        /// <summary>
        /// Gets support systems suitable for the given class from the database
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<SupportSystem> GetSupportSystems(int classId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<SupportSystem>($"Select * from SupportSystem where ClassId = '{classId}'").ToList();
                return output;
            }
        }

        /// <summary>
        /// Gets weapons from the database
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<Weapon> GetWeapons()
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<Weapon>("Select * from Weapons").ToList();
                return output;
            }
        }

        /// <summary>
        /// Gets components from the database
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<Component> GetComponents(int classId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<Component>($"Select * from Components where ClassId = '{classId}' or ClassId = 0").ToList();
                return output;
            }
        }

        /// <summary>
        /// Gets assault systems suitable for the given class from the database
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<AssaultSystem> GetAssaultSystems(int classId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<AssaultSystem>($"Select * from AssaultSystem where ClassId = '{classId}'").ToList();
                return output;
            }
        }

        /// <summary>
        /// Gets build's weapons from the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public List<Weapon> GetBuildsWeapons(int buildId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<Weapon>($"SELECT w.WeaponId, w.Name, w.Damage, w.Rpm, w.Ammo, w.SpecialEffect, w.ClassId, w.RarityId FROM Weapons w JOIN WeaponInBuild ww ON (ww.WeaponId = w.Weaponid) WHERE ww.BuildId = '{buildId}'").ToList();
                return output;
            }
        }

        /// <summary>
        /// Gets the list of ingame rarities of the items from the database
        /// </summary>
        /// <returns></returns>
        public List<Rarity> GetRarities()
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<Rarity>("SELECT * from Rarity").ToList();
                return output;
            }
        }

        /// <summary>
        /// Increases build's rating in the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="wasRatedDown"></param>
        public void RateBuildUp(int buildId, bool wasRatedDown)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                if(wasRatedDown)
                {
                    connection.Query<User>($"Update Builds Set Rating = Rating+2 where BuildId = '{buildId}'");
                }
                else
                {
                    connection.Query<User>($"Update Builds Set Rating = Rating+1 where BuildId = '{buildId}'");
                }               
            }
        }

        /// <summary>
        /// Gets user's builds with classnames from the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Build> GetSavedBuildsWithClasses(int userId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var classes = connection.Query<Build>("Select * from Class").ToList();
                var output = connection.Query<Build>($"Select * from Builds Where buildId IN (SELECT buildId FROM SavedBuilds WHERE userId = '{ userId }')").ToList();
                foreach (Build build in output)
                {
                    build.Class = classes[build.ClassId].Name;
                }
                return output;
            }
        }

        /// <summary>
        /// Gets build's class 
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public Class GetClass(int buildId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<Class>($"SELECT c.ClassId, c.Name FROM Class c JOIN Builds b ON (b.ClassId = c.ClassId) WHERE b.BuildId = {buildId}").ToList();
                return output[0];
            }
        }

        /// <summary>
        /// Decreases build's rating in the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="wasRatedDown"></param>
        public void RateBuildDown(int buildId, bool wasRatedUp)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                if(wasRatedUp)
                {
                    connection.Query($"Update Builds Set Rating = Rating-2 where BuildId = '{buildId}'");
                }
                else
                {
                    connection.Query($"Update Builds Set Rating = Rating-1 where BuildId = '{buildId}'");
                }               
            }
        }

        /// <summary>
        /// Creates a copy of a build
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="userId"></param>
        public void Copy(int buildId, int userId)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                Build tmpbuild = GetBuild(buildId);
                tmpbuild.AuthorId = userId;
                tmpbuild.Rating = 0;
                tmpbuild.Name = "Kopia - " + tmpbuild.Name;
                List<Component> tmpcomponents = GetBuildsComponents(buildId);
                List<Weapon> tmpweapons = GetBuildsWeapons(buildId);
                InsertNewBuild(tmpbuild, tmpcomponents, tmpweapons);
                transaction.Complete();
            }

        }

        /// <summary>
        /// Gets builds with classnames from the database
        /// </summary>
        /// <returns></returns>
        public List<Build> GetBuildsWithClasses()
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var classes = connection.Query<Build>("Select * from Class").ToList();
                var output = connection.Query<Build>("Select * from Builds").ToList();
                foreach (Build build in output)
                {
                    build.Class = classes[build.ClassId].Name;
                }
                return output;
            }
        }

        /// <summary>
        /// Gets build's assault system from the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public AssaultSystem GetAssaultSystem(int buildId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<AssaultSystem>($"SELECT a.AssaultSystemId, a.Name, a.Damage, a.Recharge, a.Radius, a.Charges, a.SpecialEffect, a.ClassId, a.RarityId FROM AssaultSystem a JOIN Builds b ON (b.AssaultSystemId = a.AssaultSystemId) WHERE b.BuildId = {buildId}").ToList();
                return output[0];
            }
        }

        /// <summary>
        /// Gets build's support system from the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public SupportSystem GetSupportSystem(int buildId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<SupportSystem>($"SELECT s.SupportSystemId, s.Name, s.Charges, s.Recharge, s.Radius, s.ClassId, s.RarityId FROM SupportSystem s JOIN Builds b ON (b.SupportSystemId = s.SupportSystemId) WHERE b.BuildId = {buildId}").ToList();
                return output[0];
            }
        }

        /// <summary>
        /// Gets build's strike system from the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public StrikeSystem GetStrikeSystem(int buildId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                var output = connection.Query<StrikeSystem>($"SELECT s.StrikeSystemId, s.Name, s.Damage, s.Recharge, s.Radius, s.Charges, s.SpecialEffect, s.ClassId, s.RarityId FROM StrikeSystem s JOIN Builds b ON (b.StrikeSystemId = s.StrikeSystemId) WHERE b.BuildId = {buildId}").ToList();
                return output[0];
            }
        }

        /// <summary>
        /// Inserts the build into user's saved builds in the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="userId"></param>
        public void Save(int buildId, int userId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                connection.Query($"INSERT INTO SavedBuilds (BuildId, UserId) VALUES('{buildId}', '{userId}')");
            }
        }

        /// <summary>
        /// Checks if given build is already in user's saved builds
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CanSave(int buildId, int userId)
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                bool result = true;
                var data = connection.Query<int>($"SELECT UserId from SavedBuilds where BuildId = {buildId}").ToList();
                foreach(int id in data)
                {
                    if (id == userId)
                    {
                        result = false;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Inserts a new build into the database
        /// </summary>
        /// <param name="createdBuild"></param>
        /// <param name="selectedComponents"></param>
        /// <param name="selectedWeapons"></param>
        public void InsertNewBuild(Build createdBuild, List<Component> selectedComponents, List<Weapon> selectedWeapons)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
                {
                    connection.Query($"INSERT INTO Builds (`Name`, `AdditionalNotes`, `AuthorId`, `Rating`, `StrikeSystemId`, `AssaultSystemId`, `SupportSystemId`, `ClassId`) VALUES ('{createdBuild.Name}', '{createdBuild.AdditionalNotes}', '{createdBuild.AuthorId}', '0' , '{createdBuild.StrikeSystemId}', '{createdBuild.AssaultSystemId}', '{createdBuild.SupportSystemId}', '{createdBuild.ClassId}')");
                    int newBuildId = GetNewBuildId();
                    for (int i = 0; i < selectedComponents.Count; i++)
                    {
                        connection.Query($"INSERT INTO `ComponentInBuild` (`BuildId`, `ComponentId`) VALUES ('{newBuildId}', '{selectedComponents[i].ComponentId}')");
                    }
                    for (int i = 0; i < selectedWeapons.Count; i++)
                    {
                        connection.Query($"INSERT INTO `WeaponInBuild` (`Buildid`, `Weaponid`) VALUES ('{newBuildId}', '{selectedWeapons[i].WeaponId}')");
                    }
                    connection.Query($"INSERT INTO `SavedBuilds` (`BuildId`, `UserId`) VALUES ('{newBuildId}', '{createdBuild.AuthorId}')");
                }
                transaction.Complete();
            }
        }

        /// <summary>
        /// Saves changes of edited build into the database
        /// </summary>
        /// <param name="buildId"></param>
        /// <param name="createdBuild"></param>
        /// <param name="selectedComponents"></param>
        /// <param name="selectedWeapons"></param>
        public void UpdateBuild(int buildId, Build createdBuild, List<Component> selectedComponents, List<Weapon> selectedWeapons)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
                {
                    connection.Query($"UPDATE Builds SET `Name` = '{createdBuild.Name}', `AdditionalNotes` = '{createdBuild.AdditionalNotes}', `ClassId` = {createdBuild.ClassId}, `StrikeSystemId` = '{createdBuild.StrikeSystemId}', `AssaultSystemId` = '{createdBuild.AssaultSystemId}', `SupportSystemId` = '{createdBuild.SupportSystemId}' WHERE `Builds`.`BuildId` = '{createdBuild.BuildId}'");
                    connection.Query($"DELETE FROM ComponentInBuild WHERE BuildId = '{createdBuild.BuildId}'");
                    for (int i = 0; i < selectedComponents.Count; i++)
                    {
                        connection.Query($"INSERT INTO `ComponentInBuild` (`BuildId`, `ComponentId`) VALUES ('{createdBuild.BuildId}', '{selectedComponents[i].ComponentId}')");
                    }
                    connection.Query($"DELETE FROM WeaponInBuild WHERE BuildId = '{createdBuild.BuildId}'");
                    for (int i = 0; i < selectedWeapons.Count; i++)
                    {
                        connection.Query($"INSERT INTO `WeaponInBuild` (`Buildid`, `Weaponid`) VALUES ('{createdBuild.BuildId}', '{selectedWeapons[i].WeaponId}')");
                    }
                }
                transaction.Complete();
            }
        }

        /// <summary>
        /// Gets a new, unused buildid from the database
        /// </summary>
        /// <returns></returns>
        public int GetNewBuildId()
        {
            using (IDbConnection connection = new MySqlConnection(DbConnectionHelper.CnnVal("epiz_23993350_anthem00")))  //SqlConnection(Helper.CnnVal("epiz_23993350_anthem00")))
            {
                int result = connection.Query<int>($"SELECT MAX(BuildId) FROM Builds").First();
                return result;
            }
        }
    }

}
