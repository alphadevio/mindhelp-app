using AlexPacientes.Helpers;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Models.SQLite;
using AlexPacientes.Settings;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlexPacientes.Services
{
    public class DataAccess : IDisposable
    {
        private const string DB_NAME = "mindhelpPacient.db3";

        SQLiteAsyncConnection _connection;
        
        public DataAccess()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DB_NAME);
            _connection = (File.Exists(path)) ? new SQLiteAsyncConnection(path, SQLiteOpenFlags.ReadWrite) :
                new SQLiteAsyncConnection(path, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
            CreateTables();
        }
        protected async void CreateTables()
        {
            try
            {
                CreateTableResult sessionResult = await _connection.CreateTableAsync<SessionChatData>();
                CreateTableResult messageTable = await _connection.CreateTableAsync<MessageData>();
                CreateTableResult chatTable = await _connection.CreateTableAsync<ChatSession>();
                CreateTableResult credentialsTable = await _connection.CreateTableAsync<Credentials>();
                CreateTableResult notificationsTable = await _connection.CreateTableAsync<NotificationData>();
            }
            catch (Exception exx)
            {
                exx.ToString();
            }
        }

        public async void Dispose()
        {
            await _connection.CloseAsync();
        }

        public async Task<bool>SaveUserSession(Models.AppModels.Identity userCredentials)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userCredentials.Email) || string.IsNullOrWhiteSpace(userCredentials.Password))
                    return false;
                var encrypter = new EncryptionHelper(GlobalConfig.KEY, GlobalConfig.IV, 3600);
                var data = new Credentials()
                {
                    UserName = encrypter.Encrypt(userCredentials.Email),
                    Password = encrypter.Encrypt(userCredentials.Password),
                    UserID = userCredentials.ID,
                };

                return await _connection.InsertAsync(data)>0;
            }
            catch { return false; }
        }

        public async Task<Identity> GetUserSessionCredentials()
        {
            try
            {
                string query = "SELECT* FROM Credentials";
                var data = await _connection.QueryAsync<Credentials>(query);
                var item = data.LastOrDefault();
                var encrypter = new EncryptionHelper(GlobalConfig.KEY, GlobalConfig.IV, 3600);
                return new Identity()
                {
                    Email = encrypter.Decrypt(item.UserName),
                    Password = encrypter.Decrypt(item.Password)
                };
            }
            catch { return null; }
        }

        public async Task<int> GetUserIDSession()
        {
            try
            {
                string query = "SELECT * FROM Credentials";
                var data = await _connection.QueryAsync<Credentials>(query);
                var item = data.LastOrDefault();
                return item.UserID;
            }
            catch { return 0; }
        }

        public async Task<bool> DeleteAllSessions()
        {
            try
            {
                return await _connection.DeleteAllAsync<Credentials>() > 0;
            }
            catch { return false; }
        }

        public async Task<bool> SaveNotification(NotificationData notification)
        {
            try
            {
               return await _connection.InsertAsync(notification) > 0;
            }
            catch { return false; }
        }

        public async Task<List<NotificationData>> GetAllNotificationsFromUser(int userID)
        {
            try
            {
                string query = "SELECT * FROM Notifications WHERE user_id=?";
                var data = await _connection.QueryAsync<NotificationData>(query, userID);
                return data;
            }
            catch { return null; }
        }
    }
}
